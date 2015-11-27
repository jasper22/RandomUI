
namespace RandomUI.Behaviours
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Freezable collection of <see cref="Behavior"/> objects
    /// </summary>
    /// <remarks>
    ///     User in <c>ControlTemplate</c> because behavior is not Freezable and can not be used in <c>ControlTemplate</c> easily - so here is the fix
    /// </remarks>
    public class StylizedBehaviorCollection : FreezableCollection<Behavior>
    {
        /// <summary>
        /// Creates the instance core.
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new StylizedBehaviorCollection();
        }
    }
}
