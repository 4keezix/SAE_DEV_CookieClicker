using CookieClicker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookieClicker
{
    /// <summary>
    /// Logique d'interaction pour HeirtagePage.xaml
    /// </summary>
    public partial class HeirtagePage : Page
    {
        private GameState currentState;
        public HeirtagePage()
            {
                InitializeComponent();
                currentState = GameState.HeritagePageOpen;

        }

        private void AscendButton_Click(object sender, RoutedEventArgs e)
        {
            AscendPopup.IsOpen = true;
        }

        private void ConfirmAscend_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Ascend();
            mainWindow.ChangeState(GameState.Playing);
        }


        private void CancelAscend_Click(object sender, RoutedEventArgs e)
        {
            AscendPopup.IsOpen = false;
        }
    }
}