using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using AForge.Imaging.Filters;
using AForge;
using AForge.Imaging;
using System.Drawing.Imaging;

namespace ImageTools.Components
{
    public class RGBFilterComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the RGBFilterComponent class.
        /// </summary>
        public RGBFilterComponent()
          : base("RGBFilter", "RGBFilter")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Red_Range", "R", "Red Range To Highlight", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Red_Range", "G", "Red Range To Highlight", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Red_Range", "B", "Red Range To Highlight", GH_ParamAccess.item);
            pManager.AddColourParameter("Colour", "C", "Colour to use for Highlighting", GH_ParamAccess.item, Color.Black);
            pManager.AddBooleanParameter("Direction", "D", "Invert Set", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "NewImg", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);
            Interval R = new Interval();
            DA.GetData(1, ref R);
            Interval G = new Interval();
            DA.GetData(2, ref G);
            Interval B = new Interval();
            DA.GetData(3, ref B);
            Color C = Color.Black;
            DA.GetData(4, ref C);
            Boolean Dir = false;
            DA.GetData(5, ref Dir);

            DA.SetData(0, ImageFilter.RGBFilter(sourceImage, R, G, B, C, Dir));
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
            get { return new Guid("{6d5d82ca-714f-4bfc-91e4-27a886e2d408}"); }
        }
    }
}