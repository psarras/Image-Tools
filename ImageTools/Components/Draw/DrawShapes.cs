using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Draw;
using ImageTools.Utilities;
using System.Drawing.Imaging;
using ImageTools.Properties;

namespace ImageTools.Components.Draw
{
    public class DrawShapes : ImageDrawToolbox
    {
        /// <summary>
        /// Initializes a new instance of the DrawShapes class.
        /// </summary>
        public DrawShapes()
          : base("DrawShapes", "DrawShapes", "Draw Shapes on an Image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "Canvas to draw on", GH_ParamAccess.item);
            pManager.AddGenericParameter("Shapes", "S", "Shaped to draw", GH_ParamAccess.list);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "I", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);
            List<Shape2D> shapes = new List<Shape2D>();
            DA.GetDataList(1, shapes);

            Bitmap canvas = ImageUtil.convert(sourceImage, PixelFormat.Format32bppArgb);
            Graphics gIMG = Graphics.FromImage(canvas);

            
            foreach (var s in shapes)
            {
                s.draw(gIMG);
            }

            DA.SetData(0, canvas);
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
                return Resources.DrawShape_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{302396e6-faaf-4aef-bdb6-671bb943030c}"); }
        }
    }
}