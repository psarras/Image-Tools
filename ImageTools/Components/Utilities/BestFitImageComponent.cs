using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class BestFitImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the BestFitImageComponent class.
        /// </summary>
        public BestFitImageComponent()
          : base("BestFitImage", "BFImg", "This will scale and zoom an image the best way possible to keep its ratio but match the specified sizes")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Widht", "W", "Width to match", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "Height to match", GH_ParamAccess.item);
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
            int width = 0;
            DA.GetData(1, ref width);
            int height = 0;
            DA.GetData(2, ref height);

            DA.SetData(0, ImageShape.BestFitImage(sourceImage, width, height));
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
                return Resources.BestFit;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{f8f3abf2-c71c-4176-b4b1-a36e075effdc}"); }
        }
    }
}