using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        private GoldenCookie? goldenCookie;
        private int cursorCount = 0;
        private const int cursorsPerCircle = 12; // Nombre de curseurs par cercle
        private const double baseRadius = 100; // Rayon de base pour le premier cercle
        private const double radiusIncrement = 40; // Incrément de rayon pour chaque cercle supplémentaire
        private const double baseCursorSize = 20; // Taille de base du curseur
        private int cursorUpgradePrice = 100;
        private int cursorLevel = 1;
        private readonly DispatcherTimer timer;
        private readonly DispatcherTimer goldenCookieTimer;

        private readonly ImageItem imageItem;

        private int item1Count = 0;
        private int item2Count = 0;
        private int item3Count = 0;
        private int item4Count = 0;

        // Déclaration des items administrateur
        public Assets GrandmaItem { get; private set; }
        public Assets CursorItem { get; private set; }
        public Assets FarmItem { get; private set; }
        public Assets MineItem { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            cookie = new Cookie();
            item1 = new Assets(15, 1);
            item2 = new Assets(100, 5);
            item3 = new Assets(500, 10);
            item4 = new Assets(1000, 50);

            imageItem = new ImageItem(SpecialImage, SpecialImage3, SpecialImage4);

            // Initialisation des items administrateur
            GrandmaItem = new Assets(100, 1); // Remplacez ces valeurs par les coûts et cookies par seconde réels
            CursorItem = new Assets(50, 1);
            FarmItem = new Assets(500, 10);
            MineItem = new Assets(1000, 20);

            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            goldenCookieTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(new Random().Next(60, 180)) // Intervalle aléatoire entre 1 et 3 minutes
            };
            goldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            goldenCookieTimer.Start();

            // Pour le développement, afficher le bouton Admin
            AdminButton.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            int cursorCookiesPerSecond = cursorCount * cursorLevel;
            cookie.AddCookiesFromTimer(cursorCookiesPerSecond);

            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();
        }

        private void GoldenCookieTimer_Tick(object? sender, EventArgs e)
        {
            goldenCookie = new GoldenCookie(GoldenCookieCanvas, cookie);
            goldenCookie.Spawn();

            goldenCookieTimer.Interval = TimeSpan.FromSeconds(new Random().Next(60, 180)); // Réinitialiser l'intervalle aléatoire
            goldenCookieTimer.Start();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookie.AddCookie();
            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();
            AudioPlay.PlayClickSound();
        }

        private void UpdateCookieDisplay()
        {
            CookieCountText.Text = $"{cookie.Count} cookies";
            CookiesPerSecondText.Text = $"par seconde : {cookie.CookiesPerSecond}";
        }

        private void BuyItem1_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item1, ref item1Count, Item1Count, Item1Price);
            //AddCursor();
            AudioPlay.BuyingSongs();
        }

        private void BuyItem2_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem2();
            BuyItem(item2, ref item2Count, Item2Count, Item2Price);
            AudioPlay.BuyingSongs();
        }

        private void BuyItem3_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem3();
            BuyItem(item3, ref item3Count, Item3Count, Item3Price);
            AudioPlay.BuyingSongs();
        }

        private void BuyItem4_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem4();
            BuyItem(item4, ref item4Count, Item4Count, Item4Price);
            AudioPlay.BuyingSongs();
        }

        private void BuyItem(Assets item, ref int itemCount, TextBlock itemCountTextBlock, TextBlock itemPriceTextBlock)
        {
            if (cookie.Count >= item.Cost)
            {
                cookie.DeductCookies(item.Cost);
                cookie.AddCookiesPerSecond(item.CookiesPerSecond);
                itemCount++;
                itemCountTextBlock.Text = itemCount.ToString();
                item.IncreaseCost();
                UpdateCookieDisplay();
                UpdateButtonStates();
                UpdatePrices();
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

            BuyItem1Grid.Opacity = BuyItem1Grid.IsEnabled ? 1.0 : 0.5;
            BuyItem2Grid.Opacity = BuyItem2Grid.IsEnabled ? 1.0 : 0.5;
            BuyItem3Grid.Opacity = BuyItem3Grid.IsEnabled ? 1.0 : 0.5;
            BuyItem4Grid.Opacity = BuyItem4Grid.IsEnabled ? 1.0 : 0.5;
        }

        private void UpdatePrices()
        {
            UpdatePrice(Item1Price, item1.Cost);
            UpdatePrice(Item2Price, item2.Cost);
            UpdatePrice(Item3Price, item3.Cost);
            UpdatePrice(Item4Price, item4.Cost);
        }

        private void UpdatePrice(TextBlock priceTextBlock, int price)
        {
            priceTextBlock.Text = $"{price} cookies";
            priceTextBlock.Foreground = cookie.Count >= price ? Brushes.LightGreen : Brushes.Red;
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
            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir quitter ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void SellItem(Assets item, ref int itemCount, TextBlock itemCountTextBlock)
        {
            if (itemCount > 0)
            {
                itemCount--;
                itemCountTextBlock.Text = itemCount.ToString();
                cookie.AddCookies(item.Cost / 2);  // Revendre à moitié du prix actuel
                cookie.AddCookiesPerSecond(-item.CookiesPerSecond);
                item.DecreaseCost();
                UpdateCookieDisplay();
                UpdateButtonStates();
                UpdatePrices();
            }
            else
            {
                MessageBox.Show("Vous n'avez pas cet article !");
            }
        }

        private void SellItem1_Click(object sender, RoutedEventArgs e)
        {
            SellItem(item1, ref item1Count, Item1Count);
            AudioPlay.SellingSongs();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow(this);
            adminWindow.Owner = this;
            adminWindow.Show();
        }

        public void SpawnGoldenCookie()
        {
            goldenCookie = new GoldenCookie(GoldenCookieCanvas, cookie);
            goldenCookie.Spawn();
        }

        public void AddCookies(int amount)
        {
            cookie.AddCookies(amount);
            UpdateCookieDisplay();
        }

        public void AddItems(Assets item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                item.Buy(cookie);
            }
            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();
        }

        private void AddCursor()
        {
            cursorCount++;

            // Déterminer le cercle et l'angle pour le curseur actuel
            int circleIndex = (cursorCount - 1) / cursorsPerCircle;
            int positionInCircle = (cursorCount - 1) % cursorsPerCircle;

            double angle = 2 * Math.PI * positionInCircle / cursorsPerCircle;
            double radius = baseRadius + circleIndex * radiusIncrement;

            // Calculer les coordonnées x et y
            double x = radius * Math.Cos(angle) + CookieButton.ActualWidth / 2;
            double y = radius * Math.Sin(angle) + CookieButton.ActualHeight / 2;

            // Réduire la taille du curseur en fonction du cercle
            double cursorSize = baseCursorSize / (1 + circleIndex * 0.2);

            Image cursorImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/CookieClicker;component/Images/sprite.png")),
                Width = cursorSize,
                Height = cursorSize
            };

            Canvas.SetLeft(cursorImage, x);
            Canvas.SetTop(cursorImage, y);

            CursorCanvas.Children.Add(cursorImage);
        }

        private void UpdateCursorUpgradeButton()
        {
            var image = FindVisualChild<Image>(CursorUpgradeButton);
            if (image != null)
            {
                // Dictionnaire pour mapper les niveaux de curseur aux chemins d'image
                var cursorImagePaths = new Dictionary<int, string>
        {
            { 1, "/Images/CursorUpgrade1.png" },
            { 3, "/Images/CursorUpgrade2.png" },
            { 4, "/Images/CursorUpgrade3.png" },
            { 5, "/Images/CursorUpgrade4.png" },
            { 6, "/Images/CursorUpgrade5.png" },
            { 7, "/Images/CursorUpgrade6.png" }
        };

                // Vérifiez si le niveau du curseur a une image associée et mettez à jour l'image
                if (cursorImagePaths.TryGetValue(cursorLevel, out var imagePath))
                {
                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
            }

            CursorUpgradePriceText.Text = $"Prix: {cursorUpgradePrice} cookies";
        }


        private void CursorUpgradeButton_Click(object sender, RoutedEventArgs e)
        {

                cookie.DeductCookies(cursorUpgradePrice);
                cursorLevel++; 
                cursorUpgradePrice *= 2; 
                UpdateCookieDisplay();
                UpdateButtonStates();
                UpdatePrices();
                UpdateCursorUpgradeButton();
            
           
        }

        // Méthode utilitaire pour trouver un élément visuel à l'intérieur d'un contrôle parent
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }



    }
}
