using MobileProjectManager.Models;
using MobileProjectManager.Views;
using MongoDB.Bson;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MobileProjectManager.ViewModels.Utils;

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

        public LoginViewModel()
        {
            if (!Utils.NetConnect.CheckConnection()) Toast.ShowToast("Connection Error", "Check your connection to the Network");
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
            if (!Utils.NetConnect.CheckConnection())
            {
                Toast.ShowToast("Connection Error", "Check your connection to the Network");
                return;
            }

            bool Res = Database.Database.GetUser(ref currentUser);
            if (!Res)
            {
                Utils.Toast.ShowToast("Login Failed", "Please, check you login and password");
            }
            else
            {
                Auth.CurrentUser = currentUser;
                Auth.CurrentUser.IsOnline = true;
                NavigationUtil.Navigation.PushAsync(new ProfilePage(new ProfileViewModel(Auth.CurrentUser)));
            }
        }
        public void SignUP(object lvm)
        {
            LoginViewModel viewModel = lvm as LoginViewModel;
            if (!Database.Database.CheckLogin(viewModel.currentUser))
            {
                Database.Database.AddUser(ref viewModel.currentUser);
                currentUser.IsOnline = true;
                Utils.Toast.ShowToast("SignUp Success", "SignUP has completed succesfully");
                NavigationUtil.Navigation.PopAsync();
            }
            else
            {
                Utils.Toast.ShowToast("SignUp Failed", "There is another user with this username in system");
            }
        }
        public void Logout()
        {
            currentUser.IsOnline = false;
            NavigationUtil.Navigation.PopToRootAsync();
        }
        public void ToSignUp()
        {
            NavigationUtil.Navigation.PushAsync(new SignUpPage(this));
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
        public string Number
        {
            get { return currentUser.Number; }
            set
            {
                if (currentUser.Number != value)
                {
                    currentUser.Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }
    }
}
