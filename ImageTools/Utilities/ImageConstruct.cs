using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using AForge.Math.Geometry;
using ImageTools.Draw;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
//using static System.Windows.Forms.DataFormats;

namespace ImageTools.Utilities
{
    public static class ImageConstruct
    {
        public static Bitmap ColorImage(int Width, int Height, int DPIx, int DPIy, Color color)
        {
            Bitmap img = new Bitmap(Width, Height);
            img.SetResolution(DPIx, DPIy);

            Graphics gIMG = Graphics.FromImage(img);
            System.Drawing.SolidBrush myBrush = new SolidBrush(color);

            gIMG.FillRectangle(myBrush, new Rectangle(0, 0, img.Width, img.Height));

            return img;
        }

        public static Bitmap MatchColorImage(Bitmap img, Color color)
        {
            return ImageConstruct.ColorImage(img.Width, img.Height, (int)img.HorizontalResolution, (int)img.VerticalResolution, color);
        }

        //Ellipsoid Gradient as well
        public static Bitmap CreateGradient(Bitmap img, Color Cs, Color Ce, float angle, float factor)
        {
            Bitmap canvas = MatchColorImage(img, Color.White);
            canvas = ImageUtil.convert(canvas, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(canvas);
            Rectangle myRect = new Rectangle(0, 0, canvas.Width, canvas.Height);
            myRect.Inflate(1, 1);
            LinearGradientBrush myBrush = new LinearGradientBrush(myRect, Cs, Ce, angle, true);

            Blend myBlend = new Blend();
            
            float[] relativeIntensities = { 0.0f, 0.5f, 1.0f };
            float[] relativePositions = { 0.0f, factor, 1.0f };
            myBlend.Positions = relativePositions;
            myBlend.Factors = relativeIntensities;

            myBrush.Blend = myBlend;

            g.FillRectangle(myBrush, 0, 0, canvas.Width, canvas.Height);

            return canvas;
        }

        //Ellipsoid Gradient as well
        //public static Bitmap CreateGradient2(Bitmap img, Color Cs, Color Ce, PointF s, PointF e, float factor)
        //{
        //    Bitmap canvas = MatchColorImage(img, Color.White);
        //    canvas = ImageUtil.convert(canvas, PixelFormat.Format32bppArgb);
        //    Graphics g = Graphics.FromImage(canvas);
        //    Rectangle myRect = new Rectangle(0, 0, canvas.Width, canvas.Height);
        //    myRect.Inflate(1, 1);
        //    //LinearGradientMode.
        //    LinearGradientBrush myBrush = new LinearGradientBrush(s, e, Cs, Ce);

        //    Blend myBlend = new Blend();

        //    float[] relativeIntensities = { 0.0f, 0.5f, 1.0f };
        //    float[] relativePositions = { 0.0f, factor, 1.0f };
        //    myBlend.Positions = relativePositions;
        //    myBlend.Factors = relativeIntensities;

        //    myBrush.Blend = myBlend;

        //    g.FillRectangle(myBrush, 0, 0, canvas.Width, canvas.Height);

        //    return canvas;
        //}

        public static Bitmap CreateGradient(Bitmap img, List<Color> C)
        {
            List<Bitmap> GradientIMGs = new List<Bitmap>();

            Bitmap gradientSize = new Bitmap((int)(img.Width / (C.Count - 1)), (int)(img.Height / (C.Count - 1)));

            for (int i = 0; i < C.Count - 1; i++)
            {
                GradientIMGs.Add(CreateGradient(gradientSize, C[i], C[i + 1], 0, 0.5f));
            }

            Bitmap stitch = ImageComposition.ArrayImages(GradientIMGs, 1, true, 0);

            return new Bitmap(stitch, img.Size);
        }

        public static Bitmap CreateBoundaryGradient(Bitmap img, Color Cc, List<Color> Ce, float factor)
        {
            Bitmap canvas = MatchColorImage(img, Color.White);
            canvas = ImageUtil.convert(canvas, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(canvas);
            Rectangle myRect = new Rectangle(0, 0, canvas.Width, canvas.Height);

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(myRect);
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            //pthGrBrush.SetSigmaBellShape(factor);
            pthGrBrush.SetBlendTriangularShape(factor);

            pthGrBrush.CenterColor = Cc;
            Color[] colors = Ce.ToArray<Color>();
            pthGrBrush.SurroundColors = colors;

            g.FillRectangle(pthGrBrush, myRect);
            return canvas;
        }



        public static Tuple<Bitmap, Bitmap, Bitmap, Bitmap, List<Bitmap>> blob(Bitmap img, Interval w, Interval h, Boolean couple)
        {
            Bitmap sourceImage = (Bitmap)(img);

            ////////////////////////////////////////////////////

            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = (int)h.Min;
            blobCounter.MaxHeight = (int)h.Max;
            blobCounter.MinWidth = (int)w.Min;
            blobCounter.MaxWidth = (int)w.Max;

            blobCounter.CoupledSizeFiltering = couple;

            //blobCounter.BackgroundThreshold.

            blobCounter.ProcessImage(sourceImage);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            //bitmap.UnlockBits(sourceImage);

            ////////////////////////////////////////////////////
            Bitmap newBlobImg = (Bitmap)(sourceImage).Clone();
            // create filter
            BlobsFiltering filterBlob = new BlobsFiltering();
            // configure filter
            filterBlob.CoupledSizeFiltering = couple;
            filterBlob.MinHeight = (int)h.Min;
            filterBlob.MaxHeight = (int)h.Max;
            filterBlob.MinWidth = (int)w.Min;
            filterBlob.MaxWidth = (int)w.Max;
            // apply the filter
            newBlobImg = filterBlob.Apply(newBlobImg);

            ////////////////////////////////////////////////////

            Bitmap rectImage = (Bitmap)(newBlobImg).Clone();

            GrayscaleToRGB convertToColor = new GrayscaleToRGB();
            if (rectImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                rectImage = convertToColor.Apply(newBlobImg);
            }

            Graphics g = Graphics.FromImage(rectImage);

            Pen myPen = new Pen(Color.Red, 1);

            List<Bitmap> blobIMGs = new List<Bitmap>();

            for (int i = 0; i < blobs.Length; i++)
            {
                blobIMGs.Add((Bitmap)(newBlobImg).Clone(blobs[i].Rectangle, PixelFormat.Format32bppArgb));
                g.DrawRectangle(myPen, blobs[i].Rectangle);

            }

            ////////////////////////////////////////////////////


            Bitmap colorImage = (Bitmap)(newBlobImg).Clone();

            ConnectedComponentsLabeling filter = new ConnectedComponentsLabeling();
            // apply the filter
            colorImage = filter.Apply(colorImage);
            //////////////////////////////////////////////////

            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            Bitmap shapeImage = (Bitmap)(newBlobImg).Clone();
            Graphics gg = Graphics.FromImage(shapeImage);
            Pen yellowPen = new Pen(Color.Yellow, 2); // circles
            Pen redPen = new Pen(Color.Red, 2);       // quadrilateral
            Pen brownPen = new Pen(Color.Brown, 2);   // quadrilateral with known sub-type
            Pen greenPen = new Pen(Color.Green, 2);   // known triangle
            Pen bluePen = new Pen(Color.Blue, 2);     // triangle

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;

                // is circle ?
                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    gg.DrawEllipse(yellowPen,
                    (float)(center.X - radius), (float)(center.Y - radius),
                    (float)(radius * 2), (float)(radius * 2));
                }
                else
                {
                    List<IntPoint> corners;

                    // is triangle or quadrilateral
                    if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                    {
                        // get sub-type
                        PolygonSubType subType = shapeChecker.CheckPolygonSubType(corners);

                        Pen pen;

                        if (subType == PolygonSubType.Unknown)
                        {
                            pen = (corners.Count == 4) ? redPen : bluePen;
                        }
                        else
                        {
                            pen = (corners.Count == 4) ? brownPen : greenPen;
                        }

                        gg.DrawPolygon(pen, ToPointsArray(corners));
                    }
                }
            }



            return new Tuple<Bitmap, Bitmap, Bitmap, Bitmap, List<Bitmap>>(newBlobImg, colorImage, rectImage, shapeImage, blobIMGs);
        }

