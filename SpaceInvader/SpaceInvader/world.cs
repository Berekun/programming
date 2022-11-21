using System;
using DAM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    internal class World
    {
        public float minX, minY, maxX, maxY;
        public Image Image;
        public List<Image> list;
        public List<GameObject> bullets = new List<GameObject>();
        public List<GameObject> enemies = new List<GameObject>(); 
        public float r, g, b, a;

        public void Render(ICanvas canvas)
        {
            canvas.FillRectangle(this.minX, this.minY, this.maxX*2, this.maxY*2, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            RenderObjects(canvas, bullets);
        }

        public void RenderObjects(ICanvas canvas,List<GameObject> list)
        {
            foreach(GameObject go in list)
            {
                go.Move(this.maxY);
                go.Render(canvas);
            }

        }
    }
}
