using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Draw;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class CopyImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the MoveImageComponent class.
        /// </summary>
        public CopyImageComponent()
          : base("CutImage", "CutImg", "Copy Image part into an empty Canvas with the same size")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddRectangleParameter("sourceRectangle", "Rs", "Region to copy from", GH_ParamAccess.item);
            pManager.AddRectangleParameter("destinationRectangle", "Rd", "Destination Region to paste into", GH_ParamAccess.list);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("New Image", "img", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);
            Rectangle3d rec1 = new Rectangle3d();
            DA.GetData(1, ref rec1);
            List<Rectangle3d> rec2 = new List<Rectangle3d>();
            DA.GetDataList(2, rec2);

            Bitmap clone = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);

            Graphics g = Graphics.FromImage(clone);
            Rectangle source = ShapeUtil.toRec2D(rec1);

            foreach (var r in rec2)
            {
                Rectangle dest = ShapeUtil.toRec2D(r);
                g.DrawImage(sourceImage, dest, source, GraphicsUnit.Pixel);
            }

            DA.SetData(0, clone);
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
                return Resources.Cut_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{bffdc443-7385-4a8c-919f-c7080da2db4f}"); }
        }
    }
}