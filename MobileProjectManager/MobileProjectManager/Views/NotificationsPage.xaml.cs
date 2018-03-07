using MobileProjectManager.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentPage
    {
        NotificationListViewModel ViewModel { get; set; }
        //public NotificationsPage()
        //{
        //    InitializeComponent();
        //    BindingContext = new NotificationListViewModel();
        //}
        public NotificationsPage(NotificationListViewModel plvm)
        {
            InitializeComponent();
            ViewModel = plvm;
            this.BindingContext = ViewModel;
        }
    }
}