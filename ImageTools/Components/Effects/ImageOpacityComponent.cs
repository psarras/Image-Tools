using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class ImageOpacityComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ImageOpacityComponent class.
        /// </summary>
        public ImageOpacityComponent()
          : base("ImageOpacity", "ImgOpacity", "Change the opacity of an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddNumberParameter("Value", "V", "Value to modify Opacity", GH_ParamAccess.item, 0.0);
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

            double Value = 0;
            DA.GetData(1, ref Value);

            //create a Bitmap the size of the image provided
            Bitmap bmp = new Bitmap(sourceImage.Width, sourceImage.Height);

            //create a graphics object from the image
            Graphics gfx = Graphics.FromImage(bmp);

            //create a color matrix object
            System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix();


            //set the opacity
            matrix.Matrix33 = Convert.ToSingle(Value);

            //create image attributes
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();

            //set the color(opacity) of the image
            attributes.SetColorMatrix(matrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

            //now draw the image
            gfx.DrawImage(sourceImage, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attributes);

            DA.SetData(0, bmp);
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
                return Resources.Opacity_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{ae901116-bf07-4430-8be7-290166287f66}"); }
        }
    }
}