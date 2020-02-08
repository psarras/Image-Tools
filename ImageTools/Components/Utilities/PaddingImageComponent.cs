using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class PaddingImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the PaddingImageComponent class.
        /// </summary>
        public PaddingImageComponent()
          : base("PaddingImage", "PadImg", "Add extra space on the canvas of an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Left", "L", "pixels to add from the Left", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Right", "R", "pixels to addfrom the Right", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Top", "T", "pixels to add from the Top", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Bottom", "B", "pixels to add from the Bottom", GH_ParamAccess.item, 0);
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
            int L = 0;
            DA.GetData(1, ref L);
            int R = 0;
            DA.GetData(2, ref R);
            int T = 0;
            DA.GetData(3, ref T);
            int B = 0;
            DA.GetData(4, ref B);

            DA.SetData(0, ImageShape.PaddingImage(sourceImage, L, R, T, B));
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
                return Resources.PaddingImage;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e5c6192c-ff4a-43de-a5b3-69e800ecc202}"); }
        }
    }
}