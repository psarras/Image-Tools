using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace ImageTools.Draw
{
    public class RectangleShape : Shape2D
    {
        Pen p = null;
        Brush b = null;
        GraphicsPath geom;

        public RectangleShape(Rectangle3d rec)
        {
            geom = new GraphicsPath();
            geom.AddRectangle(ShapeUtil.toRec2D(rec));
        }

        public void draw(Graphics gIMG)
        {
            if (b != null)
            {
                gIMG.FillPath(b, geom);
            }

            if (p != null)
            {
                gIMG.DrawPath(p, geom);
            }
        }

        public void fill(Pen b)
        {
            this.b = b.Brush;
        }

        public void outline(Pen p)
        {
            this.p = p;
        }
    }
}
