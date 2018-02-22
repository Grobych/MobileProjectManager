using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileProjectManager.ViewModels;

namespace MobileProjectManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectEditPage : ContentPage
	{
        public ProjectViewModel ViewModel { get; private set; }
        public ProjectEditPage(ProjectViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}