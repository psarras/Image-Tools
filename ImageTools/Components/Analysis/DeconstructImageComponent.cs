using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class DeconstructImageComponent : ImageAnalysisToolbox
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructImageComponent class.
        /// </summary>
        public DeconstructImageComponent()
          : base("DeconstructImage", "DecImg", "Get basic details regarding the given image")
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
            pManager.AddIntegerParameter("Width", "W", "Width of Image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "Height of Image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("DPIx", "DPIx", "DPIx of Image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("DPIy", "DPIy", "DPIy of Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Format", "F", "Format of the Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);

            DA.SetData(0, sourceImage.Width);
            DA.SetData(1, sourceImage.Height);
            DA.SetData(2, sourceImage.HorizontalResolution);
            DA.SetData(3, sourceImage.VerticalResolution);
            DA.SetData(4, sourceImage.PixelFormat);
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
                return Resources.DeconImage_ICON_2;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{6f6b25a0-6a8e-4f5d-803c-fb15c512b52f}"); }
        }
    }
}