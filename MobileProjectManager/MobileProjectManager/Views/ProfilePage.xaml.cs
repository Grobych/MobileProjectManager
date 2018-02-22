using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileProjectManager.ViewModels;

namespace MobileProjectManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public ProfileViewModel ViewModel { get; private set; }
		public ProfilePage (ProfileViewModel vm)
		{
			InitializeComponent ();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
	}
}