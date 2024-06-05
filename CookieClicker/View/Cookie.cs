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

        public void AddCookies(int amount)
        {
            Count += amount;
        }

        public void DeductCookies(int amount)
        {
            Count -= amount;
        }

        public void AddCookiesPerSecond(int amount)
        {
            CookiesPerSecond += amount;
        }

        public void AddCookiesFromTimer(int amount)
        {
            Count += CookiesPerSecond;
        }

        public void GoldenBonus(int amount)
        {
            Count += amount;
        }
    }
}
