using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using AForge.Imaging.Textures;

namespace ImageTools.Utilities
{
    public static class ImageMultiFilter
    {
        public static Bitmap OverlayImages(List<Bitmap> img, int Hor, int Ver)
        {
            List<Bitmap> oriImg = new List<Bitmap>();
            
            int xSize = 0;
            int ySize = 0;

            foreach (Bitmap b in img)
            {
                oriImg.Add(b);
                if (b.Height > ySize) ySize = b.Height;
                if (b.Width > xSize) xSize = b.Width;
            }

            Bitmap myImg = new Bitmap(xSize, ySize);
            myImg.SetResolution(oriImg[0].HorizontalResolution, oriImg[0].VerticalResolution);

            Graphics gIMG = Graphics.FromImage(myImg);

            int Xpos = 0;
            int Ypos = 0;

            foreach (Bitmap b in oriImg)
            {
                switch (Ver)
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
                switch (Hor)
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

                gIMG.DrawImage(b, new System.Drawing.Point(Xpos, Ypos));
            }
            return myImg;
        }

        public static Bitmap OverlayImages(Bitmap img1, Bitmap img2, int Hor, int Ver)
        {
            List<Bitmap> allIMGs = new List<Bitmap>();
            allIMGs.Add(img1);
            allIMGs.Add(img2);

            return OverlayImages(allIMGs, Hor, Ver);
        }

