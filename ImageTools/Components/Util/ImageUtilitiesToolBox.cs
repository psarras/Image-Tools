using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasshopper.Kernel;
using ImageTools.Components;

namespace ImageTools.Components
{
    public class ImageUtilitiesToolBox: ImagesPlugin
    {
        public ImageUtilitiesToolBox(string Name, string nickname, string description)
            : base(Name, nickname, description, "Utilities") 
        { 
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            throw new NotImplementedException();
        }

        public override Guid ComponentGuid
        {
            get { throw new NotImplementedException(); }
        }
    }
}
