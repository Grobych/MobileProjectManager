using MobileProjectManager.ViewModels;
using Xamarin.Forms;

namespace MobileProjectManager.Views
{
    public partial class ProjectCreatePage : ContentPage
    {
        public ProjectViewModel ViewModel { get; private set; }
        public ProjectCreatePage(ProjectViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}