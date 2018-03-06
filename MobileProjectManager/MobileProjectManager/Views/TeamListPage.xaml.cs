using MobileProjectManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProjectManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamListPage : ContentPage
	{
        TeamListViewModel ViewModel { get; set; }
        //public TeamListPage()
        //{
        //    InitializeComponent();
        //    BindingContext = new TeamListViewModel();
        //}
        public TeamListPage(TeamListViewModel plvm)
        {
            InitializeComponent();
            ViewModel = plvm;
            this.BindingContext = ViewModel;
        }
    }
}