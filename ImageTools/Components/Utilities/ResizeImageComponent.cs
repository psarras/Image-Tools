using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class ResizeImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ResizeImageComponent class.
        /// </summary>
        public ResizeImageComponent()
          : base("ResizeImage", "ResizeImg", "Resize an image on one dimension and retain the ratio scaling appropriatly")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Size", "S", "Size to adjust to", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Direction", "D", "True for Width, False for Height", GH_ParamAccess.item, true);
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
            int size = 0;
            DA.GetData(1, ref size);
            Boolean direction = true;
            DA.GetData(2, ref direction);

            DA.SetData(0, ImageShape.resizeImage(sourceImage, size, direction));

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
                return Resources.ScaleImage_ICON_4;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{4d51dd03-5ecb-48c1-a8be-1b538b7c001c}"); }
        }
    }
}