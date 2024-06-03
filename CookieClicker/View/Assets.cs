namespace CookieClicker.View
{
    public class Assets
    {
        public int Cost { get; private set; }
        public int CookiesPerSecond { get; private set; }

        public Assets(int cost, int cookiesPerSecond)
        {
            Cost = cost;
            CookiesPerSecond = cookiesPerSecond;
        }

        public void IncreaseCost(int basePrice, int itemCount)
        {
            Cost = (int)(basePrice * Math.Pow(1.15, itemCount));
        }
    }
}