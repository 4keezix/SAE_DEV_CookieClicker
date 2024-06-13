using System.Windows.Controls;
using CookieClicker.View;

namespace CookieClicker
{
    public partial class StatistiquesWindow : Page
    {
        private readonly Statistiques stats;

        public StatistiquesWindow(Statistiques statistiques)
        {
            InitializeComponent();
            stats = statistiques;
            DataContext = stats;
        }

        public void UpdateStatistics()
        {
            CookiesEnBanqueText.Text = stats.CookiesEnBanque.ToString();
            CookiesCuitsText.Text = stats.CookiesCuits.ToString();
            CookiesCuitsTotalText.Text = stats.CookiesCuitsTotal.ToString();
            TempsJeuText.Text = stats.TempsJeu.ToString(@"dd\:hh\:mm\:ss");
            ConstructionsPossedeesText.Text = stats.ConstructionsPossedees.ToString();
            CookiesParSecondeText.Text = stats.CookiesParSeconde.ToString("F2");
            CookiesParClicText.Text = stats.CookiesParClic.ToString();
            ClicsDeCookiesText.Text = stats.ClicsDeCookies.ToString();
            CookiesFaitsMainText.Text = stats.CookiesFaitsMain.ToString();
            ClicsCookiesDoresText.Text = stats.ClicsCookiesDores.ToString();
        }
    }
}
