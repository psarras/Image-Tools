using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Textures;
using AForge.Imaging.ColorReduction;

namespace ImageTools.Utilities
{
    public static class ImageFilter
    {
        public static Bitmap opacityFilter(Bitmap img, float Value)
        {
            //create a Bitmap the size of the image provided
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            //create a graphics object from the image
            Graphics gfx = Graphics.FromImage(bmp);
            //create a color matrix object
            ColorMatrix matrix = new ColorMatrix();
            //set the opacity
            matrix.Matrix33 = Convert.ToSingle(Value);
            //create image attributes
            ImageAttributes attributes = new ImageAttributes();
            //set the color(opacity) of the image
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            //now draw the image
            gfx.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attributes);

            return bmp;
        }

        public static Bitmap rangeRGBFilter(Bitmap img, Color C, Color Ch, int T, Boolean Dir)
        {
            Tuple<Interval, Interval, Interval> ranges = ImageUtil.getColorRange(C, T);
            return RGBFilter(img, ranges.Item1, ranges.Item2, ranges.Item3, Ch, Dir);
        }

        public static Bitmap RGBFilter(Bitmap img, Interval R, Interval G, Interval B, Color C, Boolean Dir)
        {

            img = ImageUtil.convert(img, PixelFormat.Format32bppArgb);

            ColorFiltering colorFilter = new ColorFiltering();

            colorFilter.Red = new IntRange((int)R.Min, (int)R.Max);
            colorFilter.Green = new IntRange((int)G.Min, (int)G.Max);
            colorFilter.Blue = new IntRange((int)B.Min, (int)B.Max);

            colorFilter.FillOutsideRange = Dir;
            colorFilter.FillColor = new RGB(C);
            Bitmap filteredImage = colorFilter.Apply(img);

            return filteredImage;
        }

        public static Bitmap BlurImage(Bitmap img, float sigma, int range)
        {
            IFilter myFilter = new GaussianBlur(sigma, range);
            return myFilter.Apply(img);
        }

        public static Bitmap DropShadowIMG(Bitmap img, int RangeX, int RangeY, Color C, float sigma, int range, float Opacity)
        {
            Bitmap coloredIMG = ImageConstruct.MatchColorImage(img, C);
            Bitmap getAlpha = ImageUtil.getARGBA(img).Item4;
            coloredIMG = ImageMultiFilter.RGBA_replaceChannel(coloredIMG, null, null, null, getAlpha);

            Bitmap movedIMG = ImageUtil.moveImage(coloredIMG, RangeX, RangeY);
            Bitmap BluredIMG = BlurImage(movedIMG, sigma, range);
            
            //Bitmap intersectIMG = ImageMultiFilter.operationImages(BluredIMG, coloredIMG, Operations.operations["Intersect"]);
            Bitmap OpacityIMG = opacityFilter(BluredIMG, Opacity);

            return OpacityIMG;
        }
        
        public static Tuple<Bitmap, Bitmap> DropShadow(Bitmap img, int RangeX, int RangeY, Color C, float sigma, int range, float Opacity)
        {
            Bitmap DropShadowImage = DropShadowIMG(img, RangeX, RangeY, C, sigma, range, Opacity);

            List<Bitmap> imgsToOverlay = new List<Bitmap>();
            imgsToOverlay.Add(DropShadowImage);
            imgsToOverlay.Add(img);

            return new Tuple<Bitmap, Bitmap>(ImageMultiFilter.OverlayImages(imgsToOverlay, 1, 1), DropShadowImage);
        }
        //Colorize Rename, Colorize Using IMG
        public static Bitmap TintImage(Bitmap img, Color C, float Value)
        {
            Bitmap coloredIMG = ImageConstruct.MatchColorImage(img, C);
            Bitmap greyScale = Grayscale.CommonAlgorithms.RMY.Apply(img);
            Bitmap getAlpha = ImageUtil.getARGBA(greyScale).Item1;
            coloredIMG = ImageMultiFilter.RGBA_replaceChannel(coloredIMG, null, null, null, getAlpha);
            coloredIMG = opacityFilter(coloredIMG, Value);

            List<Bitmap> imgsToOverlay = new List<Bitmap>();
            imgsToOverlay.Add(img);
            imgsToOverlay.Add(coloredIMG);
            return ImageMultiFilter.OverlayImages(imgsToOverlay, 1, 1);
        }

        public static Bitmap FilterImage(Bitmap img, int filter)
        {
            Bitmap sourceImage = img;
            
            sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IFilter myFilter;
            Bitmap filteredImage = sourceImage;

            if (filter == Filters.filters["Greyscale"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                filteredImage = sourceImage;
            }
            else if (filter == Filters.filters["Sepia"])
            {
                myFilter = new Sepia();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Invert"])
            {
                sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                myFilter = new Invert();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["RotateChannel"])
            {
                myFilter = new RotateChannels();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Threshold"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                myFilter = new Threshold();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["FloydFilter"])
            {
                FloydSteinbergColorDithering myReduction = new FloydSteinbergColorDithering();
                filteredImage = myReduction.Apply(sourceImage);
            }
            else if (filter == Filters.filters["OrderedDithering"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                myFilter = new OrderedDithering();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Sharpen"])
            {
                myFilter = new Sharpen();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["DifferenceEdgeDetector"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                myFilter = new DifferenceEdgeDetector();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["HomogenityEdgeDetector"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                myFilter = new HomogenityEdgeDetector();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Sobel"])
            {
                sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                myFilter = new SobelEdgeDetector();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Jitter"])
            {
                myFilter = new Jitter(); //Needs Expand
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["OilPainting"])
            {
                sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                myFilter = new OilPainting(); //Needs Expand
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["TextureFiltering"])
            {
                sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                myFilter = new Texturer(new TextileTexture(), 1.0, 0.8); //Needs Expand
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Median"])
            {
                sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                myFilter = new Median();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Mean"])
            {
                myFilter = new Mean();
                filteredImage = myFilter.Apply(sourceImage);
            }
            else if (filter == Filters.filters["Blur"])
            {
                myFilter = new GaussianBlur();
                filteredImage = myFilter.Apply(sourceImage);
            }

            //Console.Write(filteredImage.PixelFormat.ToString());
            //Console.Write(sourceImage.PixelFormat.ToString());
            filteredImage = ImageUtil.convert(filteredImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            return filteredImage;
        }



    }
}
