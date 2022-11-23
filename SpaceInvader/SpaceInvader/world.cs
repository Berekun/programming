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

        public void Render(ICanvas canvas,World world,IWindow window,GameObject player)
        {
            canvas.FillRectangle(this.minX, this.minY, this.maxX*2, this.maxY*2, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            AnimateBullet(canvas, null, world, bullets, enemies);
            AnimateSoldier(canvas, null, world, enemies,bullets);
            RenderBullets(canvas, bullets,enemies,window,player);
            RenderSoldier(canvas, enemies,bullets,window,player);
        }

        public void RenderBullets(ICanvas canvas,List<GameObject> bullets,List<GameObject> soldiers,IWindow window,GameObject player)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Render(canvas);
                bullets[i].GameObjectColisionAll(bullets,soldiers,player,window);
            }
        }

        public void AnimateBullet(ICanvas canvas,IKeyboard keyboard,World world, List<GameObject> bullets, List<GameObject> soldier)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move(keyboard, world, bullets,soldier);
            }       
        }

        public void RenderSoldier(ICanvas canvas,List<GameObject> soldier, List<GameObject> bullets, IWindow window,GameObject player)
        {
            for(int i = 0; i< soldier.Count; i++)
            {
                soldier[i].Render(canvas);
                soldier[i].GameObjectColisionAll(bullets,soldier,player,window);
            }
        }

        public void AnimateSoldier(ICanvas canvas, IKeyboard keyboard, World world, List<GameObject> soldier,List<GameObject> bullets)
        {
            for (int i = 0; i < soldier.Count; i++)
            {
                soldier[i].Move(keyboard, world, bullets,soldier);

            }
        }
    }
}
