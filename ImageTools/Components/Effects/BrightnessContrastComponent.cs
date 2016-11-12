using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using AForge.Imaging.Filters;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class BrightContrastSatComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the BrightnessContrastComponent class.
        /// </summary>
        public BrightContrastSatComponent():base("BrightnessContrastAdjust", "BCSAdjust", "Adjust the brightness and the Contrast of an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Contrast", "C", "Value for Contrast", GH_ParamAccess.item, 0);

            Param_Integer ContrastInteger = pManager[1] as Param_Integer;

            ContrastInteger.AddNamedValue("Contrast Stretch", -1);

            pManager.AddIntegerParameter("Brightness", "B", "Value for Brightness", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Saturation", "S", "Values to manipulate Saturation", GH_ParamAccess.item, 0.0);
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
            int C = 0;
            DA.GetData(1, ref C);
            int B = 0;
            DA.GetData(2, ref B);
            double S = 0;
            DA.GetData(3, ref S);

            sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Bitmap filteredImage = sourceImage;

            if (C >=0)
            {
                IFilter myFilterContrast = new ContrastCorrection(C);
                filteredImage = myFilterContrast.Apply(sourceImage);
            }
            else
            {
                IFilter myFilterContrast = new ContrastStretch();
                filteredImage = myFilterContrast.Apply(sourceImage);
            }

            IFilter myFilterBrightness = new BrightnessCorrection(B);
            filteredImage = myFilterBrightness.Apply(filteredImage);

            IFilter mySaturationFilter = new SaturationCorrection(Convert.ToSingle(S));
            filteredImage = mySaturationFilter.Apply(filteredImage);




            DA.SetData(0, filteredImage);
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
                return Resources.Contrast_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c95572ce-2ed9-499b-aca6-85477b8fbbb8}"); }
        }
    }
}