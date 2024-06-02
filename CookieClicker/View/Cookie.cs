﻿namespace CookieClicker.View
{
    public class Cookie
    {
        public int Count { get; private set; }
        public int CookiesPerSecond { get; private set; }

        public void AddCookie()
        {
            Count++;
        }

        public void AddCookiesFromTimer()
        {
            Count += CookiesPerSecond;
        }

        public void DeductCookies(int amount)
        {
            Count -= amount;
        }

        public void AddCookiesPerSecond(int amount)
        {
            CookiesPerSecond += amount;
        }
    }
}