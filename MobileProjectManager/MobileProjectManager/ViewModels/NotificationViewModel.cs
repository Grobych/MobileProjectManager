using MobileProjectManager.Models;
using MobileProjectManager.ViewModels.Utils;
using MobileProjectManager.Views.TaskViews;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                    case NotificationType.InviteToTeam :
                        {
                            User user = Database.Database.GetUser(Notification.From);
                            return "User " + user.Name + " was invited you to Team";
                        }
                    case NotificationType.WorkerAddedToProject:
                        {
                            string Name = Notification.Line.GetValue("ProjectName").AsString;
                            User user = Database.Database.GetUser(Notification.From);
                            return "User " +user.Name+" was added you to project "+Name;
                        }
                    case NotificationType.InviteAccepted:
                        {
                            string UserName = Notification.Line.GetValue("UserName").AsString;
                            string TeamName = Notification.Line.GetValue("TeamName").AsString;
                            return "User " + UserName + " was accepted you invite to team " + TeamName;
                        }
                    case NotificationType.InviteDenied:
                        {
                            string UserName = Notification.Line.GetValue("UserName").AsString;
                            string TeamName = Notification.Line.GetValue("TeamName").AsString;
                            return "User " + UserName + " was denied you invite to team " + TeamName;
                        }
                    case NotificationType.GetTaskReport:
                        {
                            string UserName = Notification.Line.GetValue("UserName").AsString;
                            string TaskName = Notification.Line.GetValue("TaskName").AsString;
                            return "User " + UserName + " get the task " + TaskName;
                        }
                    case NotificationType.TaskCompleteReport:
                        {
                            string UserName = Notification.Line.GetValue("UserName").AsString;
                            string ProjectName = Notification.Line.GetValue("ProjectName").AsString;
                            string TaskName = Notification.Line.GetValue("Task").AsString;
                            string Comment = Notification.Line.GetValue("Comment").AsString;
                            return "User " + UserName + " completed task " + TaskName + " in " + ProjectName + "\nCpmment: " + Comment;
                        }
                    case NotificationType.TaskReportApproved:
                        {
                            string TaskName = Notification.Line.GetValue("TaskName").AsString;
                            return "Task " + TaskName + " has been approved by project manager";
                        }
                    case NotificationType.TaskReportDeclined:
                        {
                            string TaskName = Notification.Line.GetValue("TaskName").AsString;
                            string Comment = Notification.Line.GetValue("Comment").AsString;
                            return "Task " + TaskName + " has not been approved by project manager;\nComment: "+Comment;
                        }
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
                        InviteToTeam(NVM);
                        break;
                    }
                case NotificationType.TaskCompleteReport:
                    {
                        TaskCompleteReport(NVM);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private async void InviteToTeam(NotificationViewModel NVM)
        {
            BsonDocument bson = NVM.Notification.Line.ToBsonDocument();
            string TeamName = bson.GetElement("TeamName").ToString();
            string TeamManager = bson.GetElement("UserName").ToString();
            bool res = await Application.Current.MainPage.DisplayAlert("Notification", "User " + TeamManager + " invite you to team " + TeamName, "Ok", "Cancel");
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
                    Line = new BsonDocument().Add("UserName", Auth.CurrentUser.Name).Add("TeamName", TeamName)
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
        }
        private async void TaskCompleteReport(NotificationViewModel NVM)
        {
            bool res = await Application.Current.MainPage.DisplayAlert("Notification", NVM.Definition , "To Task Page", "Close");
            if (res)
            {
                List<Models.Task> tasks = Database.Database.GetTaskFromUser(Auth.CurrentUser);
                Task finded = tasks.Find(item => item.Name == NVM.Notification.Line.GetValue("Task"));
                // TODO: can be tlv == null 
                await NavigationUtil.Navigation.PushAsync(new TaskPage(new TaskViewModel(finded)));
            }
        }
    }
}
