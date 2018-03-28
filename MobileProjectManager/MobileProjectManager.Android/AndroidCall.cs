using Android.Content;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(PhoneApp.Droid.AndroidPhone))]
namespace PhoneApp.Droid
{
    public class AndroidPhone : MobileProjectManager.ViewModels.Utils.IPhoneCall
    {
        public Task Call(string phoneNumber)
        {
            var packageManager = Android.App.Application.Context.PackageManager;
            Android.Net.Uri telUri = Android.Net.Uri.Parse($"tel:{phoneNumber}");
            var callIntent = new Intent(Intent.ActionCall, telUri);

            callIntent.AddFlags(ActivityFlags.NewTask);
            // проверяем доступность
            var result = null != callIntent.ResolveActivity(packageManager);

            if (!string.IsNullOrWhiteSpace(phoneNumber) && result == true)
            {
                Android.App.Application.Context.StartActivity(callIntent);
            }

            return Task.FromResult(true);
        }
    }
}