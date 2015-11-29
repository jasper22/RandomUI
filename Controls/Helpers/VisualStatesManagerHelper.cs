
namespace RandomUI.Controls.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Helper object for Window VisualState's
    /// </summary>
    public class VisualStatesManagerHelper
    {
        /// <summary>
        /// Function will return the very 'base' <see cref="FrameworkElement"/> for provided <paramref name="dependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns><see cref="FrameworkElement"/> if possible; otherwise <c>null</c></returns>
        internal static FrameworkElement GetImplementationRoot(DependencyObject dependencyObject)
        {
            Debug.Assert(dependencyObject != null, "DependencyObject should not be null.");

            return (VisualTreeHelper.GetChildrenCount(dependencyObject) == 1) ?
                VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement :
                null;
        }

        /// <summary>
        /// Tries the get <see cref="VisualStateGroup"/> from provided <paramref name="dependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns><see cref="VisualStateGroup"/> object if possible; otherwise <c>null</c></returns>
        internal static VisualStateGroup TryGetVisualStateGroup(DependencyObject dependencyObject, string groupName)
        {
            FrameworkElement root = GetImplementationRoot(dependencyObject);
            if (root == null)
            {
                return null;
            }

            return TryGetVisualStateGroup(root, groupName);
        }

        /// <summary>
        /// Tries the get <see cref="VisualStateGroup"/> from provided <paramref name="root"/>
        /// </summary>
        /// <param name="root">The root element</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns><see cref="VisualStateGroup"/> object if possible; otherwise <c>null</c></returns>
        internal static VisualStateGroup TryGetVisualStateGroup(FrameworkElement root, string groupName)
        {
            return VisualStateManager.GetVisualStateGroups(root)
                                     .OfType<VisualStateGroup>()
                                     .FirstOrDefault(group => string.CompareOrdinal(groupName, @group.Name) == 0);
        }

        /// <summary>
        /// Goes to state.
        /// </summary>
        /// <param name="targetControl">The target control.</param>
        /// <param name="visualStatesGroupName">Name of the visual states group.</param>
        /// <param name="targetState">State of the target.</param>
        /// <param name="useTransition">if set to <c>true</c> [use transition].</param>
        internal static void GoToState(FrameworkElement targetControl, string visualStatesGroupName, string targetState, bool useTransition = true)
        {
            if (StateExist(targetControl, visualStatesGroupName, targetState) == true)
            {
                VisualStateManager.GoToState(targetControl, targetState, useTransition);
            }
            else
            {
                // VisualState for this 'target state' is not defined in XAML
            }

        }

        /// <summary>
        /// Function will check if any of <see cref="VisualState"/> that exist in <paramref name="targetControl"/> does contain provided <paramref name="stateName"/>
        /// </summary>
        /// <param name="targetControl">The target control.</param>
        /// <param name="visualStatesGroupName">Name of the visual states group.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <returns><c>True</c> if exist; otherwise <c>false</c></returns>
        internal static bool StateExist(FrameworkElement targetControl, string visualStatesGroupName, string stateName)
        {
            var statesGroup = TryGetVisualStateGroup(targetControl, visualStatesGroupName);
            if (statesGroup == null)
            {
                // This VisualStateGroup is not defined in XAML !
                return false;
            }

            return statesGroup.States.OfType<VisualState>().Any(state => state.Name.StartsWith(stateName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
