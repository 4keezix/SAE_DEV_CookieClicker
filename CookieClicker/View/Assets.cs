using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CookieClicker.View
{

    class Assets
    {
        public int Cost { get; }
        public int CookiesPerSecond { get; }

        public Assets(int cost, int cookiesPerSecond)
        {
            Cost = cost;
            CookiesPerSecond = cookiesPerSecond;
        }
    }
}