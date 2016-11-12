using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using System.Drawing.Drawing2D;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class SolidPen : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the SolidPen class.
        /// </summary>
        public SolidPen()
          : base("SolidPen", "SolidPen",
              "Create A Simple Solid Pen")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "Colour to use for this pen", GH_ParamAccess.item, Color.Black);
            pManager.AddNumberParameter("Size", "S", "Size of hte Brush", GH_ParamAccess.item, 2);
            pManager.AddIntegerParameter("Style", "S", "Style of the stroke", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[2] as Param_Integer;

            foreach (KeyValuePair<int, DashStyle> pair in Styles.dash)
            {
                param.AddNamedValue(pair.Value.ToString(), pair.Key);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Pen", "P", "Resulting Pen", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Color c = Color.Black;
            DA.GetData(0, ref c);
            double f = 2;
            DA.GetData(1, ref f);
            Brush b = new SolidBrush(c);
            Pen p = new Pen(b, (float)f);

            int dash = 0;
            DA.GetData(2, ref dash);

            p.DashStyle = Styles.dash[dash];
            
            DA.SetData(0, p);
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
                return Resources.Pen_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{52d0eba2-a85a-44e2-8c7c-8e658dd11a83}"); }
        }
    }
}