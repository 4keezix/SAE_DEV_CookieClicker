namespace CookieClicker.View
{
    public class Assets
    {
        public int InitialCost { get; private set; } 
        public int Cost { get; private set; }
        public int CookiesPerSecond { get; private set; }

        public Assets(int cost, int cookiesPerSecond)
        {
            InitialCost = cost; 
            Cost = cost;
            CookiesPerSecond = cookiesPerSecond;
        }


        public void IncreaseCost()
        {
            Cost = (int)(Cost * 1.15);
        }

        public void DecreaseCost()
        {
            Cost = (int)(Cost / 1.15);
        }

        public void Buy(Cookie cookie)
        {
            if (cookie.Count >= Cost)
            {
                cookie.DeductCookies(Cost);
                cookie.AddCookiesPerSecond(CookiesPerSecond);
                IncreaseCost();
            }
        }
        public void ResetCost()
        {
            Cost = InitialCost;
        }
       
    }
}