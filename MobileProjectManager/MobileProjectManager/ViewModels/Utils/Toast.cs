using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels.Utils
{
    public class Toast
    {
        public static void ShowToast(string Title, string Text)
        {
            var options = new NotificationOptions()
            {
                Title = Title,
                Description = Text,
                IsClickable = false // Set to true if you want the result Clicked to come back (if the user clicks it)
            };
            var notification = DependencyService.Get<IToastNotificator>();
            var result = notification.Notify(options);
        }
    }
}
