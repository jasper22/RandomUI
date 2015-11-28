
namespace RandomUI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Etched <c>ComboBox</c> with callout on the top
    /// </summary>
    public class CalloutComboBox : ComboBox
    {
        public static readonly DependencyProperty PopupActualWidthProperty = DependencyProperty.Register("PopupActualWidth", typeof(double), typeof(CalloutComboBox), new PropertyMetadata(0.0));

        public static readonly DependencyProperty PopupActualHeightProperty = DependencyProperty.Register("PopupActualHeight", typeof(double), typeof(CalloutComboBox), new PropertyMetadata(0.0));

        /// <summary>
        /// Initializes the <see cref="CalloutComboBox"/> class.
        /// </summary>
        static CalloutComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalloutComboBox), new FrameworkPropertyMetadata(typeof(CalloutComboBox)));
        }

        /// <summary>
        /// Gets or sets the actual width of the popup.
        /// </summary>
        /// <value>
        /// The actual width of the popup.
        /// </value>
        public double PopupActualWidth
        {
            get { return (double)GetValue(PopupActualWidthProperty); }
            set { SetValue(PopupActualWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the actual height of the popup.
        /// </summary>
        /// <value>
        /// The actual height of the popup.
        /// </value>
        public double PopupActualHeight
        {
            get { return (double)GetValue(PopupActualHeightProperty); }
            set { SetValue(PopupActualHeightProperty, value); }
        }
    }
}
