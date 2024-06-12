using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CookieClicker.View
{
    public class GoldenCookie
    {
        private readonly Canvas goldenCookieCanvas;
        private readonly Cookie cookie;
        private readonly Image goldenCookieImage;
        private readonly DispatcherTimer lifetimeTimer;

        public GoldenCookie(Canvas goldenCookieCanvas, Cookie cookie)
        {
            this.goldenCookieCanvas = goldenCookieCanvas;
            this.cookie = cookie;

            goldenCookieImage = new Image
            {
                Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Images/Golden_cookie.png") as ImageSource,
                Width = 76,
                Height = 76,
                Opacity = 0
            };

            goldenCookieImage.MouseLeftButtonDown += GoldenCookieImage_MouseLeftButtonDown;

            lifetimeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            lifetimeTimer.Tick += LifetimeTimer_Tick;
        }

        private void GoldenCookieImage_MouseLeftButtonDown(object? sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ApplyBonus();
            AudioPlay.PlayGoldenCookieSound(); // Jouer le son ici
            var position = e.GetPosition(goldenCookieCanvas);
            ShowBonusText(cookie.CookiesPerSecond * 10, position.X, position.Y); // Afficher le texte du bonus
            RemoveGoldenCookie();
        }

        private void LifetimeTimer_Tick(object? sender, EventArgs e)
        {
            RemoveGoldenCookie();
        }

        public void Spawn()
        {
            Random random = new Random();
            double maxLeft = goldenCookieCanvas.ActualWidth - goldenCookieImage.Width;
            double maxTop = goldenCookieCanvas.ActualHeight - goldenCookieImage.Height;

            double left = random.NextDouble() * maxLeft;
            double top = random.NextDouble() * maxTop;

            goldenCookieImage.SetValue(Canvas.LeftProperty, left);
            goldenCookieImage.SetValue(Canvas.TopProperty, top);

            goldenCookieCanvas.Children.Add(goldenCookieImage);

            FadeIn(goldenCookieImage, 3);
            lifetimeTimer.Start();
        }

        private void ApplyBonus()
        {
            int bonusCookies = cookie.CookiesPerSecond * 10;
            cookie.GoldenBonus(bonusCookies);
        }

        private void ShowBonusText(int bonusCookies, double left, double top)
        {
            TextBlock bonusText = new TextBlock
            {
                Text = $"+{bonusCookies}",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Yellow)
            };

            goldenCookieCanvas.Children.Add(bonusText);
            Canvas.SetLeft(bonusText, left);
            Canvas.SetTop(bonusText, top);

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2)
            };

            TranslateTransform translateTransform = new TranslateTransform();
            bonusText.RenderTransform = translateTransform;

            DoubleAnimation moveUpAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(2)
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Children.Add(moveUpAnimation);

            Storyboard.SetTarget(fadeOutAnimation, bonusText);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(moveUpAnimation, bonusText);
            Storyboard.SetTargetProperty(moveUpAnimation, new PropertyPath("RenderTransform.Y"));

            storyboard.Completed += (s, e) => goldenCookieCanvas.Children.Remove(bonusText);
            storyboard.Begin();
        }

        private void RemoveGoldenCookie()
        {
            FadeOut(goldenCookieImage, 0);
            lifetimeTimer.Stop();
        }

        private static void FadeIn(UIElement element, double durationInSeconds)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(durationInSeconds));
            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        private static void FadeOut(UIElement element, double durationInSeconds)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(durationInSeconds));
            fadeOutAnimation.Completed += (s, e) =>
            {
                var parent = VisualTreeHelper.GetParent(element) as Panel;
                parent?.Children.Remove(element);
            };
            element.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
    }
}
