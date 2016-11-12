using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class SetTransparencyComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the SetTransparencyComponent class.
        /// </summary>
        public SetTransparencyComponent()
          : base("SetTransparency", "SetTransp", "Make a color transparent")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "Colour To make Transparent", GH_ParamAccess.item, Color.Black);
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
            Color C = Color.Black;
            DA.GetData(1, ref C);

            sourceImage.MakeTransparent(C);

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
                return Resources.OpacityPixel_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{a7923e18-4db4-4338-98e9-b25d73627b0a}"); }
        }
    }
}