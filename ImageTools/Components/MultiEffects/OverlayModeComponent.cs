using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class OverlayModeComponent : MultiImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ImageColorizeImageComponent class.
        /// </summary>
        public OverlayModeComponent()
          : base("ImageColorizeImage", "IMGcolIMG", "Do some more advance operations between two images")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Below", "B", "image to be overlayed", GH_ParamAccess.item);
            pManager.AddGenericParameter("Overlay", "O", "Image to Overlay", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "Mode to use", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[2] as Param_Integer;

            foreach (KeyValuePair<string, int> pair in Filters.mode)
            {
                param.AddNamedValue(pair.Key, pair.Value);
            }

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
            Bitmap below = null;
            DA.GetData(0, ref below);
            Bitmap over = null;
            DA.GetData(1, ref over);
            int mode = 0;
            DA.GetData(2, ref mode);

            DA.SetData(0, ImageMultiFilter.overlayMode(below, over, mode));
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
                return Resources.Overlay_Image_Advanced;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9367188f-533c-42a8-8196-e85674b45a5d}"); }
        }
    }
}