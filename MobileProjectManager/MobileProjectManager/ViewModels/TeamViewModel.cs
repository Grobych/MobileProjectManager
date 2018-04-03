using MobileProjectManager.Models;
using MobileProjectManager.ViewModels.Utils;
using MobileProjectManager.Views;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class TeamViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel Manager { get; set; }
        public ObservableCollection<ProfileViewModel> Workers { get; set; }
        public Team Team { get; set; }

        ProfileViewModel selectedUser;
        public ProfileViewModel CurrentWorker { get; set; }

        public ICommand ToTeamManagerPageCommand { protected set; get; }
        public ICommand AddWorkerCommand { protected set; get; }


        public TeamViewModel(string TeamName, ProfileViewModel creator)
        {
            this.Manager = creator;
            this.Workers = new ObservableCollection<ProfileViewModel>();
            Team = new Team(TeamName, creator.ID);
            ToTeamManagerPageCommand = new Command(ToTeamManagerPage);
            AddWorkerCommand = new Command(AddWorkerAsync);
            //GetWorkersFromDB();
        }
        public TeamViewModel(Team team)
        {
            this.Manager = new ProfileViewModel(Database.Database.GetUserFromId(team.ManagerID));
            this.Workers = new ObservableCollection<ProfileViewModel>();
            foreach (var item in team.WorkersID)
            {
                this.Workers.Add(new ProfileViewModel(Database.Database.GetUserFromId(item)));
            }
            this.Team = team;
            ToTeamManagerPageCommand = new Command(ToTeamManagerPage);
            AddWorkerCommand = new Command(AddWorkerAsync);
            //GetWorkersFromDB();
        }

        private async void AddWorkerAsync(object obj)
        {
            string UserName = await Utils.InputDialog.InputBox(NavigationUtil.Navigation);
            User user = Database.Database.GetUser(UserName);
            if (user == null)
            {
                Toast.ShowToast("Error", "User not found!");
                return;
            }
            BsonDocument bson = new BsonDocument();
            Debug.WriteLine(Team.ID);
            bson.Add("TeamId", Team.ID);
            Debug.WriteLine(bson.GetValue("TeamId"));
            Debug.WriteLine(bson.GetValue("TeamId").AsObjectId);
            bson.Add("TeamName", Team.Name);
            bson.Add("UserName", Auth.CurrentUser.Name);
            Notification notification = new Notification
            {
                From = Auth.CurrentUser.ID,
                To = user.ID,
                Type = NotificationType.InviteToTeam,
                Line = bson 
            };
            Database.Database.AddNotification(ref notification);
            Toast.ShowToast("Complete!", "invite has been sended");
        }

        private void GetWorkersFromDB(Team team)
        {

        }

        TeamViewModel(ProfileViewModel creator, ObservableCollection<ProfileViewModel> workers)
        {
            this.Manager = creator;
            this.Workers = workers;
        }

        public ObjectId ID
        {
            get { return Team.ID; }
            set
            {
                if (Team.ID != value)
                {
                    Team.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string TeamManagerName
        {
            get
            {
                return Manager.Name;
            }
            set
            {
                if (Manager.Name != value)
                {
                    Manager.Name = value;
                    OnPropertyChanged("TeamManagerName");
                }
            }
        }
        public string Name
        {
            get
            {
                return Team.Name;
            }
            set
            {
                if (Team.Name != value)
                {
                    Team.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Role
        {
            get
            {
                if (Auth.CurrentUser.ID == Manager.ID) return "Project Manager";
                else return "Worker";
            }
            //set
            //{
            //    if (Team.Name != value)
            //    {
            //        Team.Name = value;
            //        OnPropertyChanged("Name");
            //    }
            //}
        }


        public ProfileViewModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = null;
                    OnPropertyChanged("SelectedWorker");
                    CurrentWorker = value;
                    NavigationUtil.Navigation.PushAsync(new ProfilePage(CurrentWorker));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void ToTeamManagerPage()
        {
            NavigationUtil.Navigation.PushAsync(new ProfilePage(Manager));
        }
    }
}
