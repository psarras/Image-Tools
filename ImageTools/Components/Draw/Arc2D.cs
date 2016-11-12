using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Draw;
using System.Windows.Forms;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class Arc2D : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the Arc2D class.
        /// </summary>
        public Arc2D()
          : base("Arc2D", "Arc2D",
              "Create an Arc to Draw on an Image")
        {
        }

        bool pie = false;

        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);
            ToolStripMenuItem item = Menu_AppendItem(menu, "Pie", Menu_AbsoluteClicked, true, pie);
            
        }

        private void Menu_AbsoluteClicked(object sender, EventArgs e)
        {
            RecordUndoEvent("Pie");
            pie = !pie;
            ExpireSolution(true);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "Center Point of the Arc", GH_ParamAccess.item, new Point3d());
            pManager.AddNumberParameter("Radiues", "R", "Radius of the Arc", GH_ParamAccess.item, 100);
            pManager.AddNumberParameter("StartAngle", "S", "The Start Angle", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("SweepAngle", "A", "The Sweep Angle", GH_ParamAccess.item, 90);
            pManager.AddGenericParameter("Outline", "O", "Pen to use to draw the Outline", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddGenericParameter("Fill", "F", "Pen to use to Fill the shape", GH_ParamAccess.item);
            pManager[5].Optional = true;
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
            Point3d point = new Point3d();
            DA.GetData(0, ref point);

            double size = 0;
            DA.GetData(1, ref size);

            double startAngle = 0;
            DA.GetData(2, ref startAngle);

            double sweepAngle = 0;
            DA.GetData(3, ref sweepAngle);

            Pen outline = null;
            DA.GetData(4, ref outline);

            Pen fill = null;
            DA.GetData(5, ref fill);

            Shape2D shape = new ArcShape(point, size, startAngle, sweepAngle, pie);
            
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
                return Resources.Arc_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{623cfbb0-5471-4f6c-bdc7-cc1a5c353f2a}"); }
        }
    }
}