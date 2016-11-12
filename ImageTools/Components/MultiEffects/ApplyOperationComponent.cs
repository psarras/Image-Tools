//using System;
//using System.Collections.Generic;
//using System.Drawing;

//using Grasshopper.Kernel;
//using Rhino.Geometry;
//using Grasshopper.Kernel.Parameters;
//using AForge.Imaging.Filters;
//using ImageTools.Utilities;

//namespace ImageTools.Components
//{
//    public class ApplyOperationComponent : MultiImageEffectToolBox
//    {
//        /// <summary>
//        /// Initializes a new instance of the ApplyOperationComponent class.
//        /// </summary>
//        /// 


//        public ApplyOperationComponent()
//            : base("Image_Operation", "ImgOperation", "Do basic operations between two images")
//        {
//        }

//        /// <summary>
//        /// Registers all the input parameters for this component.
//        /// </summary>
//        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
//        {
//            pManager.AddGenericParameter("image1", "img1", "image to manipulate", GH_ParamAccess.item);
//            pManager.AddGenericParameter("image2", "img2", "image to manipulate", GH_ParamAccess.item);
//            pManager.AddIntegerParameter("Operation", "O", "Operation To Apply", GH_ParamAccess.item, 0);

//            Param_Integer param = pManager[2] as Param_Integer;

//            foreach (KeyValuePair<string, int> pair in Filters.operations)
//            {
//                param.AddNamedValue(pair.Key, pair.Value);
//            }

//        }

//        /// <summary>
//        /// Registers all the output parameters for this component.
//        /// </summary>
//        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
//        {
//            pManager.AddGenericParameter("New Image", "img", "Manipulated Image", GH_ParamAccess.item);
//        }

//        /// <summary>
//        /// This is the method that actually does the work.
//        /// </summary>
//        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
//        protected override void SolveInstance(IGH_DataAccess DA)
//        {
//            Bitmap sourceImage = null;
//            DA.GetData(0, ref sourceImage);
//            Bitmap sourceImage2 = null;
//            DA.GetData(1, ref sourceImage2);
//            int op = 0;
//            DA.GetData(2, ref op);

//            sourceImage = ImageUtil.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
//            sourceImage2 = ImageUtil.convert(sourceImage2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

//            Bitmap filteredImage = sourceImage;
//            IFilter myFilter;


//            if (op == Filters.operations["Add"])
//            {
//                myFilter = new Add(sourceImage2);
//                filteredImage = myFilter.Apply(sourceImage);
//            }
//            else if (op == Filters.operations["Subtract"])
//            {
//                myFilter = new Subtract(sourceImage2);
//                filteredImage = myFilter.Apply(sourceImage);
//            }
//            else if (op == Filters.operations["Intersect"])
//            {
//                myFilter = new Intersect(sourceImage2);
//                filteredImage = myFilter.Apply(sourceImage);
//            }
//            else if (op == Filters.operations["Difference"])
//            {
//                myFilter = new Difference(sourceImage2);
//                filteredImage = myFilter.Apply(sourceImage);
//            }
//            else if (op == Filters.operations["Merge"])
//            {
//                myFilter = new Merge(sourceImage2);
//                filteredImage = myFilter.Apply(sourceImage);
//            }

//            DA.SetData(0, filteredImage);
//        }

//        /// <summary>
//        /// Provides an Icon for the component.
//        /// </summary>
//        protected override System.Drawing.Bitmap Icon
//        {
//            get
//            {
//                //You can add image files to your project resources and access them like this:
//                // return Resources.IconForThisComponent;
//                return null;
//            }
//        }

//        /// <summary>
//        /// Gets the unique ID for this component. Do not change this ID after release.
//        /// </summary>
//        public override Guid ComponentGuid
//        {
//            get { return new Guid("{a5cd22eb-d336-401c-b3e1-c9bdfff6bd8e}"); }
//        }
//    }
//}