using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using Grasshopper.Kernel.Parameters;
using System.Drawing.Drawing2D;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class TexturePen : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the TextureBrush class.
        /// </summary>
        public TexturePen()
          : base("TextureBrush", "textBrush",
              "Create A textured Pen")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "A bitmap to use for the texture brush", GH_ParamAccess.item);
            //pManager.AddColourParameter("Color", "C", "Colour to use for this pen", GH_ParamAccess.item, Color.Black);
            pManager.AddNumberParameter("Size", "S", "Size of hte Brush", GH_ParamAccess.item, 2);
            pManager.AddIntegerParameter("WrapMode", "W", "Wrapmode", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[2] as Param_Integer;

            foreach (KeyValuePair<int, WrapMode> pair in Styles.wrap)
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
            Bitmap bitmap = null;
            DA.GetData(0, ref bitmap);
            //Color c = Color.Black;
            //DA.GetData(1, ref c);
            double f = 2;
            DA.GetData(1, ref f);
            int mode = 0;
            DA.GetData(2, ref mode);

            Brush b = new TextureBrush((Image)bitmap, Styles.wrap[mode]);
            
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
                return Resources.Brush_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{4a4d6130-1941-4db2-b97e-9e47a3dab524}"); }
        }
    }
}