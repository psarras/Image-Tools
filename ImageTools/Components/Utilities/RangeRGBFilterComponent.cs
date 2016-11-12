using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class RangeRGBFilterComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the RangeRGBFilterComponent class.
        /// </summary>
        public RangeRGBFilterComponent()
          : base("RangeRGBFilter", "rRGBFilter", "Highlight certain colour ranges on an image")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddColourParameter("Colour", "Cs", "Colour to Search for", GH_ParamAccess.item, Color.Black);
            pManager.AddColourParameter("Colour", "Ch", "Colour to use for Highlighting", GH_ParamAccess.item, Color.Black);
            pManager.AddIntegerParameter("Tolerance", "T", "The Size of the Ranges that this component is going to use",GH_ParamAccess.item, 0);
            pManager.AddBooleanParameter("Direction", "D", "Invert Set", GH_ParamAccess.item, true);
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
            Bitmap img = null;
            DA.GetData(0, ref img);
            Color C = Color.Black;
            DA.GetData(1, ref C);
            Color Ch = Color.Black;
            DA.GetData(2, ref Ch);
            int T = 0;
            DA.GetData(3, ref T);
            Boolean Dir = false;
            DA.GetData(4, ref Dir);

            DA.SetData(0, ImageFilter.rangeRGBFilter(img, C, Ch, T, Dir));
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
                return Resources.FilterSingle_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{af2f818c-e178-41f6-bdbe-0b585c1b04d2}"); }
        }
    }
}