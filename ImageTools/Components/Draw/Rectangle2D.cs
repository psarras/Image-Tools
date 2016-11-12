using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using ImageTools.Draw;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class Rectangle2D : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the Rectangle2D class.
        /// </summary>
        public Rectangle2D()
          : base("Rectangle2D", "Rectangle2D",
              "Description")
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
            Rectangle3d rec = new Rectangle3d(); //new Rectangle3d(new Plane(), sourceImage.Width, sourceImage.Height);
            DA.GetData(0, ref rec);

            Pen outline = null;
            DA.GetData(1, ref outline);

            Pen fill = null;
            DA.GetData(2, ref fill);

            RectangleShape shape = new RectangleShape(rec);

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
                return Resources.Rectangle_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{6f9d6790-b0a3-41f6-8af1-4cccbef3cd79}"); }
        }
    }
}