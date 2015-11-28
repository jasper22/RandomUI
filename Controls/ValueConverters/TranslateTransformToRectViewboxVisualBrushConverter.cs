
namespace RandomUI.Controls.ValueConverters
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     http://charly-studio.com/blog/aero-effect-blurry-transparency-in-wpf/
    /// </remarks>
    [ValueConversion(typeof(TranslateTransform), typeof(Rect))]
    public class TranslateTransformToRectViewboxVisualBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var translate = value as TranslateTransform;
            if (translate != null)
            {
                return new Rect(translate.X, translate.Y, 0d, 0d);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
