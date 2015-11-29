
namespace RandomUI.Controls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using Helpers;

    /// <summary>
    /// The <c>BusyAnimation1</c>
    /// </summary>
    public class BusyAnimation1 : Control
    {
        private const double STORYBOARD_DEFAULT_MAX_VALUE = 200;

        private const string TemplateRoot = "templateRoot";

        private const string AnimatedRoot = "animatedRoot";

        public static readonly DependencyProperty AnimationEasingFunctionProperty = DependencyProperty.Register("AnimationEasingFunction", typeof(IEasingFunction), typeof(BusyAnimation1), new PropertyMetadata(null));


        private Grid animatedRoot, templateRoot;

        /// <summary>
        /// Initializes the <see cref="BusyAnimation1"/> class.
        /// </summary>
        static BusyAnimation1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyAnimation1), new FrameworkPropertyMetadata(typeof(BusyAnimation1)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyAnimation1"/> class.
        /// </summary>
        public BusyAnimation1()
        {
            this.Loaded += BusyAnimation1_Loaded;
        }

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.templateRoot = GetTemplateChild(TemplateRoot) as Grid;
            this.animatedRoot = GetTemplateChild(AnimatedRoot) as Grid;
        }

        /// <summary>
        /// Handles the Loaded event of the BusyAnimation1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BusyAnimation1_Loaded(object sender, RoutedEventArgs e)
        {
            var stateGroup = VisualStatesManagerHelper.TryGetVisualStateGroup(this.animatedRoot, "DefaultVisualStates");

            if (stateGroup != null)
            {
                var vState1 = stateGroup.States.OfType<VisualState>().SingleOrDefault(state => state.Name.Equals("Working", StringComparison.OrdinalIgnoreCase));
                if (vState1 != null)
                {
                    var tmpMoveEleipsesAnimation = vState1.Storyboard;
                    if (tmpMoveEleipsesAnimation != null)
                    {
                        var moveEleipsesAnimation = vState1.Storyboard.Clone();

                        foreach (DoubleAnimation child in moveEleipsesAnimation.Children)
                        {
                            if ((child.To.HasValue == true) && (child.To.Value == STORYBOARD_DEFAULT_MAX_VALUE))
                            {
                                child.To = this.templateRoot.ActualWidth - 70;
                            }

                            if ((child.From.HasValue == true) && (child.From.Value == STORYBOARD_DEFAULT_MAX_VALUE))
                            {
                                child.From = this.templateRoot.ActualWidth - 70;
                            }
                        }

                        moveEleipsesAnimation.Begin(this.templateRoot);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the animation easing function.
        /// </summary>
        /// <value>
        /// The animation easing function.
        /// </value>
        public IEasingFunction AnimationEasingFunction
        {
            get { return (IEasingFunction)GetValue(AnimationEasingFunctionProperty); }
            set { SetValue(AnimationEasingFunctionProperty, value); }
        }
    }
}
