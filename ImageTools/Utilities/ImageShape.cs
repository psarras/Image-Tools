using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public static class ImageShape
    {
        public static Bitmap RotateFlipImage(Bitmap img, int R, int F)
        {
            RotateFlipType myRotationFlip = ImageShape.getRotationFlip(R, F);
            img.RotateFlip(myRotationFlip);
            return img;
        }


        private static RotateFlipType getRotationFlip(int R, int F)
        {

            RotateFlipType myRotationFlip = RotateFlipType.RotateNoneFlipNone;

            if (R == 90)
            {
                if (F == 1)
                {
                    myRotationFlip = RotateFlipType.Rotate90FlipX;
                }
                else if (F == 2)
                {
                    myRotationFlip = RotateFlipType.Rotate90FlipY;
                }
                else if (F == 3)
                {
                    myRotationFlip = RotateFlipType.Rotate90FlipXY;
                }
                else
                {
                    myRotationFlip = RotateFlipType.Rotate90FlipNone;
                }

            }

            if (R == 180)
            {
                if (F == 1)
                {
                    myRotationFlip = RotateFlipType.Rotate180FlipX;
                }
                else if (F == 2)
                {
                    myRotationFlip = RotateFlipType.Rotate180FlipY;
                }
                else if (F == 3)
                {
                    myRotationFlip = RotateFlipType.Rotate180FlipXY;
                }
                else
                {
                    myRotationFlip = RotateFlipType.Rotate180FlipNone;
                }

            }

            else if (R == 270)
            {
                if (F == 1)
                {
                    myRotationFlip = RotateFlipType.Rotate270FlipX;
                }
                else if (F == 2)
                {
                    myRotationFlip = RotateFlipType.Rotate270FlipY;
                }
                else if (F == 3)
                {
                    myRotationFlip = RotateFlipType.Rotate270FlipXY;
                }
                else
                {
                    myRotationFlip = RotateFlipType.Rotate270FlipNone;
                }
            }

            else
            {
                if (F == 1)
                {
                    myRotationFlip = RotateFlipType.RotateNoneFlipX;
                }
                else if (F == 2)
                {
                    myRotationFlip = RotateFlipType.RotateNoneFlipY;
                }
                else if (F == 3)
                {
                    myRotationFlip = RotateFlipType.RotateNoneFlipXY;
                }
                else
                {
                    myRotationFlip = RotateFlipType.RotateNoneFlipNone;
                }
            }


            //Print(myRotationFlip.ToString());

            return myRotationFlip;
        }


        public static Bitmap resizeImage(Bitmap img, int size, Boolean dir)
        {

            //Equation
            //img.Width * newHeight = newWidth * img.Height

            int newHeight = (int)(size * img.Height / (double)img.Width);
            int newWidth = (int)(img.Width * size / (double)img.Height);


            Bitmap myImg;

            if (dir)
            {
                myImg = new Bitmap(img, new Size(size, newHeight));
            }
            else
            {
                myImg = new Bitmap(img, new Size(newWidth, size));
            }

            return myImg;
        }

        public static List<Bitmap> resizeImage(List<Bitmap> imgs, int size, Boolean dir)
        {
            List<Bitmap> newImgs = new List<Bitmap>();

            foreach(Bitmap b in imgs)
            {
                newImgs.Add(resizeImage(b, size, dir));
            }

            return newImgs;
        }

        /// <summary>
        /// Resize Images based on their max dimension, 
        /// either Width, or height depending on dir
        /// </summary>
        /// <param name="imgs"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static List<Bitmap> resizeImage(List<Bitmap> imgs, Boolean dir)
        {
            List<Bitmap> myListMod = new List<Bitmap>();
            Tuple<int, int> sizes = ImageUtil.maxSize(imgs);
            Boolean revDir = !dir;
            if (dir)
            {
                myListMod = ImageShape.resizeImage(imgs, sizes.Item1, dir);
            }
            else
            {
                myListMod = ImageShape.resizeImage(imgs, sizes.Item2, dir);
            }
            return myListMod;
        }

        public static Bitmap BestFitImage(Bitmap img, int width, int height)
        {
            Bitmap myImg = (Bitmap)img;

            Bitmap idealSize = new Bitmap(width, height);

            Graphics gIMG = Graphics.FromImage(idealSize);

            int srcWidth = 0;
            int srcHeight = 0;


            srcWidth = myImg.Width;
            srcHeight = (int)(srcWidth * (idealSize.Height / (double)idealSize.Width));
            if (srcHeight > myImg.Height)
            {
                srcHeight = myImg.Height;
                srcWidth = (int)(srcHeight * (idealSize.Width / (double)idealSize.Height));
            }

            int sX = (int)((myImg.Width - srcWidth) / 2.0);
            int sY = (int)((myImg.Height - srcHeight) / 2.0);

            Rectangle scrRect = new Rectangle(sX, sY, srcWidth, srcHeight);
            Rectangle destRect = new Rectangle(0, 0, width, height);

            gIMG.DrawImage(myImg, destRect, scrRect, GraphicsUnit.Pixel);

            return idealSize;
        }

        public static List<Bitmap> BestFitImage(List<Bitmap> imgs, int width, int height)
        {
            List<Bitmap> newImgs = new List<Bitmap>();

            foreach(Bitmap b in imgs)
            {
                newImgs.Add(BestFitImage(b, width, height));
            }

            return newImgs;

        }

        public static Bitmap CropImage(Bitmap img, int L, int R, int T, int B)
        {
            Bitmap resizedImg = new Bitmap(img.Width - L - R, img.Height - T - B);
            Graphics gIMG = Graphics.FromImage(resizedImg);

            Rectangle scrRect = new Rectangle(L, T, resizedImg.Width, resizedImg.Height);
            Rectangle destRect = new Rectangle(0, 0, resizedImg.Width, resizedImg.Height);

            gIMG.DrawImage(img, destRect, scrRect, GraphicsUnit.Pixel);

            return resizedImg;
        }

        public static Bitmap PaddingImage(Bitmap img, int L, int R, int T, int B)
        {
            Bitmap myImg = new Bitmap(img.Width + L + R, img.Height + T + B);
            Graphics gIMG = Graphics.FromImage(myImg);

            gIMG.DrawImage(img, new System.Drawing.Point(L, T));

            return myImg;
        }
    }

}
