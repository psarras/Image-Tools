using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using System.Linq;
using Grasshopper.Kernel.Types;
using ImageTools.Properties;

namespace ImageTools.Components.Utilities
{
    public class getPixels : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the getPixels class.
        /// </summary>
        public getPixels()
          : base("getPixels", "getPixels",
              "getPixels")
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
            pManager.AddColourParameter("C", "C", "C", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap img = null;
            DA.GetData(0, ref img);

            List<Color> colors = ImageUtil.getPixelsArray(img).ToList();

            List<GH_Colour> ghColors = new List<GH_Colour>();

            foreach (Color c in colors)
                ghColors.Add(new GH_Colour(c));



            DA.SetDataList(0, ghColors);
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
                return Resources.GetPixels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{bf42f0b0-27d0-4305-822f-de5cb7da2b63}"); }
        }
    }
}