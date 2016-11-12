using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Utilities;
using AForge.Imaging.Filters;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class ThresholdImageComponents : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the ThresholdComponent class.
        /// </summary>
        public ThresholdImageComponents()
          : base("ThresholdImage", "ThresImage", "Threshold an image based on a value")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Value", "v", "Values to manipulate Threshold", GH_ParamAccess.item, 0);
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
            int value = 0;
            DA.GetData(1, ref value);


            IFilter myFilter = new Threshold(value);
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            sourceImage = myFilter.Apply(sourceImage);

            DA.SetData(0, sourceImage);
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
                return Resources.Threshold_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{bc79b95f-f149-47e2-b284-25db85f8d8e9}"); }
        }
    }
}