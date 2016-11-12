using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace ImageTools.Draw
{
    class ArcShape : Shape2D
    {
        Pen p = null;
        Brush b = null;
        GraphicsPath geom;

        public ArcShape(Rectangle3d rec, float startAngle, float sweepAngle)
        {
            geom = new GraphicsPath();
            geom.AddArc(ShapeUtil.toRec2D(rec), startAngle, sweepAngle);
        }

        public ArcShape(Point3d p, double size, double startAngle, double sweepAngle, bool pie)
        {
            geom = new GraphicsPath();
            Rectangle rec = new Rectangle((int)(p.X - size / 2.0f),
                (int)(p.Y - size / 2.0f), (int)size, (int)size);
            if (pie) geom.AddPie(rec, (float)startAngle, (float)sweepAngle);
            else geom.AddArc(rec, (float)startAngle, (float)sweepAngle);

        }

        public void outline(Pen p)
        {
            this.p = p;
        }

        public void fill(Pen b)
        {
            this.b = b.Brush;
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
    }
}
