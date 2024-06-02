namespace CookieClicker.View
{
    public class Assets
    {
        public int Cost { get; private set; }
        public int CookiesPerSecond { get; private set; }
        public int Count { get; set; }

        public Assets(int cost, int cookiesPerSecond)
        {
            Cost = cost;
            CookiesPerSecond = cookiesPerSecond;
            Count = 0;
        }

        public void IncreaseCost()
        {
            Cost = (int)(Cost * 1.15); // Increase the cost by 15%
        }
    }
}
