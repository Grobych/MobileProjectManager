using MobileProjectManager.Models;
using MobileProjectManager.Views;
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
    public class TeamListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TeamViewModel> TeamList { get; set; }
        TeamViewModel selectedTeam;
        public TeamViewModel currentTeam { get; set; }
        public ProfileViewModel Creator { get; set; }
        public ICommand CreateTeamCommand { protected set; get; }

        public TeamListViewModel(ProfileViewModel profile)
        {
            TeamList = new ObservableCollection<TeamViewModel>();
            Creator = profile;
            CreateTeamCommand = new Command(CreateTeamAsync);
        }

        public TeamViewModel SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam != value)
                {
                    selectedTeam = null;
                    OnPropertyChanged("SelectedTeam");
                    currentTeam = value;
                    NavigationUtil.Navigation.PushAsync(new TeamPage(currentTeam));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public async void CreateTeamAsync()
        {
            string TeamName = await Utils.InputDialog.InputBox(NavigationUtil.Navigation);
            Debug.WriteLine(TeamName);
            TeamViewModel tvm = new TeamViewModel(TeamName, this.Creator);
            TeamList.Add(tvm);
            Team t = tvm.Team;
            Database.Database.AddTeamToDB(ref t);
        }
    }
}
