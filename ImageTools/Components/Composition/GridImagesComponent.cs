using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class GridImagesComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the GridImageComponent class.
        /// </summary>
        public GridImagesComponent()
          : base("GridImages", "GrIMGs", "Array images on a grid")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("images", "imgs", "image to manipulate", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Warp", "W", "Number of Images on the choosen Direction", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Padding", "P", "Padding to add around and between the Images", GH_ParamAccess.item, 0);
            pManager.AddBooleanParameter("Direction", "D", "Direction to priorotise, true for horizontal, false for Vertical", GH_ParamAccess.item, true);
            pManager.AddColourParameter("Color", "C", "Background Colour to Use, only visible if there is padding", GH_ParamAccess.item, Color.White);
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
            List<Bitmap> sourceImages = new List<Bitmap>();
            DA.GetDataList(0, sourceImages);
            int warp = 0;
            DA.GetData(1, ref warp);
            int pad = 0;
            DA.GetData(2, ref pad);
            Boolean dir = true;
            DA.GetData(3, ref dir);
            Color C = Color.White;
            DA.GetData(4, ref C);

            DA.SetData(0, ImageComposition.GridImages(sourceImages, warp, pad, dir, C));
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
                return Resources.GridImage_ICON;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{234c83e9-e1b6-411b-80ba-eb8549c74d20}"); }
        }
    }
}