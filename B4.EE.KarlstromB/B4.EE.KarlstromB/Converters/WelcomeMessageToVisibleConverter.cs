using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.Converters
{
    public class WelcomeMessageToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string welcomeMessage = "";
            if (value == null)
            {
                return welcomeMessage;
            }
            else
            {
                welcomeMessage = $"Welcome back, {value}";

                return welcomeMessage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
