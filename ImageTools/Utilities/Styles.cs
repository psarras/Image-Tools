using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public static class Styles
    {
        public static Dictionary<int, HatchStyle> hatch = new Dictionary<int, HatchStyle>()
        {
            {0, HatchStyle.BackwardDiagonal},
            {1, HatchStyle.Cross},
            {2, HatchStyle.DarkDownwardDiagonal},
            {3, HatchStyle.DarkHorizontal},
            {4, HatchStyle.DarkUpwardDiagonal},
            {5, HatchStyle.DarkVertical},
            {6, HatchStyle.DashedDownwardDiagonal},
            {7, HatchStyle.DashedHorizontal},
            {8, HatchStyle.DashedUpwardDiagonal},
            {9, HatchStyle.DashedVertical},
            {10, HatchStyle.DiagonalBrick},
            {11, HatchStyle.DiagonalCross},
            {12, HatchStyle.Divot},
            {13, HatchStyle.DottedDiamond},
            {14, HatchStyle.DottedGrid},
            {15, HatchStyle.ForwardDiagonal},
            {16, HatchStyle.Horizontal},
            {17, HatchStyle.HorizontalBrick},
            {18, HatchStyle.LargeCheckerBoard},
            {19, HatchStyle.LargeConfetti},
            {20, HatchStyle.LargeGrid},
            {21, HatchStyle.LightDownwardDiagonal},
            {22, HatchStyle.LightHorizontal},
            {23, HatchStyle.LightUpwardDiagonal},
            {24, HatchStyle.LightVertical},
            {25, HatchStyle.Max},
            {26, HatchStyle.Min},
            {27, HatchStyle.NarrowHorizontal},
            {28, HatchStyle.NarrowVertical},
            {29, HatchStyle.OutlinedDiamond},
            {30, HatchStyle.NarrowVertical},
            {31, HatchStyle.Percent05},
            {32, HatchStyle.Percent10},
            {33, HatchStyle.Percent20},
            {34, HatchStyle.Percent25},
            {35, HatchStyle.Percent30},
            {36, HatchStyle.Percent40},
            {37, HatchStyle.Percent50},
            {38, HatchStyle.Percent60},
            {39, HatchStyle.Percent70},
            {40, HatchStyle.Percent75},
            {41, HatchStyle.Percent80},
            {42, HatchStyle.Percent90},
            {43, HatchStyle.Plaid},
            {44, HatchStyle.Shingle },
            {45, HatchStyle.SmallCheckerBoard},
            {46, HatchStyle.SmallConfetti},
            {47, HatchStyle.SmallGrid},
            {48, HatchStyle.SolidDiamond},
            {49, HatchStyle.Sphere},
            {50, HatchStyle.Trellis},
            {51, HatchStyle.Vertical},
            {52, HatchStyle.Wave},
            {53, HatchStyle.Weave},
            {54, HatchStyle.WideDownwardDiagonal},
            {55, HatchStyle.WideUpwardDiagonal},
            {56, HatchStyle.ZigZag}
            
        };

        public static Dictionary<int, WrapMode> wrap = new Dictionary<int, WrapMode>()
        {
            {0, WrapMode.Clamp},
            {1, WrapMode.Tile},
            {2, WrapMode.TileFlipX},
            {3, WrapMode.TileFlipXY},
            {4, WrapMode.TileFlipY}
        };

        public static Dictionary<int, DashStyle> dash = new Dictionary<int, DashStyle>()
        {
            {0, DashStyle.Solid },
            {1, DashStyle.Dash },
            {2, DashStyle.Dot },
            {3, DashStyle.DashDot },
            {4, DashStyle.DashDotDot }
        };

    }


}
