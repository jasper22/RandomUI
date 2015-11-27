
namespace RandomUI.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Enumeration of resize move
    /// </summary>
    /// <remarks>
    ///     Should be fixed 
    /// </remarks>
    [Flags]
    public enum ResizeGripDirection
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The <c>Top</c> 
        /// </summary>
        Top = 0x1,

        /// <summary>
        /// The <c>bottom</c>
        /// </summary>
        Bottom = 0x2,

        /// <summary>
        /// The <c>left</c>
        /// </summary>
        Left = 0x8,

        /// <summary>
        /// The <c>right</c>
        /// </summary>
        Right = 0x10
    }

    /// <summary>
    /// Adorner that could provide resize behavior for <see cref="Window"/>
    /// </summary>
    public class WindowResizingAdorner : Adorner
    {
        private VisualCollection visualChildren;

        private Window currentWindow;

        private ResizeThumb[] resizeThumbsCollection;

        private Point dragMouseStartPosition, dragWindowStartPosition;

        private Size windowInitialSize;

        private const double MINIMUM_THUMB_THIKNESS = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowResizingAdorner"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="window">The window.</param>
        public WindowResizingAdorner(UIElement element, Window window)
            : base(element)
        {
            this.visualChildren = new VisualCollection(element);
            this.currentWindow = window;

            this.resizeThumbsCollection = new ResizeThumb[8];

            resizeThumbsCollection[0] = CreateThumb(ResizeGripDirection.Left | ResizeGripDirection.Top, Cursors.SizeNWSE);
            resizeThumbsCollection[1] = CreateThumb(ResizeGripDirection.Right | ResizeGripDirection.Top, Cursors.SizeNESW);

            resizeThumbsCollection[2] = CreateThumb(ResizeGripDirection.Left | ResizeGripDirection.Bottom, Cursors.SizeNESW);
            resizeThumbsCollection[3] = CreateThumb(ResizeGripDirection.Right | ResizeGripDirection.Bottom, Cursors.SizeNWSE);

            resizeThumbsCollection[4] = CreateThumb(ResizeGripDirection.Left, Cursors.SizeWE);
            resizeThumbsCollection[5] = CreateThumb(ResizeGripDirection.Top, Cursors.SizeNS);
            resizeThumbsCollection[6] = CreateThumb(ResizeGripDirection.Right, Cursors.SizeWE);
            resizeThumbsCollection[7] = CreateThumb(ResizeGripDirection.Bottom, Cursors.SizeNS);
        }

        /// <summary>
        /// Creates the thumb.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="visualCursor">The visual cursor.</param>
        /// <returns></returns>
        protected ResizeThumb CreateThumb(ResizeGripDirection direction, Cursor visualCursor)
        {
            ResizeThumb thumb = new ResizeThumb
            {
                ResizeGripDirection = direction,
                Cursor = visualCursor
            };

            thumb.DragStarted += Thumb_DragStarted;
            thumb.DragDelta += Thumb_DragDelta;

            this.visualChildren.Add(thumb);

            return thumb;
        }

        /// <summary>
        /// Handles the DragStarted event of the Thumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragStartedEventArgs"/> instance containing the event data.</param>
        protected void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            ResizeThumb resizeThumb = sender as ResizeThumb;
            if (resizeThumb == null)
            {
                return;
            }

            // Save settings here that will be used later
            this.dragMouseStartPosition = PointToScreen(Mouse.GetPosition(this.currentWindow));
            this.dragWindowStartPosition = new Point(this.currentWindow.Left, this.currentWindow.Top);
            this.windowInitialSize = new Size(this.currentWindow.Width, this.currentWindow.Height);
        }

        /// <summary>
        /// Handles the DragDelta event of the Thumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragDeltaEventArgs"/> instance containing the event data.</param>
        protected void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeThumb resizeThumb = sender as ResizeThumb;
            if (resizeThumb == null)
            {
                return;
            }

            Point position = PointToScreen(Mouse.GetPosition(this.currentWindow));
            double deltaX = position.X - this.dragMouseStartPosition.X;
            double deltaY = position.Y - this.dragMouseStartPosition.Y;

            // Horizontal resize
            if ((resizeThumb.ResizeGripDirection & ResizeGripDirection.Left) == ResizeGripDirection.Left)
            {
                SetWindowWidth(this.windowInitialSize.Width - deltaX);
                this.currentWindow.Left = this.dragWindowStartPosition.X + deltaX;
            }
            else if ((resizeThumb.ResizeGripDirection & ResizeGripDirection.Right) == ResizeGripDirection.Right)
            {
                SetWindowWidth(this.windowInitialSize.Width + deltaX);
            }

            // Vertical resize
            if ((resizeThumb.ResizeGripDirection & ResizeGripDirection.Top) == ResizeGripDirection.Top)
            {
                SetWindowHeight(this.windowInitialSize.Height + deltaY);
                this.currentWindow.Top = this.dragWindowStartPosition.Y - deltaY;
            }
            else if ((resizeThumb.ResizeGripDirection & ResizeGripDirection.Bottom) == ResizeGripDirection.Bottom)
            {
                SetWindowHeight(this.windowInitialSize.Height + deltaY);
            }
        }

        /// <summary>
        /// Sets the 'width' of the window.
        /// </summary>
        /// <param name="width">The width.</param>
        protected void SetWindowWidth(double width)
        {
            if (width < 2 * MINIMUM_THUMB_THIKNESS)
            {
                width = 2 * MINIMUM_THUMB_THIKNESS;
            }

            this.currentWindow.Width = width;
        }

        /// <summary>
        /// Auxiliary method for setting Window height
        /// </summary>
        /// <param name="height">New window hright</param>
        protected void SetWindowHeight(double height)
        {
            if (height < 2 * MINIMUM_THUMB_THIKNESS)
            {
                height = 2 * MINIMUM_THUMB_THIKNESS;
            }

            this.currentWindow.Height = height;
        }


        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a <see cref="T:System.Windows.FrameworkElement" /> derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>
        /// The actual size used.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // base.ArrangeOverride(finalSize);

            //double desireWidth = AdornedElement.DesiredSize.Width;
            //double desireHeight = AdornedElement.DesiredSize.Height;

            this.resizeThumbsCollection[0].Arrange(new Rect(0, 0, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS));
            this.resizeThumbsCollection[1].Arrange(new Rect(this.DesiredSize.Width - MINIMUM_THUMB_THIKNESS, 0, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS));

            this.resizeThumbsCollection[2].Arrange(new Rect(0, this.DesiredSize.Height - MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS));
            this.resizeThumbsCollection[3].Arrange(new Rect(this.DesiredSize.Width - MINIMUM_THUMB_THIKNESS, this.DesiredSize.Height - MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS));

            this.resizeThumbsCollection[4].Arrange(new Rect(0, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS, this.DesiredSize.Height - (2 * MINIMUM_THUMB_THIKNESS)));
            this.resizeThumbsCollection[5].Arrange(new Rect(MINIMUM_THUMB_THIKNESS, 0, this.DesiredSize.Width - (2 * MINIMUM_THUMB_THIKNESS), MINIMUM_THUMB_THIKNESS));
            this.resizeThumbsCollection[6].Arrange(new Rect(this.DesiredSize.Width - MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS, MINIMUM_THUMB_THIKNESS, this.DesiredSize.Height - (2 * MINIMUM_THUMB_THIKNESS)));
            this.resizeThumbsCollection[7].Arrange(new Rect(MINIMUM_THUMB_THIKNESS, this.DesiredSize.Height - MINIMUM_THUMB_THIKNESS, this.DesiredSize.Width - (2 * MINIMUM_THUMB_THIKNESS), MINIMUM_THUMB_THIKNESS));

            return finalSize;
        }

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visualChildren.Count;
            }
        }

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)" />, and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>
        /// The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.
        /// </returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }
    }

    /// <summary>
    /// Control (actually a Border) that will be used for resize border
    /// </summary>
    public class ResizeThumb : Thumb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeThumb"/> class.
        /// </summary>
        /// <remarks>
        ///     Actually it will create a <see cref="Border"/> element and not <see cref="Thumb"/> element
        /// </remarks>
        public ResizeThumb() : base()
        {
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, Brushes.Transparent);

            ControlTemplate template = new ControlTemplate(typeof(ResizeThumb));
            template.VisualTree = borderFactory;

            this.Template = template;
        }

        /// <summary>
        /// Gets or sets the resize grip direction.
        /// </summary>
        /// <value>
        /// The resize grip direction.
        /// </value>
        public ResizeGripDirection ResizeGripDirection
        {
            get;
            set;
        }
    }
}
