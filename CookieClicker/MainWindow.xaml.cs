using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CookieClicker.View;

namespace CookieClicker
{
    public partial class MainWindow : Window
    {
        private readonly Cookie cookie;
        private readonly Assets item1;
        private readonly Assets item2;
        private readonly Assets item3;
        private readonly Assets item4;
        private readonly DispatcherTimer timer;

        private int item1Count = 0;
        private int item2Count = 0;
        private int item3Count = 0;
        private int item4Count = 0;

        public MainWindow()
        {
            InitializeComponent();
            cookie = new Cookie();
            item1 = new Assets(15, 1);
            item2 = new Assets(100, 5);
            item3 = new Assets(500, 10);
            item4 = new Assets(1000, 50);

            UpdateCookieDisplay();
            UpdateButtonStates();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            cookie.AddCookiesFromTimer();
            UpdateCookieDisplay();
            UpdateButtonStates();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookie.AddCookie();
            UpdateCookieDisplay();
            UpdateButtonStates();
        }

        private void UpdateCookieDisplay()
        {
            CookieCountText.Text = $"{cookie.Count} cookies";
            CookiesPerSecondText.Text = $"par seconde : {cookie.CookiesPerSecond}";
        }

        private void BuyItem1_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item1, ref item1Count, Item1Count, Item1Price, Compteur1);
        }

        private void BuyItem2_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item2, ref item2Count, Item2Count, Item2Price, Compteur2);
        }

        private void BuyItem3_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item3, ref item3Count, Item3Count, Item3Price, Compteur3);
        }

        private void BuyItem4_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item4, ref item4Count, Item4Count, Item4Price, Compteur4);
        }

        private void BuyItem(Assets item, ref int itemCount, TextBlock itemCountTextBlock, TextBlock itemPriceTextBlock, TextBlock itemCompteurTextBlock)
        {
            if (cookie.Count >= item.Cost)
            {
                cookie.DeductCookies(item.Cost);
                cookie.AddCookiesPerSecond(item.CookiesPerSecond);
                itemCount++;
                item.Count++;
                itemCountTextBlock.Text = itemCount.ToString();
                itemCompteurTextBlock.Text = item.Count.ToString();
                itemPriceTextBlock.Text = $"{item.Cost} cookies";
                UpdateCookieDisplay();
                UpdateButtonStates();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

        private void UpdateButtonStates()
        {
            BuyItem1Grid.IsEnabled = cookie.Count >= item1.Cost;
            BuyItem2Grid.IsEnabled = cookie.Count >= item2.Cost;
            BuyItem3Grid.IsEnabled = cookie.Count >= item3.Cost;
            BuyItem4Grid.IsEnabled = cookie.Count >= item4.Cost;

            UpdateItemOpacity(BuyItem1Grid, cookie.Count >= item1.Cost, Item1Price);
            UpdateItemOpacity(BuyItem2Grid, cookie.Count >= item2.Cost, Item2Price);
            UpdateItemOpacity(BuyItem3Grid, cookie.Count >= item3.Cost, Item3Price);
            UpdateItemOpacity(BuyItem4Grid, cookie.Count >= item4.Cost, Item4Price);
        }

        private void UpdateItemOpacity(Grid itemGrid, bool isEnabled, TextBlock itemPriceTextBlock)
        {
            itemGrid.Opacity = isEnabled ? 1.0 : 0.5;
            itemPriceTextBlock.Foreground = isEnabled ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
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
