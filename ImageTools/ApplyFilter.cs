using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using AForge.Imaging.Filters;
using ImageTools.Image.Utilities;
using AForge.Imaging.ColorReduction;
using AForge.Imaging.Textures;


namespace ImageTools
{
    public class ApplyFilter : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ApplyFilter()
            : base("ApplyFilter", "ApplyFilter",
                "Apply simple Effects on an Image",
                "Images", "Effects")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddTextParameter("filter", "filter", "What filter to apply", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("newImage", "NewImg", "Manipulated Image", GH_ParamAccess.item);
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
            string filter = "";
            DA.GetData(1, ref filter);
            

            sourceImage = ImageUtilities.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IFilter myFilter;
            Bitmap filteredImage = sourceImage;
            //Grayscale.CommonAlgorithms.Y.Apply
            switch (filter)
            {
                case "Greyscale":
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    filteredImage = sourceImage;
                    break;
                case "Sepia":
                    Console.Write("Applying: " + filter);
                    myFilter = new Sepia();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;
                case "Invert":
                    Console.Write("Applying: " + filter);
                    sourceImage = ImageUtilities.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    myFilter = new Invert();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;
                case "RotateChannel":
                    Console.Write("Applying: " + filter);
                    myFilter = new RotateChannels();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;
                case "Threshold": //Need Extended Version
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    myFilter = new Threshold();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "FloydFilter":
                    Console.Write("Applying: " + filter);
                    //sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    //myFilter = new FloydSteinbergColorDithering();
                    FloydSteinbergColorDithering myReduction = new FloydSteinbergColorDithering();
                    filteredImage = myReduction.Apply(sourceImage);
                    //filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "OrderedDithering":
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    myFilter = new OrderedDithering();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Sharpen":
                    Console.Write("Applying: " + filter);
                    myFilter = new Sharpen();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "DifferenceEdgeDetector":
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    myFilter = new DifferenceEdgeDetector();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "HomogenityEdgeDetector":
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    myFilter = new HomogenityEdgeDetector();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Sobel":
                    Console.Write("Applying: " + filter);
                    sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
                    myFilter = new SobelEdgeDetector();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Jitter":
                    Console.Write("Applying: " + filter);
                    myFilter = new Jitter(); //Needs Expand
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "OilPainting":
                    Console.Write("Applying: " + filter);
                    myFilter = new OilPainting(); //Needs Expand
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "TextureFiltering":
                    Console.Write("Applying: " + filter);
                    sourceImage = ImageUtilities.convert(sourceImage, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    myFilter = new Texturer(new TextileTexture(), 1.0, 0.8); //Needs Expand
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Median":
                    Console.Write("Applying: " + filter);
                    myFilter = new Median();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Mean":
                    Console.Write("Applying: " + filter);
                    myFilter = new Mean();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;

                case "Blur": //Need Extended Version
                    Console.Write("Applying: " + filter);
                    myFilter = new GaussianBlur();
                    filteredImage = myFilter.Apply(sourceImage);
                    break;
                default:
                    Console.Write("No Filter");
                    break;
            }

            Console.Write(filteredImage.PixelFormat.ToString());
            Console.Write(sourceImage.PixelFormat.ToString());
            filteredImage = ImageUtilities.convert(filteredImage, System.Drawing.Imaging.PixelFormat.Format32bppArgb);




            DA.SetData(0, filteredImage);
            
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
                return null;
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
