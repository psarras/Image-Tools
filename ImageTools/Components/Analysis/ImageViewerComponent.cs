using System;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.Kernel;
using ImageTools.Attributes;
using ImageTools.Utilities;
using ImageTools.Properties;

namespace ImageTools.Components
{
    public class ImageViewerComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ImageViewerComponent class.
        /// </summary>
        public ImageViewerComponent()
            : base("ImageViewer", "imgView",
                "View A Bitmap", "Images", "Analysis")
        {
        }

        private int[] size =
        {
            300,
            600,
            1000,
            200
        };

        private int pickSize = 0;

        private bool[] sizeOption =
        {
            true,
            false,
            false,
            false
        };

        public string resolution = "Resolution";
        public string dpi = "DPI";
        public string ratio = "Ratio";

        public override void CreateAttributes()
        {
            m_attributes = new GH_ImageAttributes(this);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

            ToolStripMenuItem toolStripMenuItem = GH_DocumentObject.Menu_AppendItem((ToolStrip)menu, "CustomSize");
            //GH_DocumentObject.Menu_AppendItem(toolStripMenuItem.DropDown, "MyNestedMenuItem");
            GH_DocumentObject.Menu_AppendTextItem(toolStripMenuItem.DropDown, "200",
                new GH_MenuTextBox.KeyDownEventHandler(this.KeyDown),
                new GH_MenuTextBox.TextChangedEventHandler(this.TextChanged), true);
            GH_DocumentObject.Menu_AppendItem(menu, "Small",
                new EventHandler(this.SizeOneClickedSmall), true, sizeOption[0]);
            GH_DocumentObject.Menu_AppendItem(menu, "Medium",
                new EventHandler(this.SizeOneClickedMedium), true, sizeOption[1]);
            GH_DocumentObject.Menu_AppendItem(menu, "Large",
                new EventHandler(this.SizeOneClickedLarge), true, sizeOption[2]);
        }

        private void TextChanged(GH_MenuTextBox sender, string text)
        {
            int s = 200;
            try
            {
                s = Convert.ToInt32(text);
            }
            catch (Exception)
            {
                throw new System.ArgumentException(text);
            }
            if (s < 200) s = 200;
            size[3] = s;
            text = "200";
            this.pickSize = 3;
            sizeOption[0] = false;
            sizeOption[1] = false;
            sizeOption[2] = false;
            sizeOption[3] = true;
            this.ExpireSolution(true);
        }

        private void KeyDown(GH_MenuTextBox sender, KeyEventArgs e)
        {

        }

        private void SizeOneClickedSmall(object sender, EventArgs e)
        {
            this.RecordUndoEvent("Size Small");
            this.pickSize = 0;
            sizeOption[0] = true;
            sizeOption[1] = false;
            sizeOption[2] = false;
            sizeOption[3] = false;
            this.ExpireSolution(true);
        }

        private void SizeOneClickedMedium(object sender, EventArgs e)
        {
            this.RecordUndoEvent("Size Medium");
            this.pickSize = 1;
            sizeOption[0] = false;
            sizeOption[1] = true;
            sizeOption[2] = false;
            sizeOption[3] = false;
            this.ExpireSolution(true);
        }

        private void SizeOneClickedLarge(object sender, EventArgs e)
        {
            this.RecordUndoEvent("Size Large");
            this.pickSize = 2;
            sizeOption[0] = false;
            sizeOption[1] = false;
            sizeOption[2] = true;
            sizeOption[3] = false;
            this.ExpireSolution(true);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //pManager.AddTextParameter("Path", "P", "Path To A File", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bitmap", "B", "A Bitmap Class", GH_ParamAccess.item);
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
            Bitmap img = null;
            DA.GetData(0, ref img);
            this.m_image = img;

            this.m_Size = new Size(this.size[this.pickSize],
                (int)(this.size[this.pickSize] * img.Height / img.Width));
            this.resolution = "Res: " + img.Width + " x " + img.Height;
            this.dpi = "DPI: " + img.HorizontalResolution;

            float val = (img.Width / (float)img.Height);
            float val1 = val * 1000;
            string s = "" + (int) (val1)/1000f;
            this.ratio = "" + Fraction.Parse(val).ToText() + " | " + s;
        }

        public Bitmap m_image { get; set; }
        public Size m_Size { get; set; }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resources.PictureFrame;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{fd54823f-fb0b-4eff-a371-eb6856a45c10}"); }
        }
    }
}