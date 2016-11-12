using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public static class Filters
    {
        public static Dictionary<string, int> filters = new Dictionary<string, int>()
        {
            {"Greyscale", 0},
            {"Sepia", 1},
            {"Invert", 2},
            {"RotateChannel", 3},
            {"Threshold", 4},
            {"FloydFilter", 5},
            {"OrderedDithering", 6},
            {"Sharpen", 7},
            {"DifferenceEdgeDetector", 8},
            {"HomogenityEdgeDetector", 9},
            {"Sobel", 10},
            {"Jitter", 11},
            {"OilPainting", 12},
            {"TextureFiltering", 13},
            {"Median", 14},
            {"Mean", 15},
            {"Blur", 16}
        };

        public static Dictionary<string, int> mode = new Dictionary<string, int>()
        {
            {"Add", 0},
            {"Subtract", 1},
            {"Intersect", 2},
            {"Difference", 3},
            {"Merge", 4},
            {"Multiply", 5 },
            {"Lighten Filter", 6},
            {"Linear Dodge (Add)", 7},
            {"Soft Light", 8},
            {"Darken", 10},
            {"Overlay", 11}
            //{"Lighten Filter", 0},
            //{"Linear Dodge (Add)", 1},
            //{"Soft Light", 2},
            //{"Multiply", 3},
            //{"Darken", 4},
            //{"Overlay", 5},
        };

        
        //public static Dictionary<string, int> operations = new Dictionary<string, int>()
        //{
        //    {"Add", 0},
        //    {"Subtract", 1},
        //    {"Intersect", 2},
        //    {"Difference", 3},
        //    {"Merge", 4},
        //    {"Multiply", 5 }

        //};
        

        public static Dictionary<string, int> textures = new Dictionary<string, int>()
        {
            {"Clouds", 0},
            {"Textile", 1},
            {"Wood", 2},
            {"Marble", 3 },
            {"Labyrinth", 4}
        };

    }




}
