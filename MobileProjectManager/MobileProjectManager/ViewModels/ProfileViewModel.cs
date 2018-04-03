using System;
using System.ComponentModel;
using System.Windows.Input;
using MobileProjectManager.Models;
using MongoDB.Bson;
using Xamarin.Forms;

using MobileProjectManager.ViewModels;
using MobileProjectManager.Views;
using System.Collections.Generic;
using MobileProjectManager.Views.TaskViews;
using MobileProjectManager.ViewModels.Utils;
using System.Diagnostics;

namespace MobileProjectManager.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        TeamListViewModel TeamListView { get; set; }
        public ProjectListViewModel ProjectListView { get; set; }
        public ICommand ToProjectListCommand { protected set; get; }
        public ICommand ToTeamListCommand { protected set; get; }
        public ICommand ToNotificationListCommand { protected set; get; }
        public ICommand ToTaskListCommand { protected set; get; }
        public ICommand CallCommand { protected set; get; }

        public User User { get; set; }

        public ProfileViewModel(User user)
        {
            this.User = user;
            ToProjectListCommand = new Command(ToProjectList);
            ToTeamListCommand = new Command(ToTeamsList);
            ToNotificationListCommand = new Command(ToNotificationList);
            ToTaskListCommand = new Command(ToTaskList);
            CallCommand = new Command(CallUserAsync);
            TeamListView = new TeamListViewModel(this);
        }

        private async void CallUserAsync()
        {
            await DependencyService.Get<IPhoneCall>()?.Call(User.Number);
        }

        private void ToTaskList(object obj)
        {
            // TODO: crash to taskList from profile page
            List<Models.Task> list = Database.Database.GetTaskFromUser(User);
            NavigationUtil.Navigation.PushAsync(new TaskListPage(new TaskListViewModel(list)));
        }

        private void ToNotificationList(object obj)
        {
            NavigationUtil.Navigation.PushAsync(new NotificationsPage(new NotificationListViewModel(this.User)));
        }

        private void ToProjectList(object obj)
        {
            ProjectListView = new ProjectListViewModel();
            NavigationUtil.Navigation.PushAsync(new ProjectListPage(ProjectListView));
        }
        private void ToTeamsList(object obj)
        {
            List<Team> teams = Database.Database.GetTeamsFromDB(this.User);
            foreach (var item in teams)
            {
                this.TeamListView.TeamList.Add(new TeamViewModel(item));
            }
            NavigationUtil.Navigation.PushAsync(new TeamListPage(TeamListView));
        }
        public ObjectId ID
        {
            get { return User.ID; }
            set
            {
                if (User.ID != value)
                {
                    User.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string Name
        {
            get { return User.Name; }
            set
            {
                if (User.Name != value)
                {
                    User.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public bool IsOnline
        {
            get { return User.IsOnline; }
            set
            {
                if (User.IsOnline != value)
                {
                    User.IsOnline = value;
                    OnPropertyChanged("IsOlnine");
                }
            }
        }

        public Image Img
        {
            get
            {
                return User.Img;
            }
            set
            {
                if (User.Img != value)
                {
                    User.Img = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        public string Number
        {
            get { return User.Number; }
            set
            {
                if (User.Number != value)
                {
                    User.Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public bool IsUserPage
        {
            get { return User.ID == Auth.CurrentUser.ID; }
        }
        public bool IsNotUserPage
        {
            get { return !IsUserPage; }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
