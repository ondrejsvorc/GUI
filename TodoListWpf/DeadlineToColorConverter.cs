using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TodoListWpf;

class DeadlineToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateTime dt)
        {
            return Binding.DoNothing;
        }

        const string Green = "#008000";
        const string Yellow = "#FFDE21";
        const string Red = "#FF0000";

        double days = (dt.Date - DateTime.Today).TotalDays;

        // > 6 days -> Green 
        // > 3 days -> Yellow
        // <= 3 days -> Red

        if (days > 6)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Green));
        }

        if (days > 3)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Yellow));
        }

        return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Red));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}