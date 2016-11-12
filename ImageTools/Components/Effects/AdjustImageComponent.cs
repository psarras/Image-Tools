using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using AForge.Imaging.Filters;
using System.Drawing;
using ImageTools.Properties;

namespace ImageTools.Components.Effects
{
    public class AdjustImageComponent : SingleImageEffectToolBox
    {
        /// <summary>
        /// Initializes a new instance of the AdjustImageComponent class.
        /// </summary>
        public AdjustImageComponent()
          : base("AdjustImage", "AdjustIMG", "Adjust different factors on an image")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("image", "img", "image to manipulate", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Luminance", "L", "Luminance adjustment", GH_ParamAccess.item, new Interval(0, 1));
            pManager.AddIntervalParameter("Saturation", "S", "Saturation adjustment", GH_ParamAccess.item, new Interval(0, 1));
            pManager.AddIntervalParameter("Luma", "Y", "Y adjustment", GH_ParamAccess.item, new Interval(0, 255));
            pManager.AddIntervalParameter("Chroma_Blue", "Cb", "Chroma Blue adjustment", GH_ParamAccess.item, new Interval(-1, 1));
            pManager.AddIntervalParameter("Chroma_Red", "Cr", "CHroma Red adjustment", GH_ParamAccess.item, new Interval(-1, 1));
            pManager.AddIntervalParameter("Red", "R", "Red Adjustment", GH_ParamAccess.item, new Interval(0, 255));
            pManager.AddIntervalParameter("Green", "G", "Green Adjustment", GH_ParamAccess.item, new Interval(0, 255));
            pManager.AddIntervalParameter("Blue", "B", "Blue Adjustment", GH_ParamAccess.item, new Interval(0, 255));
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
            Interval L = new Interval(0, 1);
            DA.GetData("Luminance", ref L);
            Interval S = new Interval(0, 1);
            DA.GetData("Saturation", ref S);
            Interval Y = new Interval(0, 255);
            DA.GetData("Luma", ref Y);
            Interval Cb = new Interval(-1, 1);
            DA.GetData("Chroma_Blue", ref Cb);
            Interval Cr = new Interval(-1, 1);
            DA.GetData("Chroma_Red", ref Cr);
            Interval R = new Interval(0, 255);
            DA.GetData("Red", ref R);
            Interval G = new Interval(0, 255);
            DA.GetData("Green", ref G);
            Interval B = new Interval(0, 255);
            DA.GetData("Blue", ref B);

            Bitmap filteredImage = img;

            Interval bInterval = new Interval(0, 1);
            Interval Interval255 = new Interval(0, 255);
            Interval IntervalMinusOne = new Interval(-1, 1);

            //////////////////////////////////////////////////////////////////////////
            HSLLinear myHSLfilter = new HSLLinear();
            if (bInterval.IncludesInterval(L))
                myHSLfilter.InLuminance = new AForge.Range(Convert.ToSingle(L.Min), Convert.ToSingle(L.Max));
            if (bInterval.IncludesInterval(S))
                myHSLfilter.InSaturation = new AForge.Range(Convert.ToSingle(S.Min), Convert.ToSingle(S.Max));
            filteredImage = myHSLfilter.Apply(img);
            //////////////////////////////////////////////////////////////////////////
            YCbCrLinear myYCbCrfilter = new YCbCrLinear();
            if (Interval255.IncludesInterval(Y))
                myYCbCrfilter.InCb = new AForge.Range(Convert.ToSingle(Y.Min), Convert.ToSingle(Y.Max));
            if (IntervalMinusOne.IncludesInterval(Cb))
                myYCbCrfilter.InCb = new AForge.Range(Convert.ToSingle(Cb.Min), Convert.ToSingle(Cb.Max));
            if (IntervalMinusOne.IncludesInterval(Cr))
                myYCbCrfilter.InCr = new AForge.Range(Convert.ToSingle(Cr.Min), Convert.ToSingle(Cr.Max));
            filteredImage = myYCbCrfilter.Apply(filteredImage);
            //////////////////////////////////////////////////////////////////////////
            LevelsLinear myRGBfilter = new LevelsLinear();
            if (Interval255.IncludesInterval(R))
                myRGBfilter.InRed = new AForge.IntRange((int)(R.Min), (int)(R.Max));
            if (Interval255.IncludesInterval(G))
                myRGBfilter.InGreen = new AForge.IntRange((int)(G.Min), (int)(G.Max));
            if (Interval255.IncludesInterval(B))
                myRGBfilter.InBlue = new AForge.IntRange((int)(B.Min), (int)(B.Max));
            filteredImage = myRGBfilter.Apply(filteredImage);

            DA.SetData(0, filteredImage);
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
                return Resources.Adjust_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{12b47fae-fc71-4692-a094-703815a3796a}"); }
        }
    }
}