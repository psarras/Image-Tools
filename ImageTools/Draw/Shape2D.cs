using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTools.Draw
{
    public interface Shape2D
    {
        void draw(Graphics gIMG);
        void outline(Pen p);
        void fill(Pen b);
    }
}
