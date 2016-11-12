using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace ImageTools.Utilities
{
    public static class Formats
    {
        public static Dictionary<PixelFormat, int> formats = new Dictionary<PixelFormat, int>() 
        {

            //{ PixelFormat.Format16bppArgb1555, 0}, 
            //{ PixelFormat.Format16bppGrayScale, 1},
            { PixelFormat.Format16bppRgb555, 0},
            { PixelFormat.Format16bppRgb565, 1},
            { PixelFormat.Format24bppRgb, 2},
            { PixelFormat.Format32bppArgb, 3},
            { PixelFormat.Format32bppPArgb, 4},
            { PixelFormat.Format32bppRgb, 5},
            { PixelFormat.Format48bppRgb, 6},
            { PixelFormat.Format64bppArgb, 7},
            { PixelFormat.Format64bppPArgb, 8},
        };

    }
}
