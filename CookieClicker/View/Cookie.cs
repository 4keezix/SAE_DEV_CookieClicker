using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieClicker.View
{
    public class Cookie
    {
        public int Count { get; private set; }
        public int CookiesPerSecond { get; private set; }

        public Cookie()
        {
            Count = 0;
            CookiesPerSecond = 0;
        }

        public void AddCookie()
        {
            Count++;
        }

        public void AddCookiesPerSecond(int amount)
        {
            CookiesPerSecond += amount;
        }

        public void DeductCookies(int amount)
        {
            Count -= amount;
        }

        public void AddCookiesFromTimer()
        {
            Count += CookiesPerSecond;
        }
    }
}