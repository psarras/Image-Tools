using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using ImageTools.Components;

namespace ImageTools.Attributes
{
    internal class GH_ImageAttributes : GH_ComponentAttributes
    {

        private RectangleF baseBounds;
        private RectangleF thisBounds;
        private RectangleF imgBounds;
        private RectangleF buttonBounds;
        private RectangleF dpiBounds;
        private RectangleF ratioBounds;
        private RectangleF backButton;

        public GH_ImageAttributes(ImageViewerComponent owner) : base(owner)
        {
        }

        protected override void Layout()
        {
            base.Layout();
            baseBounds = Bounds;
            thisBounds = GH_Convert.ToRectangle(Bounds);
            //Ref
            ImageViewerComponent component = Owner as ImageViewerComponent;
            //Bounds
            int buttonHorizontalResolution = component.resolution.Length * 6 + 6;
            int buttonHorizontalDPI = component.dpi.Length * 6 + 6;
            int buttonHorizontalRatio = component.ratio.Length * 6 + 6;

            buttonBounds = new RectangleF(new PointF(baseBounds.Right + 10, this.Bounds.Top),
                new SizeF(buttonHorizontalResolution, baseBounds.Height));
            buttonBounds.Inflate(0, -5);

            dpiBounds = new RectangleF(new PointF(buttonBounds.Right + 5, baseBounds.Top),
                new SizeF(buttonHorizontalDPI, baseBounds.Height));
            dpiBounds.Inflate(0, -5);

            ratioBounds = new RectangleF(new PointF(dpiBounds.Right + 5, baseBounds.Top), 
                new SizeF(buttonHorizontalRatio, baseBounds.Height));
            ratioBounds.Inflate(0, -5);

            ////////////////////////////////////////////////////////////////////////////

            SizeF minSize = new SizeF();
            if (component.m_Size.Width < 200)
                minSize = new SizeF(200, 200);
            else
                minSize = component.m_Size;

            imgBounds = new RectangleF(new PointF(baseBounds.Left + 10, baseBounds.Bottom),
                                       minSize);

            backButton = new RectangleF(imgBounds.Location, imgBounds.Size);
            backButton.Inflate(5, 5);

            thisBounds = RectangleF.Union(thisBounds, buttonBounds);
            thisBounds = RectangleF.Union(thisBounds, dpiBounds);
            thisBounds = RectangleF.Union(thisBounds, backButton);
            thisBounds = RectangleF.Union(thisBounds, ratioBounds);
            thisBounds.Width += 5;
            thisBounds.Height += 5;

            Bounds = thisBounds;
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            ImageViewerComponent component = Owner as ImageViewerComponent;

            GH_Capsule dimensionButtom = GH_Capsule.CreateTextCapsule(buttonBounds, buttonBounds,
                GH_Palette.Transparent, component.resolution);
            dimensionButtom.Render(graphics, Color.LightGray);

            GH_Capsule dpiButton = GH_Capsule.CreateTextCapsule(dpiBounds, dpiBounds,
                GH_Palette.Transparent, component.dpi);
            dpiButton.Render(graphics, Color.LightGray);

            GH_Capsule ratioButton = GH_Capsule.CreateTextCapsule(ratioBounds, ratioBounds,
                GH_Palette.Transparent, component.ratio);
            ratioButton.Render(graphics, Color.LightGray);

            //////////////////////////////
            Rectangle frameRec1 = new Rectangle((int)imgBounds.X, (int)imgBounds.Y,
                (int)imgBounds.Width, (int)imgBounds.Height);
            Rectangle frameRec2 = new Rectangle((int)imgBounds.X, (int)imgBounds.Y,
                (int)imgBounds.Width, (int)imgBounds.Height);

            Pen b = new Pen(Color.Black);
            graphics.DrawRectangle(b, frameRec1);

            Brush w = Brushes.White;
            graphics.FillRectangle(w, frameRec2);

            if (component.m_image != null) graphics.DrawImage(component.m_image, imgBounds);

            dimensionButtom.Dispose();
            dpiButton.Dispose();
            ratioButton.Dispose();
        }
    }
}