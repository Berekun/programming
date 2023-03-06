using System;
using DAM;

namespace SpaceInvader
{
    internal class World
    {
        public float minX, minY, maxX, maxY;
        public Image Image;
        public List<Image> spritesEnemies = new List<Image>();
        public List<Image> explosions = new List<Image>();
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
                // No, cuando se renderiza no se pueden calcular otro tipo de cosas
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

        public void FillExplosionsSprites(IAssetManager manager)
        {
            for(int i = 1; i <= 11;i++)
            {
                explosions.Add(manager.LoadImage("resources\\explosion" + i + ".png"));
            }
        }

        public void AnimateExplosions()
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].status == EnemyStatus.EXPLODED)
                {
                    enemies[i].explosionTime += Time.deltaTime;

                    if(enemies[i].explosionTime > 0.05f)
                    {
                        enemies[i].image = explosions[enemies[i].explosioncount];
                        enemies[i].explosioncount++;
                        enemies[i].explosionTime = 0;
                    }

                    if (enemies[i].explosioncount > 10)
                    {
                        enemies[i].status = EnemyStatus.DEAD;
                    }
                }
            }
        }

        public void RemoveDeads()
        {
            for(int i = 0; i < enemies.Count; i++) 
            {
                if (enemies[i].status == EnemyStatus.DEAD)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadImagesSoldier(List<Image> image,IAssetManager manager)
        {
            image.Add(manager.LoadImage("resources\\enemigo1.png"));
            image.Add(manager.LoadImage("resources\\enemigo2.png"));
            image.Add(manager.LoadImage("resources\\enemigo3.png"));
        }

        
    }
}
