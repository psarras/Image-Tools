using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    
    public class BoundaryGradientComponent : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the BoundaryGradientComponent class.
        /// </summary>
        public BoundaryGradientComponent()
          : base("BoundaryGradient", "bGradient")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

            pManager.AddGenericParameter("Canvas", "cnv", "Image to use as a reference", GH_ParamAccess.item);
            pManager.AddColourParameter("Colors", "Cc", "Color to use on the start of the Gradient", GH_ParamAccess.item, Color.White);
            pManager.AddColourParameter("Colors", "Ce", "Color to use on the start of the Gradient", GH_ParamAccess.list);

            pManager.AddIntegerParameter("Factor", "F", "Factor in percentage", GH_ParamAccess.item, 50);

            Param_Integer FactorParam = pManager[3] as Param_Integer;

            FactorParam.AddNamedValue("10 Percent", 10);
            FactorParam.AddNamedValue("20 Percent", 20);
            FactorParam.AddNamedValue("30 Percent", 30);
            FactorParam.AddNamedValue("40 Percent", 40);
            FactorParam.AddNamedValue("50 Percent", 50);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "NewImg", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap img = null;
            DA.GetData(0, ref img);
            Color Cc = Color.White;
            DA.GetData(1, ref Cc);
            List<Color> Ce = new List<Color>();
            DA.GetDataList(2, Ce);
            int factor = 0;
            DA.GetData(3, ref factor);

            float newFactor = (float)(factor / 100.0f);
            newFactor = Math.Min(1, newFactor);
            newFactor = Math.Max(0, newFactor);

            DA.SetData(0, ImageConstruct.CreateBoundaryGradient(img, Cc, Ce, newFactor));

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
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{44d7f510-82df-4e56-b26d-7142adb382e4}"); }
        }
    }
    
}