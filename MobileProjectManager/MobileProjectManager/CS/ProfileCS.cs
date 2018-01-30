using Xamarin.Forms;

namespace MobileProjectManager
{
    public class ProfileCS : ContentPage
    {
        public ProfileCS()
        {
            Title = "Profile Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Profile data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}