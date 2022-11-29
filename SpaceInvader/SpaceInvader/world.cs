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

        //Renderiza todos los objetos del Mundo

        public void Render(ICanvas canvas,World world,IWindow window,GameObject player,int startlifes)
        {
            canvas.FillRectangle(this.minX , this.minY, this.maxX*2, this.maxY*2, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            AnimateBullet(canvas, null, world, bullets, enemies, player, window, startlifes);
            AnimateSoldier(canvas, null, world, enemies,bullets,player,window,startlifes);
            RenderBullets(canvas, bullets,enemies,window,player,world,startlifes);
            RenderSoldier(canvas, enemies,bullets,window,player,world,startlifes);
        }

        //Renderiza balas

        public void RenderBullets(ICanvas canvas,List<GameObject> bullets,List<GameObject> soldiers,IWindow window,GameObject player,World world,int startlifes)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Render(canvas);
                bullets[i].GameObjectColisionAll(bullets,soldiers,player,window,world,startlifes);
            }
        }

        //Anima las balas

        public void AnimateBullet(ICanvas canvas,IKeyboard keyboard,World world, List<GameObject> bullets, List<GameObject> soldier, GameObject player, IWindow window, int startlifes)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move(keyboard, world, bullets,soldier,player,window,startlifes);
            }       
        }

        //Renderiza los soldiers

        public void RenderSoldier(ICanvas canvas,List<GameObject> soldier, List<GameObject> bullets, IWindow window,GameObject player,World world, int startlifes)
        {
            for(int i = 0; i< soldier.Count; i++)
            {
                soldier[i].Render(canvas);
                soldier[i].GameObjectColisionAll(bullets,soldier,player,window,world,startlifes);
            }
        }

        //Anima los soldiers

        public void AnimateSoldier(ICanvas canvas, IKeyboard keyboard, World world, List<GameObject> soldier,List<GameObject> bullets, GameObject player, IWindow window, int startlifes)
        {
            for (int i = 0; i < soldier.Count; i++)
            {
                soldier[i].Move(keyboard, world, bullets,soldier,player,window,startlifes);

            }
        }
    }
}
