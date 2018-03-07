using Acr.UserDialogs;
using MobileProjectManager.Models;
using MobileProjectManager.ViewModels.Utils;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        public Notification Notification { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public NotificationViewModel(Notification notification)
        {
            this.Notification = notification;
        }

        public NotificationType Type
        {
            get
            {
                // TODO: Get user orientired Types
                return Notification.Type;
            }
        }

        public string Definition
        {
            get
            {
                switch (Notification.Type)
                {
                    // TODO: Name instead ID
                    case NotificationType.InviteToTeam : return "User #" + Notification.From + " was invited you to Team";
                    default: return "Default Notification";
                }
            }
        }

    }


    public class NotificationListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NotificationViewModel> Notifications { get; set; }
        public NotificationViewModel selectedNotification;
        public NotificationViewModel currentNotification { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public NotificationListViewModel(User user)
        {
            Notifications = new ObservableCollection<NotificationViewModel>();
            // TODO: add new thread with update in every N sec.
            List <Notification> list = Database.Database.getNotifications(user);
            foreach (var item in list)
            {
                Notifications.Add(new NotificationViewModel(item));
            }
        }

        public NotificationViewModel SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                if (selectedNotification != value)
                {
                    selectedNotification = null;
                    currentNotification = value;
                    ShowNotification(currentNotification);
                    Database.Database.DeleteNotification(currentNotification.Notification.ID);
                    Notifications.Remove(currentNotification);
                    OnPropertyChanged("SelectedNotification");
                }
            }
        }

        private async void ShowNotification(NotificationViewModel NVM)
        {
            switch (NVM.Type)
            {
                case NotificationType.InviteToTeam:
                    {
                        BsonDocument bson = NVM.Notification.Line.ToBsonDocument();
                        bool res = await Application.Current.MainPage.DisplayAlert("Notification", "User " + bson.GetElement("UserName") + " invite you to team " + bson.GetElement("TeamName"), "Ok","Cancel");
                        if (res)
                        {
                            Team team = Database.Database.GetTeam(bson.GetElement("TeamId").Value.AsObjectId);
                            team.WorkersID.Add(Auth.CurrentUser.ID);
                            Database.Database.UpdateTeam(team);
                            Notification answer = new Notification
                            {
                                To = NVM.Notification.From,
                                From = NVM.Notification.To,
                                Type = NotificationType.InviteAccepted,
                                Line = new BsonDocument().Add("Line","OK")
                            };
                            Database.Database.AddNotification(ref answer);
                        }
                        else
                        {
                            Notification answer = new Notification
                            {
                                To = NVM.Notification.From,
                                From = NVM.Notification.To,
                                Type = NotificationType.InviteDenied,
                                Line = new BsonDocument().Add("Line", "NOT OK")
                            };
                            Database.Database.AddNotification(ref answer);
                        }
                        break;
                    }
                default:
                    {
                        break;
                        //
                    }
            }
        }
    }

}
