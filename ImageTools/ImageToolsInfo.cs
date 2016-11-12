using System;
using System.Drawing;
using Grasshopper.Kernel;
using ImageTools.Properties;

namespace ImageTools
{
    public class ImageToolsInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "ImageTools";
            }
        }

        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Resources.Image_Icon_24;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Tools for Editing, Capturing and Assempying Images";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("40aef661-8a1d-446f-bd7c-48601176c95e");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Stam";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
