using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace View.ConvertiseurImage
{
	public class Base64ToImageConverter : ByteArrayToImageSourceConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Base64 = value as string;
            if (Base64 == null)
            {
                return null;
            }
            var x = System.Convert.FromBase64String(Base64);
            return base.ConvertFrom(x);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var img = value as ImageSource;
            var y = base.ConvertBackTo(img);
            return System.Convert.ToBase64String(y);
        }
    }
}