        public static Bitmap operationImages(Bitmap img1, Bitmap img2, int op)
        {
            img1 = ImageUtil.convert(img1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            img2 = ImageUtil.convert(img2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Bitmap filteredImage = img1;
            IFilter myFilter;

            if (op == Filters.mode["Add"])
            {
                myFilter = new Add(img2);
                filteredImage = myFilter.Apply(img1);
            }
            else if (op == Filters.mode["Subtract"])
            {
                myFilter = new Subtract(img2);
                filteredImage = myFilter.Apply(img1);
            }
            else if (op == Filters.mode["Intersect"])
            {
                myFilter = new Intersect(img2);
                filteredImage = myFilter.Apply(img1);
            }
            else if (op == Filters.mode["Difference"])
            {
                myFilter = new Difference(img2);
                filteredImage = myFilter.Apply(img1);
            }
            else if (op == Filters.mode["Merge"])
            {
                myFilter = new Merge(img2);
                filteredImage = myFilter.Apply(img1);
            }
            else if (op == Filters.mode["Multiply"])
            {
                myFilter = new Multiply(img2);
                filteredImage = myFilter.Apply(img1);
            }
            return filteredImage;
        }

        public static Bitmap RGBA_replaceChannel(Bitmap img, Bitmap RChannel, Bitmap GChannel, Bitmap BChannel, Bitmap AChannel)
        {
            Bitmap NewIMG = ImageUtil.convert(img, PixelFormat.Format32bppArgb);

            if (RChannel != null)
            {
                RChannel = ImageUtil.convert(RChannel, PixelFormat.Format32bppArgb);
                RChannel = Grayscale.CommonAlgorithms.RMY.Apply(RChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.R, RChannel);
                img = myFilter.Apply(img);
            }

            if (GChannel != null)
            {
                GChannel = ImageUtil.convert(GChannel, PixelFormat.Format32bppArgb);
                GChannel = Grayscale.CommonAlgorithms.RMY.Apply(GChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.G, GChannel);
                img = myFilter.Apply(img);
            }

            if (BChannel != null)
            {
                BChannel = ImageUtil.convert(BChannel, PixelFormat.Format32bppArgb);
                BChannel = Grayscale.CommonAlgorithms.RMY.Apply(BChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.B, BChannel);
                img = myFilter.Apply(img);
            }

            if (AChannel != null)
            {
                AChannel = ImageUtil.convert(AChannel, PixelFormat.Format32bppArgb);
                AChannel = Grayscale.CommonAlgorithms.RMY.Apply(AChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.A, AChannel);
                img = myFilter.Apply(img);
            }

            return img;
        }

        public static Bitmap maskImage(Bitmap img, Bitmap mask)
        {
            Bitmap greyscale = ImageFilter.FilterImage(mask, Filters.filters["Greyscale"]);
            //greyscale = FormatUtil.convert(greyscale, PixelFormat.Format32bppArgb);

            Bitmap invert = ImageFilter.FilterImage(greyscale, Filters.filters["Invert"]);
            //invert = FormatUtil.convert(invert, PixelFormat.Format32bppArgb);

            Bitmap toAddMask = ImageUtil.convert(img, PixelFormat.Format32bppArgb);

            Bitmap withAlpha = ImageMultiFilter.RGBA_replaceChannel(toAddMask, null, null, null, invert);
            return withAlpha;
            //return ImageMultiFilter.OverlayImages(img, withAlpha, 1, 1);
        }
        
        public static Bitmap overlayMode(Bitmap below, Bitmap over, int op)
        {

            if (op == Filters.mode["Lighten Filter"]) return lighten(below, over);
            else if (op == Filters.mode["Linear Dodge (Add)"]) return linearDodgeAdd(below, over);
            else if (op == Filters.mode["Soft Light"]) return softLight(below, over);
            else if (op == Filters.mode["Multiply"]) return multiply(below, over);
            else if (op == Filters.mode["Darken"]) return darken(below, over);
            else if (op == Filters.mode["Overlay"]) return overlay(below, over);

            else if (op == Filters.mode["Add"]) return operationImages(below, over, op);
            else if (op == Filters.mode["Subtract"]) return operationImages(below, over, op);
            else if (op == Filters.mode["Intersect"]) return operationImages(below, over, op);
            else if (op == Filters.mode["Difference"]) return operationImages(below, over, op);
            else if (op == Filters.mode["Merge"]) return operationImages(below, over, op);
            else if (op == Filters.mode["Multiply"]) return operationImages(below, over, op);

            return null;
        }

        private static Bitmap darken(Bitmap below, Bitmap over)
        {
            Bitmap invertBelow = ImageFilter.FilterImage(below, Filters.filters["Invert"]);
            Bitmap myMask = maskImage(over, invertBelow);
            Bitmap overlayImages = OverlayImages(below, myMask, 1, 1);
            return operationImages(overlayImages, below, Filters.mode["Intersect"]);
        }

        private static Bitmap overlay(Bitmap below, Bitmap over)
        {
            Bitmap invertBelow = ImageFilter.FilterImage(below, Filters.filters["Invert"]);
            Bitmap myMask = maskImage(over, invertBelow);
            Bitmap overlayImages = OverlayImages(below, myMask, 1, 1);
            return operationImages(overlayImages, below, Filters.mode["Merge"]);
        }

        private static Bitmap softLight(Bitmap below, Bitmap over)
        {
            Bitmap myMask = maskImage(over, below);
            return operationImages(myMask, below, Filters.mode["Intersect"]);
        }

        private static Bitmap linearDodgeAdd(Bitmap below, Bitmap over)
        {
            Bitmap myMask = maskImage(over, below);
            return operationImages(myMask, below, Filters.mode["Add"]);
        }

        private static Bitmap lighten(Bitmap below, Bitmap over)
        {
            Bitmap myMask = maskImage(over, below);
            return operationImages(myMask, below, Filters.mode["Merge"]);
        }

        private static Bitmap multiply(Bitmap below, Bitmap over)
        {
            //Bitmap myMask = maskImage(over, below);
            //Bitmap merged = OverlayImages(below, myMask, 1, 1);
            //return operationImages(merged, below, Filters.mode["Intersect"]);
            return combineImages(below, over, over);
        }

        public static Bitmap combineImages(Bitmap img1, Bitmap img2, Bitmap valuesCombination)
        {
            Bitmap below = ImageUtil.convert(img1, PixelFormat.Format24bppRgb);
            Bitmap over = ImageUtil.convert(img2, PixelFormat.Format24bppRgb);
            Bitmap values = ImageUtil.convert(valuesCombination, PixelFormat.Format24bppRgb);
            values = Grayscale.CommonAlgorithms.RMY.Apply(values);

            TexturedMerge filter = new TexturedMerge(TextureTools.FromBitmap(values));

            filter.OverlayImage = over;

            below = filter.Apply(below);

            return below;
        }

    }
}
