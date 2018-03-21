using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    class NavigationUtil
    {
        public static INavigation Navigation { get; set; }

        public static Page getPreviousPage()
        {
            Page prevPage;
            int num = Application.Current.MainPage.Navigation.NavigationStack.Count -1;
            if (num > 0)
            {
                int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                prevPage = Application.Current.MainPage.Navigation.NavigationStack[num - 1];
                return prevPage;
            }
            else
            {
                return null;
            }
        }
        public static void Back()
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1) Navigation.PopAsync();
        }
        public static void BackToNewPage(Page page)
        {
            Navigation.InsertPageBefore(page, getPreviousPage());
            Navigation.RemovePage(getPreviousPage());
            Navigation.PopAsync();
        }
}
}
