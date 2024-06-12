using System;
using System.ComponentModel;

namespace CookieClicker.View
{
    public class Statistiques : INotifyPropertyChanged
    {
        private int cookiesEnBanque;
        private int cookiesCuits;
        private int cookiesCuitsTotal;
        private int constructionsPossedees;
        private double cookiesParSeconde;
        private int cookiesParClic;
        private int clicsDeCookies;
        private int cookiesFaitsMain;
        private int clicsCookiesDores;
        private TimeSpan tempsJeu;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int CookiesEnBanque
        {
            get => cookiesEnBanque;
            set
            {
                cookiesEnBanque = value;
                OnPropertyChanged(nameof(CookiesEnBanque));
            }
        }

        public int CookiesCuits
        {
            get => cookiesCuits;
            set
            {
                cookiesCuits = value;
                OnPropertyChanged(nameof(CookiesCuits));
            }
        }

        public int CookiesCuitsTotal
        {
            get => cookiesCuitsTotal;
            set
            {
                cookiesCuitsTotal = value;
                OnPropertyChanged(nameof(CookiesCuitsTotal));
            }
        }

        public TimeSpan TempsJeu
        {
            get => tempsJeu;
            set
            {
                tempsJeu = value;
                OnPropertyChanged(nameof(TempsJeu));
            }
        }

        public int ConstructionsPossedees
        {
            get => constructionsPossedees;
            set
            {
                constructionsPossedees = value;
                OnPropertyChanged(nameof(ConstructionsPossedees));
            }
        }

        public double CookiesParSeconde
        {
            get => cookiesParSeconde;
            set
            {
                cookiesParSeconde = value;
                OnPropertyChanged(nameof(CookiesParSeconde));
            }
        }

        public int CookiesParClic
        {
            get => cookiesParClic;
            set
            {
                cookiesParClic = value;
                OnPropertyChanged(nameof(CookiesParClic));
            }
        }

        public int ClicsDeCookies
        {
            get => clicsDeCookies;
            set
            {
                clicsDeCookies = value;
                OnPropertyChanged(nameof(ClicsDeCookies));
            }
        }

        public int CookiesFaitsMain
        {
            get => cookiesFaitsMain;
            set
            {
                cookiesFaitsMain = value;
                OnPropertyChanged(nameof(CookiesFaitsMain));
            }
        }

        public int ClicsCookiesDores
        {
            get => clicsCookiesDores;
            set
            {
                clicsCookiesDores = value;
                OnPropertyChanged(nameof(ClicsCookiesDores));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
