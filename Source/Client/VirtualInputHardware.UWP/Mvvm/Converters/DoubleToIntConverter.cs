namespace VirtualInputHardware.UWP.Mvvm.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public sealed class DoubleToIntConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (!value.GetType().Equals(typeof(double)))
            {
                throw new ArgumentException("Only Double is supported");
            }
            if (targetType.Equals(typeof(Int32)))
            {
                return (Int32)((double)value);
            }
            else if (targetType.Equals(typeof(UInt32)))
            {
                return (UInt32)((double)value);
            }
            else
            {
                throw new ArgumentException("Unsuported type {0}", targetType.FullName);
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (double)((Int32)value);
        }
    }
}
