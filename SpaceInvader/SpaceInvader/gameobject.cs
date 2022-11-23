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

        public void MovePlayer(IKeyboard k,float maxX, float minX)
        {
            MovePosition(k);
            LimitedPlayer(maxX, minX);
        }

        public void MoveBullet(float maxY,List<GameObject> bullets)
        {
            if (this.type == GameObjectsType.BULLET)
            {
                this.y += 5f * Time.deltaTime;

                LimitedBullet(bullets, maxY);
            }
        }

        public void MoveSoldier(float minY,List<GameObject> soldiers)
        {
            if (this.type == GameObjectsType.SOLDIER)
            {
                this.y -= 2 * Time.deltaTime;

                LimitedSoldier(soldiers, minY);
            }
        }

        public void MovePosition(IKeyboard keyboard)
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
        public void GameObjectColision(GameObject gameObject1,GameObject gameObject2)
        {
            if(this.type == GameObjectsType.PLAYER)
            {

            }
            else if (this.type == GameObjectsType.SOLDIER)
            {
                if(gameObject2.type == GameObjectsType.PLAYER)
                {

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

        public static void GetEnemies(List<GameObject> enemies)
        {
            float round = 0;
            float startx = -6.5f;
            if(round == 0)
            {
                for(int i = 0; i < 7; i++,startx += 2)
                {
                    GameObject soldier = new GameObject();
                    soldier.x = startx;
                    soldier.y = 9.0f;
                    soldier.width = 1f;
                    soldier.height = 1f;
                    soldier.type = GameObjectsType.SOLDIER;
                    soldier.r = 0f;
                    soldier.g = 0f;
                    soldier.b = 0f;
                    soldier.a = 1f;
                    enemies.Add(soldier);
                }
            }
        }






    }


}
