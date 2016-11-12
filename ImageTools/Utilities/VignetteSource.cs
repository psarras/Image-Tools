using System;
using System.Collections.Generic;
//using System.Windows.Media;
using System.Windows;
using System.Linq;
//using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

// Program to "vignettify" an image, for circular, elliptical, diamond, rectangular
//  and square-shaped vignettes.
// Written by Amarnath S, Bengaluru, India. Version 1.0, April 2011.
// amarnaths.codeproject@gmail.com

namespace Vignettes
{

    /// <summary>
    /// Class to implement the vignette effect. 
    /// </summary>
    public static class VignetteEffect
    {
        /*
        /// <summary>
        /// Orientation of the Ellipse, Diamond, Square or Rectangle in degrees. 
        /// This parameter is not of relevance for the Circle vignette.
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Coverage of the vignette in percentage of the image dimension (width or height).
        /// </summary>
        public double Coverage { get; set; }

        /// <summary>
        /// Width of the "band" between the inner "original image" region and the outer
        /// "border" region. This width is measured in pixels.
        /// </summary>
        public int BandPixels { get; set; }

        /// <summary>
        /// Number of steps of "gradation" to be accommodated within the above parameter BandPixels.
        /// This is just a number, and has no units.
        /// </summary>
        public int NumberSteps { get; set; }

        /// <summary>
        /// X Offset of the centre of rotation in terms of percentage with respect to half the 
        /// width of the image.
        /// </summary>
        public int Xcentre { get; set; }

        /// <summary>
        /// Y Offset of the centre of rotation in terms of percentage with respect to half the 
        /// height of the image.
        /// </summary>
        public int Ycentre { get; set; }

        /// <summary>
        /// Border Colour of the vignette. We consider only R, G, B values here. Alpha value is ignored.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Shape of the Vignette - one of Circle, Ellipse, Diamond, Rectangle or Square.
        /// </summary>
        //public VignetteShape Shape { get; set; }

        /// <summary>
        /// Name of the file for saving.
        /// </summary>
        public string FileNameToSave { get; set; }
        */
        /// <summary>
        /// Method to apply the Circular, Elliptical or Diamond-shaped vignette on an image.
        /// </summary>
        public static void ApplyEffectCircleEllipseDiamond(Bitmap img, float Angle, Point center, Color BorderColor)
        {

            int width = img.Width;
            int height = img.Height;
            int Xcentre = center.X;
            int Ycentre = center.Y;
            double geometryFactor = 0.5 / 100.0;
            int w1, w2;
            byte r, g, b;
            double wb2 = width * 0.5 + Xcentre * width * geometryFactor;
            double hb2 = height * 0.5 + Ycentre * height * geometryFactor;
            double thetaRadians = Angle * Math.PI / 180.0;
            double cos = Math.Cos(thetaRadians);
            double sin = Math.Sin(thetaRadians);
            double xprime, yprime, potential1, potential2, potential;
            double factor1, factor2, factor3, factor4;
            byte redBorder = BorderColor.R;
            byte greenBorder = BorderColor.G;
            byte blueBorder = BorderColor.B;

            List<byte> pixRedOrig = new List<byte>();       // List of red pixels in original image.
            List<byte> pixGreenOrig = new List<byte>();     // List of green pixels in original image.
            List<byte> pixBlueOrig = new List<byte>();      // List of blue pixels in original image.
            List<byte> pixRedModified = new List<byte>();   // List of red pixels in modified image.
            List<byte> pixGreenModified = new List<byte>(); // List of green pixels in modified image.
            List<byte> pixBlueModified = new List<byte>();  // List of blue pixels in modified image.

            List<double> aVals = new List<double>();          // Major axis value of the vignette shape.
            List<double> bVals = new List<double>();          // Minor axis value of the vignette shape.
            List<double> aValsMidPoints = new List<double>(); // Major axis of mid-figures of the vignette shape.
            List<double> bValsMidPoints = new List<double>(); // Minor axis of mid-figures of the vignette shape.

            List<double> weight1 = new List<double>();        // Weights for the original image.
            List<double> weight2 = new List<double>();        // Weights for the border colour.

            int BandPixels = 100;
            int NumberSteps = 50;
            double Coverage = 0.2;

            double a0, b0, aEll, bEll;
            double stepSize = BandPixels * 1.0 / NumberSteps;
            double bandPixelsBy2 = 0.5 * BandPixels;
            double arguFactor = Math.PI / BandPixels;
            double vignetteWidth = width * Coverage / 100.0;
            double vignetteHeight = height * Coverage / 100.0;
            double vwb2 = vignetteWidth * 0.5;
            double vhb2 = vignetteHeight * 0.5;
            a0 = vwb2 - bandPixelsBy2;
            b0 = vhb2 - bandPixelsBy2;

            for (int i = 0; i <= NumberSteps; ++i)
            {
                aEll = a0 + stepSize * i;
                bEll = b0 + stepSize * i;
                aVals.Add(aEll);
                bVals.Add(bEll);
            }
            for (int i = 0; i < NumberSteps; ++i)
            {
                aEll = a0 + stepSize * (i + 0.5);
                bEll = b0 + stepSize * (i + 0.5);
                aValsMidPoints.Add(aEll);
                bValsMidPoints.Add(bEll);
            }

            double wei1, wei2, arg, argCosVal;
            for (int i = 0; i < NumberSteps; ++i)
            {
                arg = arguFactor * (aValsMidPoints[i] - a0);
                argCosVal = Math.Cos(arg);
                wei1 = 0.5 * (1.0 + argCosVal);
                wei2 = 0.5 * (1.0 - argCosVal);
                weight1.Add(wei1);
                weight2.Add(wei2);
            }

            //Parameters To Initialise
            //List<double> eVals, List<double> bVals, int NumberSteps, List<byte> pixRedOrig, List<byte> pixGreenOrig
            //List<byte> pixBlueOrig
            
            // Loop over the number of pixels
            for (int i = 0; i < height; ++i)
            {
                w2 = width * i;
                for (int j = 0; j < width; ++j)
                {
                    // This is the usual rotation formula, along with translation.
                    // I could have perhaps used the Transform feature of WPF.
                    xprime = (j - wb2) * cos + (i - hb2) * sin;
                    yprime = -(j - wb2) * sin + (i - hb2) * cos;

                    factor1 = 1.0 * Math.Abs(xprime) / aVals[0];
                    factor2 = 1.0 * Math.Abs(yprime) / bVals[0];
                    factor3 = 1.0 * Math.Abs(xprime) / aVals[NumberSteps];
                    factor4 = 1.0 * Math.Abs(yprime) / bVals[NumberSteps];


                    // Equations for the circle / ellipse. 
                    // "Potentials" are analogous to distances from the inner and outer boundaries
                    // of the two ellipses.
                    potential1 = factor1 * factor1 + factor2 * factor2 - 1.0;
                    potential2 = factor3 * factor3 + factor4 * factor4 - 1.0;
                    
                    w1 = w2 + j;

                    if (potential1 <= 0.0)
                    {
                        // Point is within the inner circle / ellipse / diamond
                        r = pixRedOrig[w1];
                        g = pixGreenOrig[w1];
                        b = pixBlueOrig[w1];
                    }
                    else if (potential2 >= 0.0)
                    {
                        // Point is outside the outer circle / ellipse / diamond
                        r = redBorder;
                        g = greenBorder;
                        b = blueBorder;
                    }
                    else
                    {
                        // Point is in between the outermost and innermost circles / ellipses / diamonds
                        int k, l;

                        for (k = 1; k < NumberSteps; ++k)
                        {
                            factor1 = Math.Abs(xprime) / aVals[k];
                            factor2 = Math.Abs(yprime) / bVals[k];

                            potential = factor1 * factor1 + factor2 * factor2 - 1.0;

                            if (potential < 0.0) break;
                        }
                        l = k - 1;
                        // The formulas where the weights are applied to the image, and border.
                        r = (byte)(pixRedOrig[w1] * weight1[l] + redBorder * weight2[l]);
                        g = (byte)(pixGreenOrig[w1] * weight1[l] + greenBorder * weight2[l]);
                        b = (byte)(pixBlueOrig[w1] * weight1[l] + blueBorder * weight2[l]);
                    }
                    pixRedModified[w1] = r;
                    pixGreenModified[w1] = g;
                    pixBlueModified[w1] = b;
                }
            }
        }
    }
}
