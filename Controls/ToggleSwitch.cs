
namespace RandomUI.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// The <c>ToggleSwitch</c>
    /// </summary>
    public class ToggleSwitch : ToggleButton
    {
        public static readonly DependencyProperty LeftTextProperty = DependencyProperty.Register("LeftText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RightTextProperty = DependencyProperty.Register("RightText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch() : base()
        {
        }


        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }



        public string RightText
        {
            get { return (string)GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }
    }
}
