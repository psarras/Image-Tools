using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class ScaleImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ScaleImageComponent class.
        /// </summary>
        public ScaleImageComponent()
          : base("ScaleImage", "ScaleImg", "Stretch an image to match the dimensions given")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Width", "W", "Width of Image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "Height of Image", GH_ParamAccess.item);
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
            int W = 0;
            DA.GetData(1, ref W);
            int H = 0;
            DA.GetData(2, ref H);


            DA.SetData(0, new Bitmap(sourceImage, W, H));

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
                return Resources.Scale_Image_2;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e5fddcca-05ea-416a-a048-579542a45192}"); }
        }
    }
}