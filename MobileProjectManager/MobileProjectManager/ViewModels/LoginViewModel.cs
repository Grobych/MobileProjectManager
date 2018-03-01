using MobileProjectManager.Models;
using MobileProjectManager.Views;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


using MobileProjectManager.ViewModels.Database;
using Plugin.Toasts;

namespace MobileProjectManager.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public User currentUser = new User();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { protected set; get; }
        public ICommand ToSignUpPageCommand { protected set; get; }
        public ICommand SignUPCommand { protected set; get; }
        public ICommand LogoutCommand { protected set; get; }

        public INavigation Navigation { get; set; }



        public LoginViewModel()
        {
            currentUser = new User();
            LoginCommand = new Command(TryToLogin);
            SignUPCommand = new Command(SignUP);
            LogoutCommand = new Command(Logout);
            ToSignUpPageCommand = new Command(ToSignUp);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void TryToLogin()
        {

            //LoginViewModel viewModel = lvm as LoginViewModel;
            bool Res = Database.Database.GetUser(currentUser);
            if (!Res)
            {
                Utils.Toast.ShowToast("Login Failed", "Please, check you login and password", false);
            }
            else
            {
                currentUser.IsOnline = true;
                Navigation.PushAsync(new ProjectListPage());
            }
        }
        public void SignUP(object lvm)
        {
            LoginViewModel viewModel = lvm as LoginViewModel;
            if (!Database.Database.CheckLogin(viewModel.currentUser))
            {
                Database.Database.AddUser(viewModel.currentUser);
                currentUser.IsOnline = true;
                Utils.Toast.ShowToast("SignUp Success", "SignUP has completed succesfully", false);
                Navigation.PopAsync();
            }
            else
            {
                Utils.Toast.ShowToast("SignUp Failed", "There is another user with this username in system", false);
            }
        }
        public void Logout()
        {
            currentUser.IsOnline = false;
            Navigation.PopToRootAsync();
        }
        public void ToSignUp()
        {
            Navigation.PushAsync(new SignUpPage(this));
        }



        public string Name
        {
            get { return currentUser.Name; }
            set
            {
                if (currentUser.Name != value)
                {
                    currentUser.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public ObjectId ID
        {
            get { return currentUser.ID; }
            set
            {
                if (currentUser.ID != value)
                {
                    currentUser.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public bool IsOnline
        {
            get { return currentUser.IsOnline; }
            set
            {
                if (currentUser.IsOnline != value)
                {
                    currentUser.IsOnline = value;
                    OnPropertyChanged("IsOnline");
                }
            }
        }
        public string Email
        {
            get { return currentUser.Email; }
            set
            {
                if (currentUser.Email != value)
                {
                    currentUser.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Password
        {
            get { return currentUser.Password; }
            set
            {
                if (currentUser.Password != value)
                {
                    currentUser.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

    }
}
