using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Draw;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class Ellipse2D : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the Ellipse2D class.
        /// </summary>
        public Ellipse2D()
          : base("Ellipse2D", "Ellipse2D",
              "Create an Ellipse Shape to draw on an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "R", "Rectangle to draw Inside the Ellipse", GH_ParamAccess.item);
            pManager.AddGenericParameter("Outline", "O", "Pen to use to draw the Outline", GH_ParamAccess.item);
            pManager.AddGenericParameter("Fill", "F", "Pen to use to Fill the shape", GH_ParamAccess.item);
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
            Rectangle3d rec = new Rectangle3d();
            DA.GetData(0, ref rec);

            Pen outline = null;
            DA.GetData(1, ref outline);

            Pen fill = null;
            DA.GetData(2, ref fill);

            EllipseShape shape = new EllipseShape(rec);

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
                return Resources.Ellipse_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{82762c50-245f-4608-a6c8-580cd178314a}"); }
        }
    }
}