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
using System.Diagnostics;


namespace CookieClicker
{
    using global::CookieClicker.View;


    public partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
        }

        private void MusicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = e.NewValue / 100;
            AudioPlay.SetBackgroundMusicVolume((float)volume);
        }


        private void ClickSoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AudioPlay.EnableClickSound = false;

        }

        private void ClickSoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AudioPlay.EnableClickSound = true;

        }

        private void BuySoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AudioPlay.EnableBuySound = false;

        }

        private void BuySoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AudioPlay.EnableBuySound = true;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MusicCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AudioPlay.StopBackgroundMusic();
        }

        private void MusicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AudioPlay.PlayBackgroundMusic();
        }

        private void ChangeBackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedBackground = (BackgroundComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedBackground != null)
            {
                Debug.WriteLine($"Selected background: {selectedBackground}");
                ((MainWindow)Application.Current.MainWindow).ChangeBackground(selectedBackground);
            }
            else
            {
                Debug.WriteLine("No background selected");
            }
        }

    }
}

