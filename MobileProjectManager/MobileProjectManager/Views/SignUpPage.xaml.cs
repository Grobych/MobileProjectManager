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
	public partial class SignUpPage : ContentPage
	{
        public LoginViewModel ViewModel { get; private set; }

        public SignUpPage(LoginViewModel lvm)
        {
            InitializeComponent();
            ViewModel = lvm;
            this.BindingContext = ViewModel;
        }
    }
}