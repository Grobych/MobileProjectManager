using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(PhoneApp.iOS.IOSPhone))]
namespace PhoneApp.iOS
{
    public class IOSPhone : MobileProjectManager.ViewModels.Utils.IPhoneCall
    {
        public Task Call(string phoneNumber)
        {
            var nsurl = new NSUrl(new Uri($"tel:{phoneNumber}").AbsoluteUri);
            if (!string.IsNullOrWhiteSpace(phoneNumber) &&
                    UIApplication.SharedApplication.CanOpenUrl(nsurl))
            {
                UIApplication.SharedApplication.OpenUrl(nsurl);
            }
            return Task.FromResult(true);
        }
    }
}