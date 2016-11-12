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
    public class ColorImageComponent : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the ColorImageComponent class.
        /// </summary>
        public ColorImageComponent()
          : base("ColorImage", "cIMG", "Create a simple Colored image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Width", "W", "Width of the New Image", GH_ParamAccess.item, 500);
            pManager.AddIntegerParameter("Height", "H", "Height of the New Image", GH_ParamAccess.item, 500);
            pManager.AddIntegerParameter("DPI", "DPI", "DPI of the new Image", GH_ParamAccess.item, 96);

            Param_Integer DPIparam = pManager[2] as Param_Integer;

            DPIparam.AddNamedValue("72 DPI", 72);
            DPIparam.AddNamedValue("96 DPI", 96);
            DPIparam.AddNamedValue("150 DPI", 150);
            DPIparam.AddNamedValue("300 DPI", 300);
            DPIparam.AddNamedValue("600 DPI", 600);
            
            pManager.AddColourParameter("Color", "C", "Color of the new Image", GH_ParamAccess.item, Color.White);
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
            int Width = 0;
            DA.GetData(0, ref Width);
            int Height = 0;
            DA.GetData(1, ref Height);
            int DPI = 72;
            DA.GetData(2, ref DPI);
            Color C = Color.White;
            DA.GetData(3, ref C);

            DA.SetData(0, ImageConstruct.ColorImage(Width, Height, DPI, DPI, C));
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
                return Resources.New_Color;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d811516b-7fc2-418f-91a2-1231c906f71f}"); }
        }
    }
}