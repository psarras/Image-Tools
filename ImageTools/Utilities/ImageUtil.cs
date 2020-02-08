using AForge.Imaging;
using AForge.Imaging.Filters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public static class ImageUtil
    {
        public static Tuple<int, int> maxSize(List<Bitmap> imgs)
        {
            List<int> widths = new List<int>();
            List<int> heights = new List<int>();

            foreach (Bitmap b in imgs)
            {
                widths.Add(b.Width);
                heights.Add(b.Height);
            }

            widths.Sort();
            heights.Sort();

            return new Tuple<int, int>(widths.Last(), heights.Last());
        }

        public static List<IEnumerable<T>> splitList<T>(List<T> myListToSplit, int warp)
        {
            List<IEnumerable<T>> listOfLists = new List<IEnumerable<T>>();
            for (int i = 0; i < myListToSplit.Count(); i += warp)
            {
                listOfLists.Add(myListToSplit.Skip(i).Take(warp));
            }
            return listOfLists;
        }

        public static Interval getColorRange(int Value, int T)
        {
            int Vmin, Vmax;
            if (Value < 255 - T / 2.0)
            {
                Vmin = (int)Math.Max(0, Value - T / 2.0);
                Vmax = (int)Math.Min(255, Vmin + T);
            }
            else
            {
                Vmax = (int)Math.Min(255, Value + T / 2.0);
                Vmin = (int)Math.Max(0, Vmax - T);
            }
            return new Interval(Vmin, Vmax);
        }

        public static Tuple<Interval, Interval, Interval> getColorRange(Color C, int T)
        {
            Interval Rrange = getColorRange(C.R, T);
            Interval Grange = getColorRange(C.G, T);
            Interval Brange = getColorRange(C.B, T);

            return new Tuple<Interval, Interval, Interval>(Rrange, Grange, Brange);
        }

        public static Bitmap moveImage(Bitmap img, int x, int y)
        {
            Bitmap clone = new Bitmap(img.Width, img.Height, img.PixelFormat);
            clone.MakeTransparent(Color.Black);
            clone.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            Graphics g = Graphics.FromImage(clone);

            g.DrawImage(img, x, y);

            return clone;
        }

        public static Tuple<Bitmap, Bitmap, Bitmap, Bitmap> getARGBA(Bitmap img)
        {
            img = ImageUtil.convert(img, PixelFormat.Format32bppArgb);

            ExtractChannel myFilter = new ExtractChannel(RGB.R);
            Bitmap R = myFilter.Apply(img);

            myFilter = new ExtractChannel(RGB.G);
            Bitmap G = myFilter.Apply(img);

            myFilter = new ExtractChannel(RGB.B);
            Bitmap B = myFilter.Apply(img);

            myFilter = new ExtractChannel(RGB.A);
            Bitmap A = myFilter.Apply(img);
            return new Tuple<Bitmap, Bitmap, Bitmap, Bitmap>(R, G, B, A);
        }

        public static Bitmap convert(Bitmap img, PixelFormat format)
        {

            Bitmap clone = new Bitmap(img.Width, img.Height, format);

            Graphics gr = Graphics.FromImage(clone);

            gr.DrawImage(img, new Rectangle(0, 0, clone.Width, clone.Height));

            return clone;
        }

        public static Bitmap greyscale(Bitmap img)
        {
            return Grayscale.CommonAlgorithms.RMY.Apply(img);
        }

        public static int countNonBlackPixels(Bitmap img)
        {
            Bitmap formatIMG = convert(img, PixelFormat.Format32bppArgb);
            ImageStatistics stats = new ImageStatistics(formatIMG);
            return stats.PixelsCountWithoutBlack;


        }

        public static int countColorPixels(Bitmap img, Color C)
        {
            if (Color.Black != C)
            {
                Bitmap rangedIMG = ImageFilter.rangeRGBFilter(img, C, Color.Black, 1, true);
                return countNonBlackPixels(rangedIMG);
            }
            else
            {
                int totalPixels = img.Width * img.Height;
                return totalPixels - countNonBlackPixels(img);
            }
        }

        public static Dictionary<int, int> countColor(Bitmap img)
        {
            Dictionary<int, int> myDic = new Dictionary<int, int>();

            // Create a new bitmap.
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);

            // Lock the bitmap's bits.
            System.Drawing.Imaging.BitmapData bmpData =
              img.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
              img.PixelFormat);
            //PixelFormat px = img.PixelFormat;

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * img.Height;
            byte[] rgbByte = new byte[bytes];

            // get source bitmap pixel format size
            int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(img.PixelFormat);
            //int bytesPerPixel = bytes / (img.Height * img.Width);
            //Hardcoded
            bytesPerPixel /= 8;
            //BitConverter.ToString(byteArray);


            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbByte, 0, bytes);
            //int times = 0;
            // Set every third value to 255. A 24bpp bitmap will look red.
            //bmp.Height * 3
            try
            {
                for (int i = 0; i < rgbByte.Length; i += bytesPerPixel)
                {
                    //var key = ((rgbByte[i] << 8) + rgbByte[i + 1] << 8) + rgbByte[i + 2];
                    int key = -1;


                    switch (bytesPerPixel)
                    {
                        case 1: //8
                            key = rgbByte[i];
                            break;
                        case 2: //16
                            key = rgbByte[i] << 8 | rgbByte[i + 1];
                            break;
                        case 3: //24
                            key = rgbByte[i] << 16 | rgbByte[i + 1] << 8 | rgbByte[i + 2];
                            break;
                        case 4: //32
                            key = rgbByte[i] << 24 | rgbByte[i + 1] << 16 | rgbByte[i + 2] << 8 | rgbByte[i + 3];
                            break;


                        default:
                            break;
                    }

                    if (key != -1 && !myDic.ContainsKey(key))
                    {
                        myDic.Add(key, 0);
                    }
                    myDic[key] += 1;
                }
            }
            finally
            {
                img.UnlockBits(bmpData);
            }

            // Copy the RGB values back to the bitmap
            //System.Runtime.InteropServices.Marshal.Copy(rgbByte, 0, ptr, bytes);
            return myDic;
        }

        //Helper Method
        public static Color[] getPixelsArray(Bitmap img)
        {
            int numPixels = img.Width * img.Height;
            Color[] pixels = new Color[numPixels];

            int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(img.PixelFormat) / 8;

            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            Bitmap bmg = img.Clone(rect, img.PixelFormat);

            // Lock the bitmap's bits.
            System.Drawing.Imaging.BitmapData bmpData =
              bmg.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
              bmg.PixelFormat);

            // Get the address of the first line.

            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmg.Height;
            byte[] rgbByte = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbByte, 0, bytes);

            int counter = 0;
            for (int i = 0; i < rgbByte.Length; i += bytesPerPixel)
            {
                int grey, red, green, blue, alpha;
                switch (bytesPerPixel)
                {
                    case 1: //8
                        //key = rgbByte[i];
                        grey = (int)rgbByte[i];
                        pixels[counter] = Color.FromArgb(grey, grey, grey);
                        break;
                    case 2: //16
                        grey = (int)rgbByte[i + 1];
                        alpha = (int)rgbByte[i];
                        pixels[counter] = Color.FromArgb(alpha, Color.FromArgb(grey, grey, grey));
                        break;
                    case 3: //24
                        red = (int)rgbByte[i + 2];
                        green = (int)rgbByte[i + 1];
                        blue = (int)rgbByte[i];
                        pixels[counter] = Color.FromArgb(red, green, blue);
                        break;
                    case 4: //32
                        alpha = (int)rgbByte[i + 3];
                        red = (int)rgbByte[i + 2];
                        green = (int)rgbByte[i + 1];
                        blue = (int)rgbByte[i];
                        pixels[counter] = Color.FromArgb(alpha, red, green, blue);
                        //key = rgbByte[i] << 24 | rgbByte[i + 1] << 16 | rgbByte[i + 2] << 8 | rgbByte[i + 3];
                        break;
                    default:
                        break;

                        
                }

                counter++;

            }

            return pixels;
        }

        static Bitmap setPixelsArray(Color[] pixels, Bitmap img)
        {
            return null;
        }

        public static Color byteGrey(int col)
        {
            byte grey = (byte)(col >> 0);

            return Color.FromArgb(grey, grey, grey);
        }

        public static Color byteGreyAlpha(int col)
        {
            byte a = (byte)(col >> 0);
            byte grey = (byte)(col >> 8);

            return Color.FromArgb(a, grey, grey, grey);
        }


        public static Color byteARGB(int col)
        {

            byte a = (byte)(col >> 0);
            byte r = (byte)(col >> 8);
            byte g = (byte)(col >> 16);
            byte b = (byte)(col >> 24);

            return Color.FromArgb(a, r, g, b);

        }

        public static Color byteARG(int col)
        {

            byte r = (byte)(col >> 0);
            byte g = (byte)(col >> 8);
            byte b = (byte)(col >> 16);


            return Color.FromArgb(r, g, b);
        }

    }
}
