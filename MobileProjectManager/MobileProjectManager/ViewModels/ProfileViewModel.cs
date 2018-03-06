﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using MobileProjectManager.Models;
using MongoDB.Bson;
using Xamarin.Forms;

using MobileProjectManager.ViewModels;
using MobileProjectManager.Views;

namespace MobileProjectManager.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        TeamListViewModel TeamListView { get; set; }
        public ProjectListViewModel ProjectListView { get; set; }
        public ICommand ToProjectListCommand { protected set; get; }
        public ICommand ToTeamListCommand { protected set; get; }


        public User User { get; set; }

        public ProfileViewModel(User user)
        {
            this.User = user;
            ProjectListView = new ProjectListViewModel();
            ToProjectListCommand = new Command(ToProjectList);
            ToTeamListCommand = new Command(ToTeamsList);
            TeamListView = new TeamListViewModel(this);
        }

        private void ToProjectList(object obj)
        {
            NavigationUtil.Navigation.PushAsync(new ProjectListPage(ProjectListView));
        }
        private void ToTeamsList(object obj)
        {
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

        //public byte[] Img
        //{
        //    get { return User.Img; }
        //    set
        //    {
        //        if (User.Img != value)
        //        {
        //            User.Img = value;
        //            OnPropertyChanged("Img");
        //        }
        //    }
        //}

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
