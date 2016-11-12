using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTools.Components
{
    public class ImagesPlugin : GH_Component
    {
        public ImagesPlugin(string Name, string nickname, string description, string subcategory)
            : base(Name, nickname, description, "Images", subcategory) 
        {
        }
        public override Guid ComponentGuid
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            throw new NotImplementedException();
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            throw new NotImplementedException();
        }
    }
}
