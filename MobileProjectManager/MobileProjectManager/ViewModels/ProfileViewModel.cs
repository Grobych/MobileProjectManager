using System.ComponentModel;

using MobileProjectManager.Models;

namespace MobileProjectManager.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User User { get; set; }

        public ProfileViewModel()
        {
            User = new User();
        }
      //  public ProfileViewModel(User user)
    //    {
  //          User = user;
//        }



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

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Name.Trim());
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
