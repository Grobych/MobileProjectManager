using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Linq;

namespace MobileProjectManager.ViewModels.Utils
{
    class NetConnect
    {
        // обработка изменения состояния подключения
        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckConnection();
        }
        // получаем состояние подключения
        public static bool CheckConnection()
        {
            if (CrossConnectivity.Current != null &&
                CrossConnectivity.Current.ConnectionTypes != null &&
                CrossConnectivity.Current.IsConnected == true)
            {
                ConnectionType connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                return CrossConnectivity.Current.IsConnected;
                //connectionDetailsLbl.Text = connectionType.ToString();
            }
            else
            {
                return false;
            }
        }
    }
}
