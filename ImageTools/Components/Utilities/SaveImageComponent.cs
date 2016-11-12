using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class SaveImageComponent : ImageUtilitiesToolBox
    {
        /// <summary>
        /// Initializes a new instance of the SaveImageComponent class.
        /// </summary>
        public SaveImageComponent()
          : base("SaveImage", "SaveImg", "Save a bitmap")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddTextParameter("Folder", "F", "Folder to Save to", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "Image Name", GH_ParamAccess.item, "myImage");
            pManager.AddTextParameter("Format", "f", "Image format. 'jpg', 'png'...", GH_ParamAccess.item, "png");
            pManager.AddBooleanParameter("Save", "S", "Save Image", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap sourceImage = null;
            DA.GetData(0, ref sourceImage);
            string folder = "";
            DA.GetData(1, ref folder);
            string name = "";
            DA.GetData(2, ref name);
            string format = "";
            DA.GetData(3, ref format);
            Boolean save = false;
            DA.GetData(4, ref save);

            if (save) sourceImage.Save(folder + "\\" + name + "." + format);
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
                //return Resources.SaveImage_ICON;
                return Resources.SaveImage;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{350240af-e6d9-4ecd-9571-5961bce5b6a9}"); }
        }
    }
}