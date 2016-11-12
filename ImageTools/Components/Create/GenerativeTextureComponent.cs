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
    public class GenerativeTextureComponent : ImageCreateToolbox
    {
        /// <summary>
        /// Initializes a new instance of the GenerativeTextureComponent class.
        /// </summary>
        public GenerativeTextureComponent()
          : base("GenerativeTexture", "genTexture", "Create a texture from generative algorithms")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "img", "Image to match", GH_ParamAccess.item);
            pManager.AddNumberParameter("LowCutOff", "L", "LowCutOff [0, 1]. Can exceed these values", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("HighCutOff", "H", "HighCutOff [0, 1]. Can exceed these values", GH_ParamAccess.item, 0.01);

            pManager.AddIntegerParameter("Texture", "T", "Texture to create", GH_ParamAccess.item, 0);

            Param_Integer param = pManager[3] as Param_Integer;

            foreach (KeyValuePair<string, int> pair in Filters.textures)
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
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Bitmap img = null;
            DA.GetData(0, ref img);
            double param1 = 0;
            DA.GetData(1, ref param1);
            double param2 = 0;
            DA.GetData(2, ref param2);
            int texture = 0;
            DA.GetData(3, ref texture);

            DA.SetData(0, ImageConstruct.CreateTexture(img, param1, param2, texture));
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
                return Resources.Gen_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{a946bcc7-38cd-4b39-a666-c7737b8353de}"); }
        }
    }
}