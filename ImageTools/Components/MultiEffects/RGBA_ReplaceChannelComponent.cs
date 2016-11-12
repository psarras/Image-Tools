using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using AForge.Imaging.Filters;
using AForge.Imaging;
using Grasshopper.Kernel.Parameters;
using System.Drawing.Imaging;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class RGBA_ReplaceChannelComponent : MultiImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the RGBA_ReplaceChannelComponent class.
        /// </summary>
        public RGBA_ReplaceChannelComponent()
          : base("RGBA_ReplaceChannel", "RGBA_RepChannel", "Replace any channel of an image and reconstruct the image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);

            pManager.AddGenericParameter("Red_Channel", "R", "Red Channel to Replace", GH_ParamAccess.item);
            pManager.AddGenericParameter("Green_Channel", "G", "Green Channel to Replace", GH_ParamAccess.item);
            pManager.AddGenericParameter("Blue_Channel", "B", "Blue Channel to Replace", GH_ParamAccess.item);
            pManager.AddGenericParameter("Alpha_Channel", "A", "Alpha Channel to Replace", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "img", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);

            Bitmap RChannel = null;
            DA.GetData(1, ref RChannel);
            Bitmap GChannel = null;
            DA.GetData(2, ref GChannel);
            Bitmap BChannel = null;
            DA.GetData(3, ref BChannel);
            Bitmap AChannel = null;
            DA.GetData(4, ref AChannel);

            sourceImage = ImageUtil.convert(sourceImage, PixelFormat.Format32bppArgb);
            
            if (RChannel != null)
            {
                RChannel = ImageUtil.convert(RChannel, PixelFormat.Format32bppArgb);
                RChannel = Grayscale.CommonAlgorithms.RMY.Apply(RChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.R, RChannel);
                sourceImage = myFilter.Apply(sourceImage);
            }

            if (GChannel != null)
            {
                GChannel = ImageUtil.convert(GChannel,  PixelFormat.Format32bppArgb);
                GChannel = Grayscale.CommonAlgorithms.RMY.Apply(GChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.G, GChannel);
                sourceImage = myFilter.Apply(sourceImage);
            }

            if (BChannel != null)
            {
                BChannel = ImageUtil.convert(BChannel,  PixelFormat.Format32bppArgb);
                BChannel = Grayscale.CommonAlgorithms.RMY.Apply(BChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.B, BChannel);
                sourceImage = myFilter.Apply(sourceImage);
            }

            if (AChannel != null)
            {
                AChannel = ImageUtil.convert(AChannel,  PixelFormat.Format32bppArgb);
                AChannel = Grayscale.CommonAlgorithms.RMY.Apply(AChannel);
                ReplaceChannel myFilter = new ReplaceChannel(RGB.A, AChannel);
                sourceImage = myFilter.Apply(sourceImage);
            }
            
            DA.SetData(0, sourceImage);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resources.Replace_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e4263b02-6394-4c00-9085-b36144fe26f4}"); }
        }
    }
}