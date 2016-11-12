using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class VariableMorphComponent : MultiImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the VariableMorphComponent class.
        /// </summary>
        public VariableMorphComponent()
          : base("VariableMorphComponent", "Nickname", "Overlay two images using a third image to specify blending for each pixel")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image1", "img1", "image1 to manipulate", GH_ParamAccess.item);
            pManager.AddGenericParameter("image2", "img2", "image2 to manipulate", GH_ParamAccess.item);
            pManager.AddGenericParameter("Values", "V", "Image that represents the Values of blending", GH_ParamAccess.item);
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
            Bitmap sourceImage1 = null;
            DA.GetData(0, ref sourceImage1);
            Bitmap sourceImage2 = null;
            DA.GetData(1, ref sourceImage2);
            Bitmap values = null;
            DA.GetData(2, ref values);

            DA.SetData(0, ImageMultiFilter.combineImages(sourceImage1, sourceImage2, values));
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
                return Resources.Morph_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{13a4b1fa-2434-460f-bdfd-5e64dd1e8ba5}"); }
        }
    }
}