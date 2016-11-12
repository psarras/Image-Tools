using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using System.Drawing.Drawing2D;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class HatchPen : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the HatchPen class.
        /// </summary>
        public HatchPen()
          : base("HatchPen", "HatchPen",
              "Create A Hatch Pen")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "Cf", "Colour to use for foreground", GH_ParamAccess.item, Color.White);
            pManager.AddColourParameter("Color", "Cb", "Colour to use for background", GH_ParamAccess.item, Color.Black);
            pManager.AddNumberParameter("Size", "S", "Size of hte Brush", GH_ParamAccess.item, 2);
            pManager.AddIntegerParameter("Hatchstyle", "H", "The hatchstyle to use", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[3] as Param_Integer;

            foreach (KeyValuePair<int, HatchStyle> pair in Styles.hatch)
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
            Color Cb = Color.Black;
            DA.GetData(0, ref Cb);
            Color Cf = Color.White;
            DA.GetData(1, ref Cf);
            double f = 2;
            DA.GetData(2, ref f);
            int mode = 0;
            DA.GetData(3, ref mode);

            Brush b = new HatchBrush(Styles.hatch[mode], Cf, Cb);
            Pen p = new Pen(b, (float)f);

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
                return Resources.Hatch_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{a25d26cd-b1b5-4675-bea5-dbd7932a32a6}"); }
        }
    }
}