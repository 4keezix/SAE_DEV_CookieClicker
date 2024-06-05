namespace CookieClicker.View
{
    public class Item
    {
        public int Cost { get; private set; }
        public int CookiesPerSecond { get; private set; }
        public int Count { get; private set; }

        public Item(int initialCost, int cookiesPerSecond)
        {
            Cost = initialCost;
            CookiesPerSecond = cookiesPerSecond;
            Count = 0;
        }

        public void Buy(Cookie cookie)
        {
            if (cookie.Count >= Cost)
            {
                cookie.DeductCookies(Cost);
                cookie.AddCookiesPerSecond(CookiesPerSecond);
                Count++;
                IncreaseCost();
            }
            else
            {
                throw new System.Exception("Pas assez de cookies !");
            }
        }

        private void IncreaseCost()
        {
            Cost = (int)(Cost * 1.15);
        }
    }
}
