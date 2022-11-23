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
            RenderBullets(canvas, bullets);
            RenderSoldier(canvas, enemies);
        }

        public void RenderBullets(ICanvas canvas,List<GameObject> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i].MoveBullet(this.maxY,list);
                list[i].Render(canvas); 
            }
        }

        public void RenderSoldier(ICanvas canvas,List<GameObject> list)
        {
            for(int i = 0; i< list.Count; i++)
            {
                list[i].Render(canvas);
                list[i].MoveSoldier(this.minY, list);
            }
        }
    }
}
