using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillaPilla
{
    internal class world
    {
        public float x, y, width, heigth;
        public DAM.Image Image;
        public float r, g, b, a;

        public void Render(ICanvas canvas)
        {
            canvas.FillRectangle(this.x, this.y, this.width, this.heigth, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, this.a);
        }
    }
}
