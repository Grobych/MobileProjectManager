using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileProjectManager.ViewModels;

namespace MobileProjectManager.Views
{
    public partial class ProjectListPage : ContentPage
    {
        public ProjectListPage()
        {
            InitializeComponent();
            BindingContext = new ProjectListViewModel() { Navigation = this.Navigation };
        }
    }
}