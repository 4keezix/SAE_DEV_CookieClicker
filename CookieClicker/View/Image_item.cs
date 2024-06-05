using System.Windows;
using System.Windows.Controls;

namespace CookieClicker.View
{
    public class ImageItem
    {
        private bool isSpecialImage2Displayed = false;
        private bool isSpecialImage3Displayed = false;
        private bool isSpecialImage4Displayed = false;

        private Image specialImage2;
        private Image specialImage3;
        private Image specialImage4;

        public ImageItem(Image specialImage2, Image specialImage3, Image specialImage4)
        {
            this.specialImage2 = specialImage2;
            this.specialImage3 = specialImage3;
            this.specialImage4 = specialImage4;
        }

        public void ShowImageForItem2()
        {
            if (!isSpecialImage2Displayed)
            {
                specialImage2.Visibility = Visibility.Visible;
                isSpecialImage2Displayed = true;
            }
        }

        public void ShowImageForItem3()
        {
            if (!isSpecialImage3Displayed)
            {
                specialImage3.Visibility = Visibility.Visible;
                isSpecialImage3Displayed = true;
            }
        }

        public void ShowImageForItem4()
        {
            if (!isSpecialImage4Displayed)
            {
                specialImage4.Visibility = Visibility.Visible;
                isSpecialImage4Displayed = true;
            }
        }
    }
}
