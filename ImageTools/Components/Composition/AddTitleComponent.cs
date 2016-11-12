using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class AddTitleComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the AddTitleComponent class.
        /// </summary>
        public AddTitleComponent()
          : base("AddTitle", "Title", "Add a Title Text by extending the image on the top or bottom side")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "Img", "Image to add Title To", GH_ParamAccess.item);
            pManager.AddTextParameter("Text", "T", "Text to add as Title", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Align", "A", "Alignment of Text", GH_ParamAccess.item, 0);

            Param_Integer AlignParam = pManager[2] as Param_Integer;

            AlignParam.AddNamedValue("Top Left", 0);
            AlignParam.AddNamedValue("Top Mid", 1);
            AlignParam.AddNamedValue("Top Right", 2);
            AlignParam.AddNamedValue("Bot Left", 3);
            AlignParam.AddNamedValue("Bot Mid", 4);
            AlignParam.AddNamedValue("Bot Right", 5);

            pManager.AddIntegerParameter("Size", "S", "Size as a percentage of Height. 10 = 10%", GH_ParamAccess.item, 10);

            Param_Integer SizeParam = pManager[3] as Param_Integer;

            SizeParam.AddNamedValue("5%", 5);
            SizeParam.AddNamedValue("7%", 7);
            SizeParam.AddNamedValue("10%", 10);
            SizeParam.AddNamedValue("15%", 15);
            SizeParam.AddNamedValue("20%", 20);
            SizeParam.AddNamedValue("25%", 25);
            SizeParam.AddNamedValue("50%", 50);

            pManager.AddColourParameter("Text Color", "tC", "Text Color", GH_ParamAccess.item, Color.Black);
            pManager.AddColourParameter("Background Color", "bC", "Background Color", GH_ParamAccess.item, Color.White);
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
            Bitmap img = null;
            DA.GetData(0, ref img);
            string text = "";
            DA.GetData(1, ref text);
            int align = 0;
            DA.GetData(2, ref align);
            int size = 10;
            DA.GetData(3, ref size);
            Color cText = Color.Black;
            DA.GetData(4, ref cText);
            Color bText = Color.White;
            DA.GetData(5, ref bText);
            string font = "Arial";
            DA.GetData(6, ref font);

            DA.SetData(0, ImageComposition.AddTitle(img, text, size / 100.0f, align, cText, bText, font));


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
                return Resources.Title_Image;
                
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{48e2cf20-a07c-4ec8-bb22-d74b5c2be2ec}"); }
        }
    }
}