using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CookieClicker.View;
using WpfAnimatedGif;

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

        private bool isStatsPageOpen = false;
        private bool isOptionPageOpen = false;
        private bool isInfoPageOpen = false;
        private OptionsPage? optionsPage;
        private StatistiquesWindow statsPage;
        private Statistiques stats;

        private const int cursorsPerCircle = 12; // Nombre de curseurs par cercle
        private const double baseRadius = 100; // Rayon de base pour le premier cercle
        private const double radiusIncrement = 40; // Incrément de rayon pour chaque cercle supplémentaire
        private const double baseCursorSize = 20; // Taille de base du curseur
        private int cursorUpgradePrice = 100;
        private int cursorLevel = 1;
        private int grandmaUpgradePrice = 300;
        private int grandmaLevel = 1;
        private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(1) };
        private readonly DispatcherTimer goldenCookieTimer = new() { Interval = TimeSpan.FromSeconds(new Random().Next(60, 180)) };
        private readonly DispatcherTimer textChangeTimer = new() { Interval = TimeSpan.FromMinutes(1) };

        private readonly ImageItem imageItem;
        private Advertising? advertising;

        private int item1Count = 0;
        private int item2Count = 0;
        private int item3Count = 0;
        private int item4Count = 0;

        private readonly string[] textMessages = { "Cliquez-moi", "Bonjour", "Nouveau message", "Continuez à cliquer", "Vous êtes génial" };
        private int currentTextIndex = 0;

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
            stats = new Statistiques();

            // Garantir le fait que le nom et le nombre de cookie soit au premier plan
            Panel.SetZIndex(BakeryNameTextBlock, 10);
            Panel.SetZIndex(CookieCountText, 10);

            imageItem = new ImageItem(SpecialImage, SpecialImage3, SpecialImage4);

            statsPage = new StatistiquesWindow(stats);

            // Initialisation des items administrateur
            GrandmaItem = new Assets(100, 1); // Remplacez ces valeurs par les coûts et cookies par seconde réels
            CursorItem = new Assets(50, 1);
            FarmItem = new Assets(500, 10);
            MineItem = new Assets(1000, 20);

            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();

            timer.Tick += Timer_Tick;
            timer.Start();

            goldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            goldenCookieTimer.Start();

            // Pour le développement, afficher le bouton Admin
            AdminButton.Visibility = Visibility.Visible;

            // Démarrer la musique de fond
            AudioPlay.PlayBackgroundMusic();

            textChangeTimer.Tick += TextChangeTimer_Tick;
            textChangeTimer.Start();


            var imagePath = "pack://application:,,,/Images/oasis.gif";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imagePath);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(SlideshowImage, image);
        }

        private void SlideshowImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://oasis-sirop.com") { UseShellExecute = true });
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            double totalCookiesPerSecond = CalculateTotalCookiesPerSecond();
            cookie.AddCookiesFromTimer((int)totalCookiesPerSecond);

            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();
            UpdateStats();
        }

        private void GoldenCookieTimer_Tick(object? sender, EventArgs e)
        {
            goldenCookie = new GoldenCookie(GoldenCookieCanvas, cookie);
            goldenCookie.Spawn();

            goldenCookieTimer.Interval = TimeSpan.FromSeconds(new Random().Next(60, 180)); // Réinitialiser l'intervalle aléatoire
            goldenCookieTimer.Start();
        }

        private void TextChangeTimer_Tick(object? sender, EventArgs e)
        {
            UpdateDynamicTextBlock();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookie.AddCookie();
            UpdateCookieDisplay();
            UpdateButtonStates();
            UpdatePrices();
            AudioPlay.PlayClickSound();
            GenerateCookie();
            UpdateStats();
        }

        private void DynamicTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            AnimateTextBlock(DynamicTextBlock);
            currentTextIndex = (currentTextIndex + 1) % textMessages.Length;
            DynamicTextBlock.Text = textMessages[currentTextIndex];
        }

        private static void AnimateTextBlock(TextBlock textBlock)
        {
            DoubleAnimation animation = new()
            {
                From = 1.0,
                To = 1.5,
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                AutoReverse = true
            };

            ScaleTransform transform = new();
            textBlock.RenderTransform = transform;
            textBlock.RenderTransformOrigin = new Point(0.5, 0.5);

            transform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        private void UpdateDynamicTextBlock()
        {
            currentTextIndex = (currentTextIndex + 1) % textMessages.Length;
            DynamicTextBlock.Text = textMessages[currentTextIndex];
        }

        private double CalculateTotalCookiesPerSecond()
        {
            double cursorProduction = item1Count * GetCursorProductionPerSecond();
            double grandmaProduction = item2Count * GetGrandMaProductionPerSecond();
            double farmProduction = FarmItem.CookiesPerSecond * item3Count;
            double mineProduction = MineItem.CookiesPerSecond * item4Count;

            return cursorProduction + grandmaProduction + farmProduction + mineProduction;
        }

        private void UpdateCookieDisplay()
        {
            CookieCountText.Text = $"{cookie.Count} cookies";
            double cookiesPerSecond = CalculateTotalCookiesPerSecond();
            CookiesPerSecondText.Text = $"par seconde {cookiesPerSecond:F2}";
        }

        private void BuyItem1_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(item1, ref item1Count, Item1Count);
            AudioPlay.BuyingSongs();
            UpdateStats();
        }

        private void BuyItem2_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem2();
            BuyItem(item2, ref item2Count, Item2Count);
            AudioPlay.BuyingSongs();
            UpdateStats();
        }

        private void BuyItem3_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem3();
            BuyItem(item3, ref item3Count, Item3Count);
            AudioPlay.BuyingSongs();
            UpdateStats();
        }

        private void BuyItem4_Click(object sender, RoutedEventArgs e)
        {
            imageItem.ShowImageForItem4();
            BuyItem(item4, ref item4Count, Item4Count);
            AudioPlay.BuyingSongs();
            UpdateStats();
        }

        private void BuyItem(Assets item, ref int itemCount, TextBlock itemCountTextBlock)
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
            if (isOptionPageOpen)
            {
                mainFrame.Content = null;
                isOptionPageOpen = false;
            }
            else
            {
                mainFrame.Content = new OptionsPage();
                isOptionPageOpen = true;
            }
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
            UpdateStats();
        }

        private void SellItem2_Click(object sender, RoutedEventArgs e)
        {
            SellItem(item2, ref item2Count, Item2Count);
            AudioPlay.SellingSongs();
            UpdateStats();
        }

        private void SellItem3_Click(object sender, RoutedEventArgs e)
        {
            SellItem(item3, ref item3Count, Item3Count);
            AudioPlay.SellingSongs();
            UpdateStats();
        }

        private void SellItem4_Click(object sender, RoutedEventArgs e)
        {
            SellItem(item4, ref item4Count, Item4Count);
            AudioPlay.SellingSongs();
            UpdateStats();
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

        private double GetCursorProductionPerSecond()
        {
            return 0.4 * cursorLevel; // Production de cookies par curseur par seconde, à multiplier par le nombre de curseurs
        }

        private void AddCursor()
        {
            item1Count++;
            // Déterminer le cercle et l'angle pour le curseur actuel
            int circleIndex = (item1Count - 1) / cursorsPerCircle;
            int positionInCircle = (item1Count - 1) % cursorsPerCircle;

            double angle = 2 * Math.PI * positionInCircle / cursorsPerCircle;
            double radius = baseRadius + circleIndex * radiusIncrement;

            // Calculer les coordonnées x et y
            double x = radius * Math.Cos(angle) + CookieButton.ActualWidth / 2;
            double y = radius * Math.Sin(angle) + CookieButton.ActualHeight / 2;

            // Réduire la taille du curseur en fonction du cercle
            double cursorSize = baseCursorSize / (1 + circleIndex * 0.2);

            Image cursorImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/CookieClicker;component/Images/CursorUpgrade.png")),
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
                var cursorImagePaths = new Dictionary<int, string>
        {
            { 2, "/Images/CursorUpgrade.png" },
            { 3, "/Images/CursorUpgrade10.png" },
            { 4, "/Images/CursorUpgrade3.png" },
            { 5, "/Images/CursorUpgrade4.png" },
            { 6, "/Images/CursorUpgrade5.png" },
            { 7, "/Images/CursorUpgrade6.png" }
        };

                if (cursorImagePaths.TryGetValue(cursorLevel, out var imagePath))
                {
                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
            }

            CursorUpgradePriceText.Text = $"Prix: {cursorUpgradePrice} cookies";
        }

        private void CursorUpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookie.Count >= cursorUpgradePrice)
            {
                cookie.DeductCookies(cursorUpgradePrice);
                cursorLevel++;
                cursorUpgradePrice *= 5;
                UpdateCookieDisplay();
                UpdateButtonStates();
                UpdatePrices();
                UpdateCursorUpgradeButton();

                UpdateCursorProductionText();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

        private void UpdateCursorProductionText()
        {
            double cursorCookiesPerSecond = GetCursorProductionPerSecond();

            CursorProductionText.Text = $"Chaque curseur produit {cursorCookiesPerSecond} cookies par seconde";
        }

        private void UpdateGrandMaUpgradeButton()
        {
            var image = FindVisualChild<Image>(GrandmaUpgradeButton);
            if (image != null)
            {
                var grandmaImagePaths = new Dictionary<int, string>
        {
            { 2, "/Images/GrandMaUpgrade (2).png" },
            { 3, "/Images/GrandMaUpgrade (7).png" },
            { 4, "/Images/GrandMaUpgrade (3).png" },
            { 5, "/Images/GrandMaUpgrade (4).png" },
            { 6, "/Images/GrandMaUpgrade (5).png" },
            { 7, "/Images/GrandMaUpgrade (6).png" }
        };

                if (grandmaImagePaths.TryGetValue(grandmaLevel, out var imagePath))
                {
                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
            }

            GrandmaUpgradePriceText.Text = $"Prix: {grandmaUpgradePrice} cookies";
        }

        private void GrandmaUpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookie.Count >= grandmaUpgradePrice)
            {
                cookie.DeductCookies(grandmaUpgradePrice);
                grandmaLevel++;
                grandmaUpgradePrice *= 5;
                UpdateCookieDisplay();
                UpdateButtonStates();
                UpdatePrices();
                UpdateGrandMaUpgradeButton();

                UpdateGrandMaProductionText();
            }
            else
            {
                MessageBox.Show("Pas assez de cookies !");
            }
        }

        private void UpdateGrandMaProductionText()
        {
            double grandmaCookiesPerSecond = GetGrandMaProductionPerSecond();
            GrandmaProductionText.Text = $"Chaque grand-mère produit {grandmaCookiesPerSecond} cookies par seconde";
        }

        private double GetGrandMaProductionPerSecond()
        {
            return grandmaLevel * 0.4;
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

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
            return null!;
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            if (isStatsPageOpen)
            {
                mainFrame.Content = null; // Ferme la page des statistiques
                isStatsPageOpen = false;
            }
            else
            {
                UpdateStats(); // Mise à jour des statistiques avant d'afficher la page
                mainFrame.Navigate(statsPage);
                isStatsPageOpen = true;
            }
        }

        // Mise à jour des statistiques
        private void UpdateStats()
        {
            stats.CookiesEnBanque = cookie.Count;
            stats.CookiesCuits = cookie.TotalCookiesProduced; // Remplacez par le nombre total de cookies cuits si c'est différent
            stats.CookiesCuitsTotal = cookie.TotalCookiesProduced; // Exemple, remplacez par la bonne valeur
            stats.TempsJeu = DateTime.Now - cookie.StartTime;
            stats.ConstructionsPossedees = item1Count + item2Count + item3Count + item4Count;
            stats.CookiesParSeconde = CalculateTotalCookiesPerSecond();
            stats.CookiesParClic = cookie.CookiesPerClick;
            stats.ClicsDeCookies = cookie.TotalClicks;
            stats.CookiesFaitsMain = cookie.CookiesMadeByHand;
            stats.ClicsCookiesDores = cookie.GoldenCookieClicks; // Exemple, remplacez par la bonne valeur

            statsPage.UpdateStatistics();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (isInfoPageOpen)
            {
                mainFrame.Content = null;
                isInfoPageOpen = false;
            }
            else
            {
                mainFrame.Navigate(new InfoPage());
                isInfoPageOpen = true;
            }
        }

        private void GenerateCookie()
        {
            Image cookie = new()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/cookie.jpg")),
                Width = 50,
                Height = 50,
                Opacity = 0.8
            };

            Random rand = new Random();
            double startLeft = rand.Next(0, (int)CookieCanvas.ActualWidth - 50);
            Canvas.SetLeft(cookie, startLeft);
            Canvas.SetTop(cookie, -50);

            // Ajoutez le cookie au canvas à l'index 0 pour le mettre en arrière-plan
            CookieCanvas.Children.Insert(0, cookie);
            Panel.SetZIndex(cookie, -1); // Assurez-vous que le cookie a un ZIndex bas

            DoubleAnimation fallAnimation = new()
            {
                From = -50,
                To = CookieCanvas.ActualHeight,
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };

            fallAnimation.Completed += (s, a) => CookieCanvas.Children.Remove(cookie);

            cookie.BeginAnimation(Canvas.TopProperty, fallAnimation);
        }
    }
}
