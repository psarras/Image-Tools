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
    public class ArrayImagesComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the ArrayImageComponent class.
        /// </summary>
        public ArrayImagesComponent()
          : base("ArrayImages", "ArIMGs", "Array a list of Images into a Column or a Row")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Images", "imgs", "Images to Combine", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Align", "A", "Different Alignments", GH_ParamAccess.item, 0);

            Param_Integer AlignParam = pManager[1] as Param_Integer;

            AlignParam.AddNamedValue("Left/Top", 0);
            AlignParam.AddNamedValue("Center", 1);
            AlignParam.AddNamedValue("Right/Bot", 2);

            pManager.AddBooleanParameter("Direction", "Dir", "True for Horizontal False for Vertical", GH_ParamAccess.item, true);
            pManager.AddIntegerParameter("Padding", "P", "Padding to add between the images, does not add on the edges", GH_ParamAccess.item, 0);
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
            int Align = 0;
            DA.GetData(1, ref Align);
            Boolean Dir = false;
            DA.GetData(2, ref Dir);
            int Pad = 0;
            DA.GetData(3, ref Pad);

            DA.SetData(0, ImageComposition.ArrayImages(sourceImage, Align, Dir, Pad));

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
                return Resources.ArrayImages_ICON_2;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e6f5b140-5e38-4983-a179-dfe85edf35cb}"); }
        }
    }
}