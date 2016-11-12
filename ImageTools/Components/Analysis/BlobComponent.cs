using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class BlobComponent : ImageAnalysisToolbox
    {
        /// <summary>
        /// Initializes a new instance of the BlobComponent class.
        /// </summary>
        public BlobComponent()
          : base("BlobDetect", "Blob", "Detect blobs on an image based on size")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Width Limits", "W", "Size limits to test", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Height Limits", "H", "Size limits to test", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Coupled Limits", "C", "Are limits Coupled?", GH_ParamAccess.item, true);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Blob Image", "B", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Labeled Image", "L", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Blob Pieces", "Bs", "Manipulated Image", GH_ParamAccess.list);
            pManager.AddGenericParameter("Rect Image", "R", "Manipulated Image", GH_ParamAccess.item);
            pManager.AddGenericParameter("Shapes Image", "S", "Manipulated Image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap img = null;
            DA.GetData(0, ref img);
            Interval w = new Interval(0, img.Width);
            DA.GetData(1, ref w);
            Interval h = new Interval(0, img.Height);
            DA.GetData(2, ref h);
            Boolean coupled = true;
            DA.GetData(3, ref coupled);


            Tuple<Bitmap, Bitmap, Bitmap, Bitmap, List<Bitmap>> results;
            results = ImageConstruct.blob(img, w, h, coupled);
            DA.SetData(0, results.Item1);
            DA.SetData(1, results.Item2);
            DA.SetData(3, results.Item3);
            DA.SetData(4, results.Item4);
            DA.SetDataList(2, results.Item5);

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
                return Resources.Blob_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{2432bb40-51ef-4780-ba9a-929e8a4ff508}"); }
        }
    }
}