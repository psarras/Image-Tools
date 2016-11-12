using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components.Create
{
    public class ColorPalette : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the ColorPalette class.
        /// </summary>
        public ColorPalette()
            : base("ColorPalette", "ColorPalette",
                "Description")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("t", "t", "Param", GH_ParamAccess.item);
            pManager.AddVectorParameter("Constant", "C", "Parameter Constant", GH_ParamAccess.item);
            pManager.AddVectorParameter("Multi", "M", "Parameter Multi", GH_ParamAccess.item);
            pManager.AddVectorParameter("Repeat", "R", "Parameter Repeat", GH_ParamAccess.item);
            pManager.AddVectorParameter("Phase", "P", "Parameter Phase", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("Vector", "V", "Vector", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double t = 0;
            DA.GetData(0, ref t);
            Vector3d C = new Vector3d();
            DA.GetData(1, ref C);
            Vector3d M = new Vector3d();
            DA.GetData(2, ref M);
            Vector3d R = new Vector3d();
            DA.GetData(3, ref R);
            Vector3d P = new Vector3d();
            DA.GetData(4, ref P);

            DA.SetData(0, ColorUtil.getColorPretty(t, C, M, R, P));

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
                return Resources.PrettyCustom_Gradient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{b01f101d-c0cd-4168-ae9f-ce17d7200c69}"); }
        }
    }
}