        // Conver list of AForge.NET's points to array of .NET points
        private static System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }

        public static Bitmap CreateCloudTexture(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Texturer myFilter = new Texturer(new CloudsTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateTextileTexture(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Texturer myFilter = new Texturer(new TextileTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateWoodTexture(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Texturer myFilter = new Texturer(new WoodTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateLabyrinthTexture(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Texturer myFilter = new Texturer(new LabyrinthTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateMarbleTexture(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Texturer myFilter = new Texturer(new MarbleTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateTexturedMerge(Bitmap img, double firstLevel, double preserveLevel)
        {
            Bitmap sourceImage = ImageUtil.convert(img, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            TexturedMerge myTools = new TexturedMerge(new MarbleTexture());

            Texturer myFilter = new Texturer(new MarbleTexture(), firstLevel, preserveLevel); //Needs Expand

            return myFilter.Apply(sourceImage);
        }

        public static Bitmap CreateTexture(Bitmap img, double firstLevel, double preserveLevel, int texture)
        {
            if (texture == Filters.textures["Clouds"])
            {
                return CreateCloudTexture(img, firstLevel, preserveLevel);
            }
            else if (texture == Filters.textures["Textile"])
            {
                return CreateTextileTexture(img, firstLevel, preserveLevel);
            }
            else if (texture == Filters.textures["Wood"])
            {
                return CreateWoodTexture(img, firstLevel, preserveLevel);
            }
            else if (texture == Filters.textures["Marble"])
            {
                return CreateMarbleTexture(img, firstLevel, preserveLevel);
            }
            else if (texture == Filters.textures["Labyrinth"])
            {
                return CreateLabyrinthTexture(img, firstLevel, preserveLevel);
            }
            return null;
        }

        public static void DrawLine(Bitmap img)
        {
            Graphics gIMG = Graphics.FromImage(img);
            //gIMG.DrawEllipse()
            //gIMG.DrawArc()
            //Pen p = new Pen(Co)

        }

        public static Bitmap DrawShape(Bitmap img, Pen p)
        {

            return null;
        }


        public static Bitmap DrawEllipse(Bitmap img, Pen p, Rectangle rec)
        {
            Bitmap canvas = ImageUtil.convert(img, PixelFormat.Format32bppArgb);
            Graphics gIMG = Graphics.FromImage(canvas);
            //Pen pp = new Pen()
            Pen pp = new Pen(new SolidBrush(Color.Black), 10);
            gIMG.DrawEllipse(p, rec);
            //gIMG.DrawArc
            //gIMG.DrawCurve
            //gIMG.DrawRectangle
            //gIMG.DrawPolygon
            //gIMG.DrawPath
            //gIMG.DrawPath(pp, new GraphicsPath())
            return canvas;
        }

        public static Bitmap DrawEllipses(Bitmap img, Pen p, List<Rectangle> rec)
        {
            Bitmap canvas = ImageUtil.convert(img, PixelFormat.Format32bppArgb);
            Graphics gIMG = Graphics.FromImage(canvas);
            //Pen pp = new Pen()
            Pen pp = new Pen(new SolidBrush(Color.Black), 10);

            foreach (var r in rec)
            {
                gIMG.DrawEllipse(p, r);
            }
            //gIMG.DrawArc
            //gIMG.DrawCurve
            //gIMG.DrawRectangle
            //gIMG.DrawPolygon
            //gIMG.DrawPath
            //gIMG.DrawPath(pp, new GraphicsPath())
            return canvas;
        }

        public static Bitmap DrawShape(Bitmap img, List<Shape2D> shapes)
        {
            Bitmap canvas = ImageUtil.convert(img, PixelFormat.Format32bppArgb);
            Graphics gIMG = Graphics.FromImage(canvas);

            foreach (var s in shapes)
            {
                s.draw(gIMG);
            }
            //gIMG.DrawArc
            //gIMG.DrawCurve
            //gIMG.DrawRectangle
            //gIMG.DrawPolygon
            //gIMG.DrawPath
            //gIMG.DrawPath(pp, new GraphicsPath())
            return canvas;

        }

    }
}
