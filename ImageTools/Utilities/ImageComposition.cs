using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    public static class ImageComposition
    {
        public static Bitmap ArrayImages(List<Bitmap> img, int align, Boolean dir, int pad)
        {
            List<Bitmap> oriImg = new List<Bitmap>();

            int xSize = 0;
            int ySize = 0;

            Bitmap sample = (Bitmap)(img[0]);
            float xDPI = sample.HorizontalResolution;
            float yDPI = sample.VerticalResolution;

            foreach (Bitmap b in img)
            {
                oriImg.Add(b);
                if (dir)
                {
                    xSize += b.Width;
                    if (b.Height > ySize) ySize = b.Height;
                }
                else
                {
                    ySize += b.Height;
                    if (b.Width > xSize) xSize = b.Width;
                }
                //Print("Got Image with Size: " + b.Width.ToString() + ", " + b.Height.ToString());

            }

            if (dir)
            {
                xSize += pad * (oriImg.Count - 1);
            }
            else
            {
                ySize += pad * (oriImg.Count - 1);
            }

            //Print("Size: " + xSize.ToString() + ", " + ySize.ToString());

            Bitmap myImg = new Bitmap(xSize, ySize);
            myImg.SetResolution(xDPI, yDPI);
            Graphics gIMG = Graphics.FromImage(myImg);


            int Xpos = 0;
            int Ypos = 0;

            foreach (Bitmap b in oriImg)
            {

                if (dir)
                {
                    switch (align)
                    {
                        case 1:
                            Ypos = (int)((myImg.Height - b.Height) / 2);
                            break;
                        case 2:
                            Ypos = (int)(myImg.Height - b.Height);
                            break;
                        default:
                            Ypos = 0;
                            break;
                    }
                }
                else
                {
                    switch (align)
                    {
                        case 1:
                            Xpos = (int)((myImg.Width - b.Width) / 2);
                            break;
                        case 2:
                            Xpos = (int)(myImg.Width - b.Width);
                            break;
                        default:
                            Xpos = 0;
                            break;
                    }
                }
                //Print("Place Image At: " + Xpos.ToString() + ", " + Ypos.ToString());
                //Print("Image Size: " + b.Width.ToString() + ", " + b.Height.ToString());
                gIMG.DrawImage(b, Xpos, Ypos);

                if (dir)
                {
                    Xpos += b.Width + pad;
                }
                else
                {
                    Ypos += b.Height + pad;
                }
            }


            return myImg;
        }

        public static List<Bitmap> ArrayImages(List<IEnumerable<Bitmap>> listOfLists, int align, Boolean dir, int pad)
        {
            List<Bitmap> mergedBitmaps = new List<Bitmap>();

            foreach (List<Bitmap> myList in listOfLists)
            {
                mergedBitmaps.Add(ImageComposition.ArrayImages(myList, align, dir, pad));
            }

            return mergedBitmaps;
        }
        
        public static Bitmap AddTextImage(Bitmap img, string text, int size, int Align, Color C, string font)
        {
            Bitmap myImg = new Bitmap((Bitmap)(img));

            Graphics gIMG = Graphics.FromImage(myImg);

            System.Drawing.SolidBrush myBrush = new SolidBrush(C);

            StringFormat myFormat = new StringFormat(StringFormatFlags.NoClip);

            int xLoc = 0;
            int yLoc = 0;

            switch (Align)
            {
                case 0:
                    xLoc = 0;
                    yLoc = 0;
                    myFormat.Alignment = StringAlignment.Near;
                    myFormat.LineAlignment = StringAlignment.Near;
                    break;
                case 1:
                    xLoc = myImg.Width / 2;
                    myFormat.Alignment = StringAlignment.Center;
                    myFormat.LineAlignment = StringAlignment.Near;
                    yLoc = 0;
                    break;
                case 2:
                    xLoc = myImg.Width;
                    yLoc = 0;
                    myFormat.Alignment = StringAlignment.Far;
                    myFormat.LineAlignment = StringAlignment.Near;
                    break;
                case 3:
                    xLoc = 0;
                    yLoc = myImg.Height / 2;
                    myFormat.Alignment = StringAlignment.Near;
                    myFormat.LineAlignment = StringAlignment.Center;
                    break;
                case 4:
                    xLoc = myImg.Width / 2;
                    yLoc = myImg.Height / 2;
                    myFormat.Alignment = StringAlignment.Center;
                    myFormat.LineAlignment = StringAlignment.Center;
                    break;
                case 5:
                    xLoc = myImg.Width;
                    yLoc = myImg.Height / 2;
                    myFormat.Alignment = StringAlignment.Far;
                    myFormat.LineAlignment = StringAlignment.Center;
                    break;
                case 6:
                    xLoc = 0;
                    yLoc = myImg.Height;
                    myFormat.Alignment = StringAlignment.Near;
                    myFormat.LineAlignment = StringAlignment.Far;
                    break;
                case 7:
                    xLoc = myImg.Width / 2;
                    yLoc = myImg.Height;
                    myFormat.Alignment = StringAlignment.Center;
                    myFormat.LineAlignment = StringAlignment.Far;
                    break;
                case 8:
                    xLoc = myImg.Width;
                    yLoc = myImg.Height;
                    myFormat.Alignment = StringAlignment.Far;
                    myFormat.LineAlignment = StringAlignment.Far;
                    break;
                default:
                    xLoc = 0;
                    yLoc = 0;
                    myFormat.Alignment = StringAlignment.Near;
                    break;
            }



            System.Drawing.Font myFont = new System.Drawing.Font(font, size);

            gIMG.DrawString(text, myFont, myBrush, xLoc, yLoc, myFormat);

            return myImg;
        }

        public static Bitmap GridImages(List<Bitmap> imgs, int warp, int pad, Boolean dir)
        {
            //Split Lists into multiple lists
            List<IEnumerable<Bitmap>> listOfLists = ImageUtil.splitList(imgs, warp);
            //List To place the images for One Direction
            List<Bitmap> firstPassBitmaps = new List<Bitmap>();
            Boolean revDir = !dir;
            //For each List
            foreach (IEnumerable<Bitmap> myList in listOfLists)
            {
                List<Bitmap> CastList = myList.ToList();
                //Change their Size To Match
                List<Bitmap> myListMod = ImageShape.resizeImage(CastList, revDir);
                firstPassBitmaps.Add(ArrayImages(myListMod, 1, dir, pad));
            }

            
            //Repeat with the other direction
            List<Bitmap> firstPassBitmapsMod = ImageShape.resizeImage(firstPassBitmaps, dir);
            Bitmap finalBitmap = ArrayImages(firstPassBitmapsMod, 1, revDir, pad);

            return finalBitmap;
        }

        public static Bitmap GridImages(List<Bitmap> imgs, int warp, int pad, Boolean dir, Color C)
        {
            Bitmap gridImage = GridImages(imgs, warp, pad, dir);

            Bitmap padImage = ImageShape.PaddingImage(gridImage, pad, pad, pad, pad);

            return AddBackgroundImage(padImage, C);
        }

        public static Bitmap AddBackgroundImage(Bitmap img, Color C)
        {
            Bitmap background = ImageConstruct.MatchColorImage(img, C);

            List<Bitmap> imagesToOverlay = new List<Bitmap>();
            imagesToOverlay.Add(background);
            imagesToOverlay.Add(img);

            return ImageMultiFilter.OverlayImages(imagesToOverlay, 1, 1);
        }

        public static Bitmap AddTitle(Bitmap img, string text, float size, int Align, Color Ftext, Color Btext, string font)
        {
            Bitmap baseIMG = ImageConstruct.ColorImage(img.Width, (int)(size * img.Height), 
                (int)img.HorizontalResolution, (int)img.VerticalResolution, Btext);

            int align = 5;
            if (Align == 0 || Align == 3) align = 3;
            if (Align == 1 || Align == 4) align = 4;
            if (Align == 2 || Align == 5) align = 5;

            Bitmap baseText = AddTextImage(baseIMG, text, (int)(baseIMG.Height / 2.0f), align, Ftext, font);
            List<Bitmap> imgsToJoin = new List<Bitmap>();

            if (Align <= 2)
            {
                imgsToJoin.Add(baseText);
                imgsToJoin.Add(img);
            } else
            {
                imgsToJoin.Add(img);
                imgsToJoin.Add(baseText);
            }

            return ArrayImages(imgsToJoin, 1, false, 0);
        }

        public static List<Bitmap> AddNumbering(List<Bitmap> img, int size, int Align, Color Ftext, string font)
        {
            List<Bitmap> imgWithNumbers = new List<Bitmap>();
            PointF p = new PointF(0, 0);
            int align = 0;
            if (Align == 0) align = 0;
            if (Align == 1) align = 2;
            
            if (Align == 2) align = 6;
            if (Align == 3) align = 8;

            for (int i = 0; i < img.Count; i++)
            {
                int s = (int)((size / 100.0f) * img[i].Height);
                imgWithNumbers.Add(AddTextImage(img[i], i.ToString(), s, align, Ftext, font));
            }
            return imgWithNumbers;
        }


    }
}

