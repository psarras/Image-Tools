using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using AForge.Imaging.Filters;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class BlurImageComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the BlurImageComponent class.
        /// </summary>
        public BlurImageComponent()
          : base("BlurImage", "BlurImage", "Blur an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddNumberParameter("sigma", "S", "sigma parameter", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("Range", "R", "Range parameter of the effect", GH_ParamAccess.item, 2);
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
            double sigma = 1;
            DA.GetData(1, ref sigma);
            int range = 2;
            DA.GetData(2, ref range);

            DA.SetData(0, ImageFilter.BlurImage(sourceImage, Convert.ToSingle(sigma), range));
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
                return Resources.Blur_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{303fccc2-b014-4b12-81fa-1b6c91f47c2a}"); }
        }
    }
}