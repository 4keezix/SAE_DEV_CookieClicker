using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace CookieClicker
{
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SteamButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://store.steampowered.com/app/1454400/Cookie_Clicker/",
                UseShellExecute = true
            });
        }

        private void PatreonButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.patreon.com/",
                UseShellExecute = true
            });
        }

        private void Realgame_Click(object sender, RoutedEventArgs e)
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
