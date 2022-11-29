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
    public enum GameObjectType
    {
        PLAYER,SOLDIER,CAVALIER,DAVROS,BULLET
    }
    internal class GameObject
    {
        public float x,y,width,height;
        public GameObjectType type;
        public float r, g, b, a;
        public Image Image;
        public List<Image> list = new List<Image>();
        float shotTime = 0;
        public int lifes = 3;

        //Renderiza los GameObjects

        public void Render(ICanvas canvas)
        {
            if (this.Image != null)
                canvas.FillRectangle(this.x - this.width / 2, this.y - this.height / 2, this.width, this.height, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.9f);
            else
                canvas.FillRectangle(this.x - this.width / 2, this.y - this.height / 2, this.width, this.height, this.r, this.g, this.b, this.a);

        }

        //Funcion de movimiento en general

        public void Move(IKeyboard k,World world,List<GameObject> bullets,List<GameObject> soldiers)
        {
            if(this.type == GameObjectType.PLAYER)
            {
                MovePosition(k);
                FixPositionPlayer(world.maxX, world.minX);
            }
            else if(this.type == GameObjectType.BULLET)
            {
                MoveBullet(world.maxY, bullets);
            }
            else if(this.type == GameObjectType.SOLDIER)
            {
                MoveSoldier(world.minY, soldiers);
            }
            
        }

        //Funcion de movimiento de bala

        private void MoveBullet(float maxY,List<GameObject> bullets)
        {
            if (this.type == GameObjectType.BULLET)
            {
                this.y += 5f * Time.deltaTime;

                RemoveBullet(bullets, maxY);
            }
        }

        //Funcion de movimiento de soldier

        private void MoveSoldier(float minY,List<GameObject> soldiers)
        {
            if (this.type == GameObjectType.SOLDIER)
            {
                this.y -= 2 * Time.deltaTime;

                RemoveSoldier(soldiers, minY);
            }
        }

        //Funcion de movimiento de la nave

        private void MovePosition(IKeyboard keyboard)
        {
            if(this.type == GameObjectType.PLAYER)
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

        //Funcion de limite de jugador

        public void FixPositionPlayer(float maxX,float minX)
        {
            if(this.type == GameObjectType.PLAYER)
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

        //Funcion que elimina balas

        public void RemoveBullet(List<GameObject> bullets, float maxY)
        {
            if(this.y >= maxY)
            {
                bullets.Remove(this);
            }
        }

        //Funcion que elimina soldiers

        public void RemoveSoldier(List<GameObject> soldier, float minY)
        {
            if(this.y <= minY)
            {
                soldier.Remove(this);
            }
        }

        //Funcion que comprueba si dos GameObject estan colisionando

        public bool GameObjectColision(GameObject gameObject2)
        {
            if(this.type == GameObjectType.SOLDIER)
            {
                if (gameObject2.type == GameObjectType.BULLET)
                {
                    return colliders.IsColision(this.x, this.y, this.width, this.height, gameObject2.x, gameObject2.y, gameObject2.width, gameObject2.height);
                }
            }
            else if (this.type == GameObjectType.PLAYER)
            {
                if(gameObject2.type == GameObjectType.SOLDIER)
                {
                    return colliders.IsColision(this.x, this.y, this.width, this.height, gameObject2.x, gameObject2.y, gameObject2.width, gameObject2.height);
                }
            }

            return false;
        }

        //Funcion que comprueba objetos de listas con otros objetos

        public void GameObjectColisionAll(List<GameObject> bullets,List<GameObject> soldiers,GameObject player,IWindow window,World world,int startlifes)
        {

            for (int i = 0; i < soldiers.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (soldiers[i].GameObjectColision(bullets[j]) == true)
                    {
                        soldiers.Remove(soldiers[i]);
                        bullets.Remove(bullets[j]);
                        j--;
                        i--;
                    }

                    if (soldiers.Count == 0)
                        break;
                }
            }

            for (int i = 0; i < soldiers.Count; i++)
            {
                if (player.GameObjectColision(soldiers[i]) == true)
                {
                    player.lifes--;
                    GameEngine.ResetWorld(player, world.enemies, world.bullets, startlifes, window);
                }
            }
        }

        //Funcion de disparo del player

        public void Shoot(ICanvas canvas,List<GameObject> bullets)
        {
            this.shotTime += Time.deltaTime;
            if(this.shotTime >= 0.5f)
            {
                GameObject bullet = new GameObject();
                bullet.x = this.x;
                bullet.y = this.y;
                bullet.width = 0.4f;
                bullet.height = 0.7f;
                bullet.type = GameObjectType.BULLET;
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
