using MobileProjectManager.ViewModels;
using Xamarin.Forms;

namespace MobileProjectManager.Views
{
    public partial class ProjectPage : ContentPage
    {
        public ProjectViewModel ViewModel { get; private set; }
        public ProjectPage(ProjectViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}