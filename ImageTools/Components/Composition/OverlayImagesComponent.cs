using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using System.Drawing;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class OverlayImagesComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the OverlayImagesComponent class.
        /// </summary>
        public OverlayImagesComponent()
          : base("OverlayImages", "OverImgs", "Combine a list of images by stacking on top of each other")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Horizontal", "H", "Horizontal Alignment", GH_ParamAccess.item, 1);

            Param_Integer HorParam = pManager[1] as Param_Integer;

            HorParam.AddNamedValue("Left", 0);
            HorParam.AddNamedValue("Mid", 1);
            HorParam.AddNamedValue("Right", 2);

            pManager.AddIntegerParameter("Vertical", "V", "Vertical Alignment", GH_ParamAccess.item, 1);

            Param_Integer VerParam = pManager[2] as Param_Integer;

            VerParam.AddNamedValue("Top", 0);
            VerParam.AddNamedValue("Mid", 1);
            VerParam.AddNamedValue("Bot", 2);
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
            List<Bitmap> sourceImage = new List<Bitmap>();
            DA.GetDataList(0, sourceImage);
            int Hor = 0;
            DA.GetData(1, ref Hor);
            int Ver = 0;
            DA.GetData(2, ref Ver);

            DA.SetData(0, ImageMultiFilter.OverlayImages(sourceImage, Hor, Ver));
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
                return Resources.Overlay_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3877ca9e-242c-41f8-8c7c-2f07f1e5427e}"); }
        }
    }
}