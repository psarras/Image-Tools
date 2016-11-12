using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using System.Drawing.Imaging;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class LoadImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the LoadImageComponent class.
        /// </summary>
        public LoadImageComponent()
          : base("LoadImage", "LoadImg", "Load image from a path")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "Path of an Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "img", "Loaded Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = "";
            DA.GetData(0, ref path);
            Bitmap img = new Bitmap(path);
            img = ImageUtil.convert(img, PixelFormat.Format32bppArgb);
            DA.SetData(0, img);
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
                return Resources.LoadImage;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{906d9aca-4849-498a-9bf4-7129d6d95fc2}"); }
        }
    }
}