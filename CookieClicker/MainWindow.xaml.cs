using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CookieClicker.View;

namespace CookieClicker
{
    public partial class MainWindow : Window
    {
        private Cookie cookie;
        private Assets item1;
        private Assets item2;
        private Assets item3;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            cookie = new Cookie();
            item1 = new Assets(15, 1);
            item2 = new Assets(100, 5);
            item3 = new Assets(500, 10);

            UpdateCookieDisplay();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            cookie.AddCookiesFromTimer();
            UpdateCookieDisplay();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookie.AddCookie();
            UpdateCookieDisplay();
        }

        private void UpdateCookieDisplay()
        {
            CookieCountText.Text = $"{cookie.Count} cookies";
            CookiesPerSecondText.Text = $"par seconde : {cookie.CookiesPerSecond}";
        }

        private void BuyItem1_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(item1);
        }

        private void BuyItem2_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(item2);
        }

        private void BuyItem3_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(item3);
        }

        private void BuyItem(Assets item)
        {
            if (cookie.Count >= item.Cost)
            {
                cookie.DeductCookies(item.Cost);
                cookie.AddCookiesPerSecond(item.CookiesPerSecond);
                UpdateCookieDisplay();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

        private void BakeryNameTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BakeryNameTextBlock.Visibility = Visibility.Collapsed;
            BakeryNameTextBox.Visibility = Visibility.Visible;
            BakeryNameTextBox.Focus();
        }

        private void BakeryNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveBakeryName();
        }

        private void BakeryNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveBakeryName();
            }
        }

        private void SaveBakeryName()
        {
            if (string.IsNullOrWhiteSpace(BakeryNameTextBox.Text))
            {
                MessageBox.Show("Le nom de la pâtisserie ne peut pas être vide !");
                BakeryNameTextBox.Focus();
            }
            else
            {
                BakeryNameTextBlock.Text = BakeryNameTextBox.Text;
                BakeryNameTextBox.Visibility = Visibility.Collapsed;
                BakeryNameTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Owner = this; // Définit la fenêtre principale comme propriétaire de la fenêtre des options
            optionsWindow.ShowDialog(); // Affiche la fenêtre des options comme une fenêtre modale
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir quitter ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
