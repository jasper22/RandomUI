
namespace RandomUI.Behaviours
{
    using System.Windows;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Object provide attached properties for <c>Behavior</c> binding in <c>ControlTemplate</c>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class StylizedBehaviors
    {
        /// <summary>
        /// The "original behavior" property
        /// </summary>
        /// <remarks>
        ///     This DP actually bind to <see cref="Behavior"/> object
        /// </remarks>
        private static readonly DependencyProperty OriginalBehaviorProperty = DependencyProperty.RegisterAttached(@"OriginalBehaviorInternal", typeof(Behavior), typeof(StylizedBehaviors), new UIPropertyMetadata(null));

        /// <summary>
        /// The behaviors property
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(@"Behaviors", typeof(StylizedBehaviorCollection), typeof(StylizedBehaviors), new FrameworkPropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// Gets the behaviors.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static StylizedBehaviorCollection GetBehaviors(DependencyObject obj)
        {
            return (StylizedBehaviorCollection)obj.GetValue(BehaviorsProperty);
        }

        /// <summary>
        /// Sets the behaviors.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static void SetBehaviors(DependencyObject obj, StylizedBehaviorCollection value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }

        /// <summary>
        /// Gets the original behavior.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        private static Behavior GetOriginalBehavior(DependencyObject obj)
        {
            return obj.GetValue(OriginalBehaviorProperty) as Behavior;
        }

        /// <summary>
        /// Sets the original behavior.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        private static void SetOriginalBehavior(DependencyObject obj, Behavior value)
        {
            obj.SetValue(OriginalBehaviorProperty, value);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="dpo">The dpo.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///     Will actually apply the 'NewValue' <see cref="Behavior"/> objects
        /// </remarks>
        private static void OnPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            var uie = dpo as UIElement;

            if (uie == null)
            {
                return;
            }

            BehaviorCollection itemBehaviors = Interaction.GetBehaviors(uie);

            var newBehaviors = e.NewValue as StylizedBehaviorCollection;
            var oldBehaviors = e.OldValue as StylizedBehaviorCollection;

            if (newBehaviors == oldBehaviors)
            {
                return;
            }

            if (oldBehaviors != null)
            {
                foreach (var behavior in oldBehaviors)
                {
                    int index = GetIndexOf(itemBehaviors, behavior);

                    if (index >= 0)
                    {
                        itemBehaviors.RemoveAt(index);
                    }
                }
            }

            if (newBehaviors != null)
            {
                foreach (var behavior in newBehaviors)
                {
                    int index = GetIndexOf(itemBehaviors, behavior);

                    if (index < 0)
                    {
                        var clone = (Behavior)behavior.Clone();
                        SetOriginalBehavior(clone, behavior);
                        itemBehaviors.Add(clone);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the index of provided <paramref name="behavior"/> in provided <paramref name="itemBehaviors"/> collection
        /// </summary>
        /// <param name="itemBehaviors">The item behaviors collection</param>
        /// <param name="behavior">The behavior to search for</param>
        /// <returns>Index of Behavior in collection if found; otherwise <c>-1</c></returns>
        private static int GetIndexOf(BehaviorCollection itemBehaviors, Behavior behavior)
        {
            int index = -1;

            Behavior orignalBehavior = GetOriginalBehavior(behavior);

            for (int i = 0; i < itemBehaviors.Count; i++)
            {
                Behavior currentBehavior = itemBehaviors[i];

                if (currentBehavior == behavior || currentBehavior == orignalBehavior)
                {
                    index = i;
                    break;
                }

                Behavior currentOrignalBehavior = GetOriginalBehavior(currentBehavior);

                if (currentOrignalBehavior == behavior
                    || currentOrignalBehavior == orignalBehavior)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
