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

        public void Move(IKeyboard k,float maxX, float minX)
        {
            MovePosition(k);
            Limited(maxX, minX);
        }

        public void Move(float maxY)
        {
            if (this.type == GameObjectsType.BULLET)
            {
                this.y += 5f * Time.deltaTime;
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

        public void Limited(float maxX,float minX,float maxY,List<GameObject> list)
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

            if(this.type == GameObjectsType.BULLET)
            {
                if(this.y >= maxY)
                {
                    list.Remove(this); 
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
