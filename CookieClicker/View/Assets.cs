namespace CookieClicker.View
{
    public class Assets
    {
        public int Cost { get; private set; }
        public int CookiesPerSecond { get; }

        public Assets(int initialCost, int cookiesPerSecond)
        {
            Cost = initialCost;
            CookiesPerSecond = cookiesPerSecond;
        }

        public void IncreaseCost()
        {
            Cost = (int)(Cost * 1.15);
        }
    }
}
