using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Draw;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class Curve2D : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the Curve2D class.
        /// </summary>
        public Curve2D()
          : base("Curve2D", "Curve2D",
              "Create a Curve to Draw on an Image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "CP", "Points to use for the Path", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Closed", "C", "is this Closed", GH_ParamAccess.item, false);
            pManager.AddNumberParameter("Tension", "T", "Specify the tension", GH_ParamAccess.item, 0);
            pManager.AddGenericParameter("Outline", "O", "Pen to use to draw the Outline", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddGenericParameter("Fill", "F", "Pen to use to Fill the shape", GH_ParamAccess.item);
            pManager[4].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shape", "S", "Shape to Draw", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> cp = new List<Point3d>();
            DA.GetDataList(0, cp);

            bool closed = false;
            DA.GetData(1, ref closed);

            double tension = 0;
            DA.GetData(2, ref tension);

            Pen outline = null;
            DA.GetData(3, ref outline);

            Pen fill = null;
            DA.GetData(4, ref fill);

            Shape2D shape = new CurveShape(cp, closed, (float)tension);

            if (outline != null) shape.outline(outline);
            if (fill != null) shape.fill(fill);

            DA.SetData(0, shape);
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
                return Resources.Curve_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{778cd071-2e7b-42c9-b68a-7a0c35ca42c0}"); }
        }
    }
}