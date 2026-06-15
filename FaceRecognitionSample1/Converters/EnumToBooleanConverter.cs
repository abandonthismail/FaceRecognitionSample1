using System;
using System;
using System.Windows.Data;

namespace FaceRecognitionSample1.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        // Convert Enum value to Boolean for RadioButton.IsChecked
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();

            return string.Equals(checkValue, targetValue, StringComparison.InvariantCultureIgnoreCase);
        }

        // Convert Boolean back to Enum value when RadioButton is checked
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Binding.DoNothing;

            bool isChecked = (bool)value;
            if (isChecked)
            {
                return Enum.Parse(targetType, parameter.ToString(), true);
            }

            return Binding.DoNothing;
        }
    }
}