using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class FormatImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the FormatImageComponent class.
        /// </summary>
        public FormatImageComponent()
          : base("FormatImage", "FImage", "Convert image from one format to another")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Format", "F", "Change the Format of the Image", GH_ParamAccess.item, 3);

            Param_Integer param = pManager[1] as Param_Integer;

            foreach (KeyValuePair<PixelFormat, int> pair in Formats.formats)
            {
                param.AddNamedValue(pair.Key.ToString(), pair.Value);
            }

            pManager.AddIntegerParameter("DPI", "DPI", "DPI of the new Image", GH_ParamAccess.item, 96);

            Param_Integer DPIparam = pManager[2] as Param_Integer;

            DPIparam.AddNamedValue("72 DPI", 72);
            DPIparam.AddNamedValue("96 DPI", 96);
            DPIparam.AddNamedValue("150 DPI", 150);
            DPIparam.AddNamedValue("300 DPI", 300);
            DPIparam.AddNamedValue("600 DPI", 600);

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
            int format = 0;
            DA.GetData(1, ref format);
            int dpi = 96;
            DA.GetData(2, ref dpi);

            List<PixelFormat> myFormats = Formats.formats.Keys.ToList();

            if (format < 0)
            {
                format = 0;
            } else if (format > myFormats.Count - 1)
            {
                format = myFormats.Count - 1;
            }


            PixelFormat myFormat = (PixelFormat)myFormats[format];

            Bitmap newImg = ImageUtil.convert(sourceImage, myFormat);

            newImg.SetResolution(dpi, dpi);

            DA.SetData(0, newImg);
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
                return Resources.Format_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{4e0b03d7-b26b-4b3d-b9e8-c87107fe1277}"); }
        }
    }
}