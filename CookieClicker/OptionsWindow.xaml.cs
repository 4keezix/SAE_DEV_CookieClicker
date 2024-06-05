using CookieClicker.View;
using NAudio.Gui;
using System.Windows;
using System.Windows.Controls;

namespace CookieClicker
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();

            // Initialiser les contrôles
            VolumeSlider.ValueChanged += VolumeSlider_ValueChanged;
            ChangeSoundButton.Click += ChangeSoundButton_Click;
            CloseButton.Click += CloseButton_Click;
        }

        // Gestion du bouton de fermeture
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Gestion du changement de volume
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = VolumeSlider.Value;
            // Ajuster le volume de l'application
            //AudioPlay.SetVolume(volume);
        }

        // Gestion du changement de son
                // Gestion du changement de son
        private void ChangeSoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SoundTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedSound = selectedItem.Content.ToString();
                string soundFilePath = SelectSoundFile(); // Méthode pour obtenir le chemin du fichier son

                switch (selectedSound)
                {
                    case "Son du clic de cookie":
                        // Changer le son du clic de cookie
                        AudioPlay.PlayClickSound();
                        break;

                    case "Son d'achat d'atout":
                        // Changer le son d'achat d'atout
                        AudioPlay.BuyingSongs();
                        break;

                    case "Son de revente":
                        // Changer le son de revente
                        AudioPlay.SellingSongs();
                        break;
                }
            }
        }

        // Méthode pour ouvrir une boîte de dialogue de sélection de fichier et obtenir le chemin du fichier son
        private string SelectSoundFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Fichiers audio (*.wav;*.mp3)|*.wav;*.mp3"
            };

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                return openFileDialog.FileName;
            }
            return string.Empty;
        }
    }
}
