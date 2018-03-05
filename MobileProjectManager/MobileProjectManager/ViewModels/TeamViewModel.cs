using MobileProjectManager.Models;
using MobileProjectManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MobileProjectManager.ViewModels
{
    class TeamViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel Manager { get; set; }
        public ObservableCollection<ProfileViewModel> Workers { get; set; }

        ProfileViewModel selectedWorker;
        public ProfileViewModel CurrentWorker { get; set; }

        TeamViewModel(ProfileViewModel creator)
        {
            this.Manager = creator;
            this.Workers = new ObservableCollection<ProfileViewModel>();
            GetWorkersFromDB(this.Manager);
        }

        private void GetWorkersFromDB(ProfileViewModel manager)
        {
            Database.Database.CheckIsCommandManager();
        }

        TeamViewModel(ProfileViewModel creator, ObservableCollection<ProfileViewModel> workers)
        {
            this.Manager = creator;
            this.Workers = workers;
        }





        public ProfileViewModel SelectedUser
        {
            get { return selectedWorker; }
            set
            {
                if (selectedWorker != value)
                {
                    selectedWorker = null;
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
    }
}
