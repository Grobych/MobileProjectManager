using MobileProjectManager.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel() { Navigation = this.Navigation };
        }
    }

}