using MobileProjectManager.Models;
using MobileProjectManager.ViewModels.Utils;
using MobileProjectManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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



        public TeamViewModel(string TeamName, ProfileViewModel creator)
        {
            this.Manager = creator;
            this.Workers = new ObservableCollection<ProfileViewModel>();
            Team = new Team(TeamName, creator.ID);
            ToTeamManagerPageCommand = new Command(ToTeamManagerPage);
            //GetWorkersFromDB();
        }

        private void GetWorkersFromDB(Team team)
        {

        }

        TeamViewModel(ProfileViewModel creator, ObservableCollection<ProfileViewModel> workers)
        {
            this.Manager = creator;
            this.Workers = workers;
        }

        TeamViewModel(Team team)
        {
            User Manager = Database.Database.GetUser(team.ManagerID);
            this.Manager = new ProfileViewModel(Manager);
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
