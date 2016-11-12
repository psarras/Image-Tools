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
    public class RotateFlipImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the RotateFlipComponent class.
        /// </summary>
        public RotateFlipImageComponent()
          : base("RotateFlipImage", "RotFlipImg", "Do some basic matrix manipulations on an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Rotate", "R", "Rotation Values", GH_ParamAccess.item, 0);

            Param_Integer paramDegrees = pManager[1] as Param_Integer;

            paramDegrees.AddNamedValue("0 degrees", 0);
            paramDegrees.AddNamedValue("90 degrees", 90);
            paramDegrees.AddNamedValue("180 degrees", 180);
            paramDegrees.AddNamedValue("270 degrees", 270);

            pManager.AddIntegerParameter("Flip", "F", "Flip Values", GH_ParamAccess.item, 0);

            Param_Integer paramFlip = pManager[2] as Param_Integer;

            paramFlip.AddNamedValue("Flip None", 0);
            paramFlip.AddNamedValue("Flip X", 1);
            paramFlip.AddNamedValue("Flip Y", 2);
            paramFlip.AddNamedValue("Flip XY", 3);

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
            int R = 0;
            DA.GetData(1, ref R);
            int F = 0;
            DA.GetData(2, ref F);

            DA.SetData(0, ImageShape.RotateFlipImage(sourceImage, R, F));
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
                return Resources.Rotate_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d13cc5ef-9ae3-4c41-a0af-5dd5802fc0fa}"); }
        }
    }
}