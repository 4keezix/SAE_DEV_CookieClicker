using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CookieClicker.View
{
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private void SteamButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://store.steampowered.com/app/1454400/Cookie_Clicker/",
                UseShellExecute = true
            });
        }

        private void PatreonButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.patreon.com/",
                UseShellExecute = true
            });
        }

        private void Realgame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://orteil.dashnet.org/cookieclicker/",
                UseShellExecute = true
            });
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
}
