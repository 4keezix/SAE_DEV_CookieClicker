namespace CookieClicker.View
{
    public class Cookie
    {
        public int Count { get; private set; }
        public int CookiesPerSecond { get; private set; }
        public int TotalCookiesProduced { get; private set; }
        public int CookiesMadeByHand { get; private set; }
        public int TotalClicks { get; private set; }
        public int GoldenCookieClicks { get; private set; }
        public DateTime StartTime { get; }
        public int CookiesPerClick { get; private set; }
        public int cursorUpgradePrice = 100;
        public int cursorLevel = 1;
        public int grandmaUpgradePrice = 300;
        public int grandmaLevel = 1;

        public Cookie()
        {
            Count = 0;
            CookiesPerSecond = 0;
            StartTime = DateTime.Now;
            CookiesPerClick = 1; 
        }

        public void AddCookie()
        {
            Count += CookiesPerClick;
            TotalCookiesProduced += CookiesPerClick;
            CookiesMadeByHand += CookiesPerClick;
            TotalClicks++;
        }

        public void AddCookies(int amount)
        {
            Count += amount;
            TotalCookiesProduced += amount;
        }

        public void DeductCookies(int amount)
        {
            Count -= amount;
        }

        public void AddCookiesPerSecond(int amount)
        {
            CookiesPerSecond += amount;
        }

        public void AddCookiesFromTimer(double cursorCookiesPerSecond)
        {
            int cookiesToAdd = CookiesPerSecond + (int)cursorCookiesPerSecond;
            Count += cookiesToAdd;
            TotalCookiesProduced += cookiesToAdd;
        }

        public void GoldenBonus(int amount)
        {
            Count += amount;
            TotalCookiesProduced += amount;
            GoldenCookieClicks++;
        }

        public void Reset()
        {
            Count = 0;
            TotalCookiesProduced = 0;
            CookiesPerSecond = 0;
            TotalClicks = 0;
            CookiesMadeByHand = 0;
            GoldenCookieClicks = 0;
            grandmaLevel = 0;
            cursorLevel = 0;
            grandmaUpgradePrice = 300;
            cursorUpgradePrice = 100;
    }
    }
}
