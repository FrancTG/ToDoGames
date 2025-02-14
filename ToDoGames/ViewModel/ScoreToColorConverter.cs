using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ToDoGames.ViewModel
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class ScoreToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush;
            int score = (int)value;
            if (score > 89)
            {
                brush = Brushes.Purple;
            }
            else if (score > 79)
            {
                brush = Brushes.LawnGreen;
            }
            else if (score > 69)
            {
                brush = Brushes.ForestGreen;
            }
            else if (score > 49)
            {
                brush = Brushes.Orange;
            }
            else
            {
                brush = Brushes.Red;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
