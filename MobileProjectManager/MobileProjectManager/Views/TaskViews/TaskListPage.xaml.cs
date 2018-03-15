using MobileProjectManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProjectManager.Views.TaskViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        TaskListViewModel ViewModel { get; set; }
        public TaskListPage(TaskListViewModel tlvm)
        {
            InitializeComponent();
            ViewModel = tlvm;
            this.BindingContext = ViewModel;
        }
    }
}