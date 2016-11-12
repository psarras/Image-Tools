using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class AddNumberImageComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the AddNumberImageComponent class.
        /// </summary>
        public AddNumberImageComponent()
          : base("AddNumberImage", "addNumIMG", "Add a number on a list of images based on their order")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "Img", "Image to add Title To", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Align", "A", "Alignment of Text", GH_ParamAccess.item, 0);

            Param_Integer AlignParam = pManager[1] as Param_Integer;

            AlignParam.AddNamedValue("Top Left", 0);
            AlignParam.AddNamedValue("Top Right", 1);
            AlignParam.AddNamedValue("Bot Left", 2);
            AlignParam.AddNamedValue("Bot Right", 3);

            pManager.AddIntegerParameter("Size", "S", "Size as a percentage of Height. 10 = 10%", GH_ParamAccess.item, 10);

            Param_Integer SizeParam = pManager[2] as Param_Integer;

            SizeParam.AddNamedValue("5%", 5);
            SizeParam.AddNamedValue("7%", 7);
            SizeParam.AddNamedValue("10%", 10);
            SizeParam.AddNamedValue("15%", 15);
            SizeParam.AddNamedValue("20%", 20);
            SizeParam.AddNamedValue("25%", 25);
            SizeParam.AddNamedValue("50%", 50);

            pManager.AddColourParameter("Text Color", "tC", "Text Color", GH_ParamAccess.item, Color.Black);
            pManager.AddTextParameter("Font", "F", "Font to use", GH_ParamAccess.item, "Calibri");

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
            List<Bitmap> img = new List<Bitmap>();
            DA.GetDataList(0, img);
            int align = 0;
            DA.GetData(1, ref align);
            int size = 0;
            DA.GetData(2, ref size);
            Color Ftext = Color.White;
            DA.GetData(3, ref Ftext);
            string font = "Calibri";
            DA.GetData(4, ref font);

            DA.SetDataList(0, ImageComposition.AddNumbering(img, size, align, Ftext, font));
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
                return Resources.AutoNumber_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{aaa0fd4a-d949-43a6-824e-108d8d078d3d}"); }
        }
    }
}