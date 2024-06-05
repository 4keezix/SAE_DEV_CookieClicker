using System.Windows;

namespace CookieClicker
{
    public partial class AdminWindow : Window
    {
        private readonly MainWindow mainWindow;

        public AdminWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void SpawnGoldenCookie_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SpawnGoldenCookie();
        }

        private void AddCookies_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CookiesToAddTextBox.Text, out int cookies))
            {
                mainWindow.AddCookies(cookies);
            }
        }

        private void Add10000Cookies_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.AddCookies(10000);
        }

        private void AddGrandmas_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GrandmasToAddTextBox.Text, out int grandmas))
            {
                mainWindow.AddItems(mainWindow.GrandmaItem, grandmas);
            }
        }

        private void AddCursors_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CursorsToAddTextBox.Text, out int cursors))
            {
                mainWindow.AddItems(mainWindow.CursorItem, cursors);
            }
        }

        private void AddFarms_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(FarmsToAddTextBox.Text, out int farms))
            {
                mainWindow.AddItems(mainWindow.FarmItem, farms);
            }
        }

        private void AddMines_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MinesToAddTextBox.Text, out int mines))
            {
                mainWindow.AddItems(mainWindow.MineItem, mines);
            }
        }
    }
}
