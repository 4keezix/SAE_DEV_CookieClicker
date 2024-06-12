using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CookieClicker.View
{
    public class DynamicTextManager
    {
        private readonly TextBlock dynamicTextBlock;
        private readonly DispatcherTimer changeTextTimer;
        private readonly List<string> textOptions;
        private readonly Random random;

        public DynamicTextManager(TextBlock textBlock)
        {
            dynamicTextBlock = textBlock;
            random = new Random();
            textOptions = new List<string>
            {
                "Cliquez-moi",
                "Plus de cookies!",
                "Délicieux!",
                "Continuez à cliquer!",
                "Miam miam!"
            };

            dynamicTextBlock.MouseLeftButtonDown += DynamicTextBlock_Click;

            changeTextTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            changeTextTimer.Tick += (s, e) => ChangeText();
            changeTextTimer.Start();
        }

        public void DynamicTextBlock_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AnimateTextChange();
            ChangeText();
        }

        private void ChangeText()
        {
            int index = random.Next(textOptions.Count);
            dynamicTextBlock.Text = textOptions[index];
        }

        private void AnimateTextChange()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOut.Completed += (s, e) =>
            {
                ChangeText();
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                dynamicTextBlock.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };

            dynamicTextBlock.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
    }
}
