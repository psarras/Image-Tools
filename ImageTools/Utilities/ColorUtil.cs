using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public class ColorUtil
    {
        // in a language with good vector support
        public static Vector3d getColorPretty(double t, Vector3d constant, Vector3d multi, 
            Vector3d repeat, Vector3d phase)
        {

            double r = (constant.X + multi.X * 
                Math.Cos(2 * Math.PI * repeat.X * t + phase.X));
            double g = (constant.Y + multi.Y * 
                Math.Cos(2 * Math.PI * repeat.Y * t + phase.Y));
            double b = (constant.Z + multi.Z * 
                Math.Cos(2 * Math.PI * repeat.Z * t + phase.Z));


            return new Vector3d(clamp(r), clamp(g), clamp(b));
            
            //return constant + multi *Math.Cos(2 * Math.PI * repeat + phase);
        }

        public static double clamp(double a)
        {
            return Math.Min(1, Math.Max(0, a));
        }
    }
}
