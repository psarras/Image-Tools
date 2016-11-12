using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class TintImageComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ColorImageComponent class.
        /// </summary>
        public TintImageComponent()
          : base("TintImage", "TintIMG", "Tint an image with a certain color")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "Color to Colorize Image", GH_ParamAccess.item, Color.White);
            pManager.AddNumberParameter("Value", "V", "Intensity of Color", GH_ParamAccess.item, 1);
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
            Color C = Color.White;
            DA.GetData(1, ref C);
            double Value = 1;
            DA.GetData(2, ref Value);

            DA.SetData(0, ImageFilter.TintImage(img, C, Convert.ToSingle(Value)));
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
                return Resources.Tint_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{27e9ff64-e40c-4cf3-83f4-0f256b46603e}"); }
        }
    }
}