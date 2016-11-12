using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTools.Draw
{
    public static class ShapeUtil
    {
        public static Rectangle toRec2D(Rectangle3d rec)
        {
            return new Rectangle((int)rec.PointAt(0, 0).X,
                (int)rec.PointAt(0, 0).Y, (int)rec.Width, (int)rec.Height);
        }
    }
}
