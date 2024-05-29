using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CookieClicker
{
    public partial class MainWindow : Window
    {
        private int cookieCount = 0;
        private int cookiesPerSecond = 0;
        private int item1Cost = 15;
        private int item1CookiesPerSecond = 1;
        private int grandmaCost = 115;
        private int grandmasCount = 0;

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            UpdateCookieDisplay();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            cookieCount += cookiesPerSecond;
            UpdateCookieDisplay();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookieCount++;
            UpdateCookieDisplay();
        }

        private void UpdateCookieDisplay()
        {
            CookieCountText.Text = $"{cookieCount} cookies";
            CookiesPerSecondText.Text = $"par seconde : {cookiesPerSecond}";
        }



        private void BuyItem1_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(15, 1); // Coût de 15 cookies, ajoute 1 cookie/sec
        }

        private void BuyItem2_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(100, 5); // Coût de 100 cookies, ajoute 5 cookies/sec
        }

        private void BuyItem3_Click(object sender, MouseButtonEventArgs e)
        {
            BuyItem(500, 10); // Coût de 500 cookies, ajoute 10 cookies/sec
        }

        private void BuyItem(int cost, int cookiesPerSecondIncrease)
        {
            if (cookieCount >= cost)
            {
                cookieCount -= cost;
                cookiesPerSecond += cookiesPerSecondIncrease;
                UpdateCookieDisplay();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

   


        private void BuyGrandma_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= grandmaCost)
            {
                cookieCount -= grandmaCost;
                cookiesPerSecond++; // Assume each grandma adds 1 cookie per second
                grandmasCount++;
                UpdateCookieDisplay();
                AddGrandma();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

        private void AddGrandma()
        {
            Image grandmaImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/GrandMère.png")),
                Width = 50,
                Height = 50,
                Margin = new Thickness(5)
            };
            GrandmaPanel.Children.Add(grandmaImage);
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
