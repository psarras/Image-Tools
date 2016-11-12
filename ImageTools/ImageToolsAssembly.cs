using Grasshopper.Kernel;
using ImageTools.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTools
{
    public class ImageToolsAssembly : GH_AssemblyPriority
    {
        public ImageToolsAssembly() { }

        public override GH_LoadingInstruction PriorityLoad()
        {
            Grasshopper.Instances.ComponentServer.AddCategoryIcon("Images", Resources.Image_Icon_16);
            return (GH_LoadingInstruction)0;
        }

    }
}
