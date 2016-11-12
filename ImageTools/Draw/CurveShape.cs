using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace ImageTools.Draw
{
    public class CurveShape : Shape2D
    {
        Pen p = null;
        Brush b = null;
        GraphicsPath geom;

        public CurveShape(List<Point3d> cp, bool close, float tension)
        {
            System.Drawing.Point[] points;
            points = new System.Drawing.Point[cp.Count];

            geom = new GraphicsPath();

            for (int i = 0; i < cp.Count; i++)
            {
                points[i] = new System.Drawing.Point(
                    (int)cp[i].X, (int)cp[i].Y);
            }

            if (close) geom.AddClosedCurve(points, tension);
            else geom.AddCurve(points, tension);
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
