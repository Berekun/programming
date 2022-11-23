using System;
using DAM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace SpaceInvader
{
    public enum GameObjectsType
    {
        PLAYER,SOLDIER,CAVALIER,DAVROS,BULLET
    }
    internal class GameObject
    {
        public float x,y,width,height;
        public GameObjectsType type;
        public float r, g, b, a;
        public Image Image;
        public List<Image> list;
        float shotTime = 0;

        public void Render(ICanvas canvas)
        {
            if (this.Image != null)
                canvas.FillRectangle(this.x - this.width / 2, this.y - this.height / 2, this.width, this.height, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            else
                canvas.FillRectangle(this.x, this.y, this.width, this.height, this.r, this.g, this.b, this.a);

        }

        public void Move(IKeyboard k,World world,List<GameObject> bullets,List<GameObject> soldiers)
        {
            if(this.type == GameObjectsType.PLAYER)
            {
                MovePosition(k);
                LimitedPlayer(world.maxX, world.minX);
            }
            else if(this.type == GameObjectsType.BULLET)
            {
                MoveBullet(world.maxY, bullets);
            }
            else if(this.type == GameObjectsType.SOLDIER)
            {
                MoveSoldier(world.minY, soldiers);
            }
            
        }

        private void MoveBullet(float maxY,List<GameObject> bullets)
        {
            if (this.type == GameObjectsType.BULLET)
            {
                this.y += 5f * Time.deltaTime;

                LimitedBullet(bullets, maxY);
            }
        }

        private void MoveSoldier(float minY,List<GameObject> soldiers)
        {
            if (this.type == GameObjectsType.SOLDIER)
            {
                this.y -= 2 * Time.deltaTime;

                LimitedSoldier(soldiers, minY);
            }
        }

        private void MovePosition(IKeyboard keyboard)
        {
            if(this.type == GameObjectsType.PLAYER)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    this.x += 0.1f;
                }

                if (keyboard.IsKeyDown(Keys.Left))
                {
                    this.x -= 0.1f;
                }
            }
        }

        public void LimitedPlayer(float maxX,float minX)
        {
            if(this.type == GameObjectsType.PLAYER)
            {
                float maxpos= maxX - this.width/2;

                if(this.x > maxpos)
                {
                    this.x = maxpos;
                }

                if (this.x < -maxpos)
                {
                    this.x = -maxpos;
                }
            }
        }

        public void LimitedBullet(List<GameObject> bullets, float maxY)
        {
            if(this.y >= maxY)
            {
                bullets.Remove(this);
            }
        }

        public void LimitedSoldier(List<GameObject> soldier, float minY)
        {
            if(this.y <= minY)
            {
                soldier.Remove(this);
            }
        }

        /*public void GameObjectColision(List<GameObject> bullets,List<GameObject> soldiers
        {

        }
        */
        public bool GameObjectColision(GameObject gameObject2)
        {
            if(this.type == GameObjectsType.SOLDIER)
            {
                if (gameObject2.type == GameObjectsType.BULLET)
                {
                    return colliders.IsColision(this.x, this.y, this.width, this.height, gameObject2.x, gameObject2.y, gameObject2.width, gameObject2.height);
                }
            }
            else if (this.type == GameObjectsType.PLAYER)
            {
                if(gameObject2.type == GameObjectsType.SOLDIER)
                {
                    return colliders.IsColision(this.x, this.y, this.width, this.height, gameObject2.x, gameObject2.y, gameObject2.width, gameObject2.height);
                }
            }

            return false;
        }

        public void GameObjectColisionAll(List<GameObject> bullets,List<GameObject> soldiers,GameObject player,IWindow window)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                   if(bullets[j].GameObjectColision(soldiers[i]) == true)
                   {
                        soldiers.Remove(soldiers[i]);
                   }

                }

                if (soldiers[i].GameObjectColision(player) == true)
                {
                    window.Close();
                } 
            }
        }

        public void Shoot(ICanvas canvas,List<GameObject> bullets)
        {
            this.shotTime += Time.deltaTime;
            if(this.shotTime >= 1f)
            {
                GameObject bullet = new GameObject();
                bullet.x = this.x;
                bullet.y = this.y;
                bullet.width = 0.4f;
                bullet.height = 0.7f;
                bullet.type = GameObjectsType.BULLET;
                bullet.r = 1.0f;
                bullet.g = 1.0f;
                bullet.b = 1.0f;
                bullet.a = 1.0f;
                bullets.Add(bullet);
                this.shotTime = 0;
            }
        }
    }
}
