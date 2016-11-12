using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components.Create
{
    public class ColorAutoPalette : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the ColorAutoPalette class.
        /// </summary>
        public ColorAutoPalette()
            : base("ColorAutoPalette", "ColorAutoPalette",
                "Description")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Number", "N", "Number of colours", GH_ParamAccess.item, 3);
            pManager.AddNumberParameter("Range", "R", "Range of the spectrum to sample", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("Seed", "S", "Seed to use for randomized parameters", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Colour", "C", "Resulting Colour", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int n = 3;
            DA.GetData(0, ref n);
            double r = 1;
            DA.GetData(1, ref r);
            int s = 1;
            DA.GetData(2, ref s);

            Random val = new Random(s);
            
            Vector3d con = new Vector3d(val.NextDouble(), val.NextDouble(), val.NextDouble());
            Vector3d mul = new Vector3d(3 * val.NextDouble(), 3 * val.NextDouble(), 3 * val.NextDouble());
            Vector3d rep = new Vector3d(val.NextDouble(), val.NextDouble(), val.NextDouble());
            Vector3d pha = new Vector3d(val.NextDouble(), val.NextDouble(), val.NextDouble());

            List<double> values = new List<double>();
            double step = r/(n-1);
            double temp = 0;
            for(int i = 0; i < n-1; i++)
            {
                values.Add(temp);
                temp += step;
            }
            values.Add(temp);

            List<Color> cols = new List<Color>();
            foreach(double v in values)
            {
                Vector3d vCol = ColorUtil.getColorPretty(v, con, mul, rep, pha);
                cols.Add(Color.FromArgb((int)(vCol.X * 255), (int)(vCol.Y * 255), 
                                        (int)(vCol.Z * 255)));
            }


            
            DA.SetDataList(0, cols);
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
                return Resources.Pretty_Gradient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{31bd7abe-018f-4e63-b445-fc3a33cfd959}"); }
        }
    }
}