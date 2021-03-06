﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using MobileProjectManager.Views;
using MobileProjectManager.ViewModels.Database;
using MobileProjectManager.Models;
using MobileProjectManager.ViewModels;
using Acr.UserDialogs;

namespace MobileProjectManager
{
	public partial class App : Application
	{
		public App ()
		{
            // TODO: add page's titles
            // TODO: add avatars
            Database.Connect();
            MainPage = new NavigationPage(new LoginPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
