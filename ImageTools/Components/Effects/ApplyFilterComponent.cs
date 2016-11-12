using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using AForge.Imaging.Filters;
using AForge.Imaging.ColorReduction;
using AForge.Imaging.Textures;
using Grasshopper.Kernel.Parameters;
using ImageTools.Utilities;
using Vignettes;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class ApplyFilterComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ApplyFilterComponent()
            : base("ApplyFilter", "ApplyFilter", "Apply different filters on an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {    
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntegerParameter("filter", "filter", "What filter to apply", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[1] as Param_Integer;

            foreach (KeyValuePair<string, int> pair in Filters.filters)
            {
                param.AddNamedValue(pair.Key, pair.Value);
            } 

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
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);
            int filter = 0;
            DA.GetData(1, ref filter);

            //VignetteEffect.ApplyEffectCircleEllipseDiamond(sourceImage, 0, new System.Drawing.Point(0, 0), Color.Black);

            DA.SetData(0, ImageFilter.FilterImage(sourceImage, filter));
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Resources.Effect_Image;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{f15a654a-118f-4dc7-8986-2144d911a5dc}"); }
        }
    }
}
