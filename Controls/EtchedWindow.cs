
namespace RandomUI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name = WindowBorder, Type = typeof(Border))]
    public class EtchedWindow : Window
    {
        private const string WindowBorder = "WindowBorder";

        public static readonly DependencyProperty IsWindowDraggingProperty = DependencyProperty.Register("IsWindowDragging", typeof(bool), typeof(EtchedWindow), new PropertyMetadata(false));

        public static readonly RoutedEvent OnStartWindowDragEvent = EventManager.RegisterRoutedEvent("OnStartWindowDrag", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EtchedWindow));

        public static readonly RoutedEvent OnFinishWindowDragEvent = EventManager.RegisterRoutedEvent("OnFinishWindowDrag", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EtchedWindow));

        private Border rootBorder;

        /// <summary>
        /// Initializes the <see cref="EtchedWindow"/> class.
        /// </summary>
        static EtchedWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EtchedWindow), new FrameworkPropertyMetadata(typeof(EtchedWindow)));
        }

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.rootBorder = GetTemplateChild(WindowBorder) as Border;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized" /> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized" /> is set to <see langword="true " />internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs" /> that contains the event data.</param>
        /// <remarks>
        ///     Sometimes the 'Style' property for template didn't pick up when this code was copy/paste in other location
        ///     So - adding this override function will insert correct StyleKey and everything just works
        /// </remarks>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.DefaultStyleKey = typeof(EtchedWindow);
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is window dragging.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is window dragging; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindowDragging
        {
            get { return (bool)GetValue(IsWindowDraggingProperty); }
            set { SetValue(IsWindowDraggingProperty, value); }
        }

        /// <summary>
        /// Occurs when [on start window drag].
        /// </summary>
        public event RoutedEventHandler OnStartWindowDrag
        {
            add { AddHandler(OnStartWindowDragEvent, value); }
            remove { RemoveHandler(OnStartWindowDragEvent, value); }
        }

        /// <summary>
        /// Occurs when [on finish window drag].
        /// </summary>
        public event RoutedEventHandler OnFinishWindowDrag
        {
            add { AddHandler(OnFinishWindowDragEvent, value); }
            remove { RemoveHandler(OnFinishWindowDragEvent, value); }
        }

        /// <summary>
        /// Raises the start window drag event.
        /// </summary>
        internal void RaiseStartWindowDragEvent()
        {
            this.IsWindowDragging = true;

            RoutedEventArgs newEventArgs = new RoutedEventArgs(EtchedWindow.OnStartWindowDragEvent);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// Raises the finish window drag event.
        /// </summary>
        internal void RaiseFinishWindowDragEvent()
        {
            this.IsWindowDragging = false;

            RoutedEventArgs newEventArgs = new RoutedEventArgs(EtchedWindow.OnFinishWindowDragEvent);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// Gets the window root border (that border whole window)
        /// </summary>
        /// <returns></returns>
        internal Border GetWindowRootBorder()
        {
            return this.rootBorder;
        }

    }
}
