using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class DropShadowComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the DropShadowComponent class.
        /// </summary>
        public DropShadowComponent()
          : base("DropShadow", "dShadow", "Add a drop shadow effect, requires transparency to be visible")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "Colour of the shadow", GH_ParamAccess.item, Color.Black);
            pManager.AddIntegerParameter("RangeX", "X", "Shadow Drop on X", GH_ParamAccess.item, -2);
            pManager.AddIntegerParameter("RangeY", "Y", "Shadow Drop on Y", GH_ParamAccess.item, 2);
            pManager.AddNumberParameter("Sigma", "S", "sigma for Blur effect", GH_ParamAccess.item, 0.2);
            pManager.AddIntegerParameter("Range", "R", "Range for Blur effect", GH_ParamAccess.item, 2);
            pManager.AddNumberParameter("Opacity", "O", "Opacity of the Shadow", GH_ParamAccess.item, 0.5);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "img", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("New Image", "DSimg", "Manipulated Image", GH_ParamAccess.item);
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
            int X = 0;
            DA.GetData(2, ref X);
            int Y = 0;
            DA.GetData(3, ref Y);
            double S = 0;
            DA.GetData(4, ref S);
            int R = 2;
            DA.GetData(5, ref R);
            double O = 0.5;
            DA.GetData(6, ref O);

            Tuple<Bitmap, Bitmap> myResult = ImageFilter.DropShadow(img, X, Y, C, Convert.ToSingle(S), R, Convert.ToSingle(O));

            DA.SetData(0, myResult.Item1);
            DA.SetData(1, myResult.Item2);
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
                return Resources.DropShadow_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9063ca33-5a12-46fe-b702-4f5ce726fadc}"); }
        }
    }
}
