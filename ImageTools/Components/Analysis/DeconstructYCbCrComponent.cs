using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using ImageTools.Utilities;
using AForge.Imaging.Filters;
using System.Drawing;
using AForge.Imaging;

namespace ImageTools.Components
{
    
    public class DeconstructYCbCrComponent : ImageAnalysisToolbox
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructYCbCrComponent class.
        /// </summary>
        public DeconstructYCbCrComponent()
          : base("DeconstructYCbCr", "DecYCbCr")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Y_Channel", "Y", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Cb_Channel", "Cb", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Cr_Channel", "Cr", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);

            sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            YCbCrExtractChannel myFilter = new YCbCrExtractChannel(YCbCr.YIndex);
            DA.SetData(0, myFilter.Apply(sourceImage));

            myFilter = new YCbCrExtractChannel(YCbCr.CbIndex);
            DA.SetData(1, myFilter.Apply(sourceImage));

            myFilter = new YCbCrExtractChannel(YCbCr.CrIndex);
            DA.SetData(2, myFilter.Apply(sourceImage));
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
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{0b93ec5d-08b1-442a-bb99-5a708f843e16}"); }
        }
    }
    
}