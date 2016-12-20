namespace VirtualInputHardware.UWP.Mvvm.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public sealed class IntToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double)((Int32)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Int32)((double)value);
        }
    }
}
