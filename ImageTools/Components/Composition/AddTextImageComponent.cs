using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using ImageTools.Properties;
using ImageTools.Utilities;

namespace ImageTools.Components
{
    public class AddTextImageComponent : ImageCompositionToolbox
    {
        /// <summary>
        /// Initializes a new instance of the AddTextImageComponent class.
        /// </summary>
        public AddTextImageComponent()
          : base("AddTextImage", "txtImg", "Add text on an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddTextParameter("Text", "T", "Text to write on Image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Size", "S", "Size of the Text", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("Align", "A", "Alignment of the Text", GH_ParamAccess.item, 0);

            Param_Integer AlignParam = pManager[3] as Param_Integer;
            AlignParam.AddNamedValue("Top Left", 0);
            AlignParam.AddNamedValue("Top Mid", 1);
            AlignParam.AddNamedValue("Top Right", 2);
            AlignParam.AddNamedValue("Mid Left", 3);
            AlignParam.AddNamedValue("Mid", 4);
            AlignParam.AddNamedValue("Mid Right", 5);
            AlignParam.AddNamedValue("Bot Left", 6);
            AlignParam.AddNamedValue("Bot Mid", 7);
            AlignParam.AddNamedValue("Bot Right", 8);

            pManager.AddColourParameter("Color", "C", "Colour of the Text", GH_ParamAccess.item, Color.Black);
            pManager.AddTextParameter("Font", "F", "Font to Use", GH_ParamAccess.item, "Arial");
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
            string text = "";
            DA.GetData(1, ref text);
            int size = 1;
            DA.GetData(2, ref size);
            int align = 0;
            DA.GetData(3, ref align);
            Color C = Color.Black;
            DA.GetData(4, ref C);
            string font = "Arial";
            DA.GetData(5, ref font);


            DA.SetData(0, ImageComposition.AddTextImage(sourceImage, text, size, align, C, font));
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
                return Resources.Text_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{293e7425-313c-493e-922f-0cb5919b8864}"); }
        }
    }
}
