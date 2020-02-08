using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components.Utilities
{
    public class CountIMGColours : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the CountAllPixels class.
        /// </summary>
        public CountIMGColours()
          : base("CountPixels", "CountPx",
                "Count pixels on an image based on the given color")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "Colors Found", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Frequency", "F", "Number of Pixels", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap img = null;
            DA.GetData(0, ref img);
            Dictionary<int, int> dictionary = ImageUtil.countColor(img);

            List<int> frequency = new List<int>();
            List<Color> colors = new List<Color>();

            int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(img.PixelFormat);
            bytesPerPixel /= 8;

            foreach (KeyValuePair<int, int> pair in dictionary)
            {
                switch (bytesPerPixel)
                {
                    case 2:
                        colors.Add(ImageUtil.byteGreyAlpha(pair.Key));
                        break;
                    case 3:
                        colors.Add(ImageUtil.byteARG(pair.Key));
                        break;
                    case 4:
                        colors.Add(ImageUtil.byteARGB(pair.Key));
                        break;
                    default:
                        colors.Add(ImageUtil.byteARG(pair.Key));
                        break;
                }
                
                frequency.Add(pair.Value);
            }

            DA.SetDataList(0, colors);
            DA.SetDataList(1, frequency);
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
                return Resources.Count_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{b7486c70-94d7-4658-8310-330bfdd76200}"); }
        }
    }
}