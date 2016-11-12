using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;


namespace ImageTools.Components
{
    public class DeconstructRGBAComponent : ImageAnalysisToolbox
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructRGBA class.
        /// </summary>
        public DeconstructRGBAComponent()
            : base("DeconstructRGBA", "DecRGBA", "Deconstruct image on its base channels")
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
            pManager.AddGenericParameter("Red_Channel", "R", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Green_Channel", "G",  "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Blue_Channel", "B", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Alpha_Channel", "A", "Manipulated Image", GH_ParamAccess.item);
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

            ExtractChannel myFilter = new ExtractChannel(RGB.R);
            DA.SetData(0, myFilter.Apply(sourceImage));

            myFilter = new ExtractChannel(RGB.G);
            DA.SetData(1, myFilter.Apply(sourceImage));

            myFilter = new ExtractChannel(RGB.B);
            DA.SetData(2, myFilter.Apply(sourceImage));

            myFilter = new ExtractChannel(RGB.A);
            DA.SetData(3, myFilter.Apply(sourceImage));

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
                return Resources.RGB_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{696ef134-4324-41bf-8dc4-e13616d0a74f}"); }
        }
    }
}