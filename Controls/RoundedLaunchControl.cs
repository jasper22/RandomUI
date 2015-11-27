
namespace RandomUI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class RoundedLaunchControl : Control
    {
        static RoundedLaunchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedLaunchControl), new FrameworkPropertyMetadata(typeof(RoundedLaunchControl)));
        }

        public RoundedLaunchControl()
        {
        }
    }
}
