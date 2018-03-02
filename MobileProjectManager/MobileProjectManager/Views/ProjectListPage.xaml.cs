using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileProjectManager.ViewModels;

namespace MobileProjectManager.Views
{
    public partial class ProjectListPage : ContentPage
    {
        ProjectListViewModel ViewModel { get; set; }
        public ProjectListPage()
        {
            InitializeComponent();
            BindingContext = new ProjectListViewModel();
        }
        public ProjectListPage(ProjectListViewModel plvm)
        {
            InitializeComponent();
            ViewModel = plvm;
            this.BindingContext = ViewModel;
        }
    }
}