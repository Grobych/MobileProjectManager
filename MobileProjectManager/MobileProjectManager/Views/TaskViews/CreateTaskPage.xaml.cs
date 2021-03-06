﻿using MobileProjectManager.ViewModels;
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
	public partial class CreateTaskPage : ContentPage
	{
        public TaskViewModel ViewModel { get; private set; }
        public CreateTaskPage(TaskViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}