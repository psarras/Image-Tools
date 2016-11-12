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
    public class GradienImageComponent : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the GradienImageComponent class.
        /// </summary>
        public GradienImageComponent()
          : base("GradienImage", "gradIMG", "Create a gradient image from two colors")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Canvas", "cnv", "Image to use as a reference", GH_ParamAccess.item);
            pManager.AddColourParameter("Start Color", "Cs", "Color to use on the start of the Gradient", GH_ParamAccess.item, Color.Black);
            pManager.AddColourParameter("Start Color", "Ce", "Color to use on the start of the Gradient", GH_ParamAccess.item, Color.White);
            pManager.AddIntegerParameter("Angle", "A", "Angle of the Gradient", GH_ParamAccess.item, 0);

            Param_Integer AngleParam = pManager[3] as Param_Integer;

            AngleParam.AddNamedValue("0 Degrees", 0);
            AngleParam.AddNamedValue("45 Degrees", 45);
            AngleParam.AddNamedValue("90 Degrees", 90);
            AngleParam.AddNamedValue("135 Degrees", 135);
            AngleParam.AddNamedValue("180 Degrees", 180);

            pManager.AddIntegerParameter("Factor", "F", "Factor in percentage", GH_ParamAccess.item, 50);

            Param_Integer FactorParam = pManager[4] as Param_Integer;

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
            Color Cs = Color.Black;
            DA.GetData(1, ref Cs);
            Color Ce = Color.White;
            DA.GetData(2, ref Ce);
            int angle = 0;
            DA.GetData(3, ref angle);
            int factor = 0;
            DA.GetData(4, ref factor);

            DA.SetData(0, ImageConstruct.CreateGradient(img, Cs, Ce, angle, factor/100.0f));
            //DA.SetData(0, ImageConstruct.CreateGradient2(img, Cs, Ce, new PointF(0, 0), 
            //    new PointF(img.Width, (int)(img.Height/2f)), factor / 100.0f));
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
                return Resources.New_Gradient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{426c0086-316a-4c2c-8edc-9b2471a08560}"); }
        }
    }
}
