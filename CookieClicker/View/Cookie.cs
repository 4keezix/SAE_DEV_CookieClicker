namespace CookieClicker.View
{
    public class Cookie
    {
        public int Count { get; private set; }
        public int CookiesPerSecond { get; private set; }
        public int TotalCookiesProduced { get; private set; }
        public int CookiesMadeByHand { get; private set; }
        public int TotalClicks { get; private set; }
        public double ProductionMultiplier { get; private set; }
        public int GoldenCookieClicks { get; private set; }
        public int item1Count = 0;
        public int item2Count = 0;
        public int item3Count = 0;
        public int item4Count = 0;
        public DateTime StartTime { get; }
        public int CookiesPerClick { get; private set; }
        public int cursorUpgradePrice = GameConstants.GrandmaLevel;
        public int cursorLevel = GameConstants.GrandmaLevel;
        public int grandmaUpgradePrice = GameConstants.GrandmaUpgradePrice;
        public int grandmaLevel = GameConstants.GrandmaLevel;


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
            setItem1Count(0);
            setItem2Count(0);
            setItem3Count(0);
            setItem4Count(0);
            setCursorUpgradePrice(100);
            setCursorLevel(0);
            setGrandmaUpgradePrice(300);
            setGrandmaLevel(0);
        }
        public void Ascend()
        {
           Count = 0;
           



        }

        public int getItem1Count()
        {
            return item1Count;
        }

        public int getItem2Count()
        {
            return item2Count;
        }

        public int getItem3Count()
        {
            return item3Count;
        }

        public int getItem4Count()
        {
            return item4Count;
        }

        public int getCursorUpgradePrice()
        {
            return cursorUpgradePrice;
        }

        public int getCursorLevel()
        {
            return cursorLevel;
        }

        public int getGrandmaUpgradePrice()
        {
            return grandmaUpgradePrice;
        }

        public int getGrandmaLevel()
        {
            return grandmaLevel;
        }

        // Setters with private access
        private void setItem1Count(int item1Count)
        {
            this.item1Count = item1Count;
        }

        private void setItem2Count(int item2Count)
        {
            this.item2Count = item2Count;
        }

        private void setItem3Count(int item3Count)
        {
            this.item3Count = item3Count;
        }

        private void setItem4Count(int item4Count)
        {
            this.item4Count = item4Count;
        }

        private void setCursorUpgradePrice(int cursorUpgradePrice)
        {
            this.cursorUpgradePrice = cursorUpgradePrice;
        }

        private void setCursorLevel(int cursorLevel)
        {
            this.cursorLevel = cursorLevel;
        }

        private void setGrandmaUpgradePrice(int grandmaUpgradePrice)
        {
            this.grandmaUpgradePrice = grandmaUpgradePrice;
        }

        private void setGrandmaLevel(int grandmaLevel)
        {
            this.grandmaLevel = grandmaLevel;
        }
    }
}
