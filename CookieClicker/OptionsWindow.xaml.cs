using System.Windows;

namespace CookieClicker
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            this.Width = Application.Current.MainWindow.Width;
            this.Height = Application.Current.MainWindow.Height;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
