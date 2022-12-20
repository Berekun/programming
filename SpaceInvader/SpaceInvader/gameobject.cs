using System;
using DAM;

namespace SpaceInvader
{
    public enum GameObjectType
    {
        PLAYER, SOLDIER, CAVALIER, DAVROS, BULLET
    }

    public enum EnemyStatus
    {
        ALIVE, DEAD, EXPLODED
    }
    internal class GameObject
    {
        public float x, y, width, height;
        public GameObjectType type;
        public EnemyStatus status;
        public float r, g, b, a;
        public Image image;
        public List<Image> list = new List<Image>();
        float shotTime = 0;
        public int kills = 0;
        public float explosionTime = 0;
        public int explosioncount = 0;
        public int lifes = 3;

        //Renderiza los GameObjects

        public void Render(ICanvas canvas)
        {
            if (this.image != null)
                canvas.FillRectangle(this.x - this.width / 2, this.y - this.height / 2, this.width, this.height, this.image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.9f);
            else
                canvas.FillRectangle(this.x - this.width / 2, this.y - this.height / 2, this.width, this.height, this.r, this.g, this.b, this.a);

        }

        //Funcion de movimiento en general

        public void Move(IKeyboard k,World world,List<GameObject> bullets,List<GameObject> soldiers, GameObject player, IWindow window, int startlifes)
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
                MoveSoldier(world.minY, soldiers,player,window,world,startlifes);
            }
            
        }

        //Funcion de movimiento de bala

        private void MoveBullet(float maxY,List<GameObject> bullets)
        {
            // Este if es innecesario
            if (this.type == GameObjectType.BULLET)
            {
                this.y += 5f * Time.deltaTime;

                RemoveBullet(bullets, maxY);
            }
        }

        //Funcion de movimiento de soldier

        private void MoveSoldier(float minY,List<GameObject> soldiers, GameObject player, IWindow window, World world, int startlifes)
        {
            // If innecesario
            if (this.type == GameObjectType.SOLDIER)
            {
                this.y -= 2 * Time.deltaTime;

                RemoveSoldier(soldiers, minY,player,window,world,startlifes);
            }
        }

        //Funcion de movimiento de la nave

        private void MovePosition(IKeyboard keyboard)
        {
            // If innecesario
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

        public void RemoveSoldier(List<GameObject> soldier, float minY, GameObject player, IWindow window, World world, int startlifes)
        {
            if(this.y <= minY)
            {
                // Esto es un poco peligroso, no lo hagas, usa RemoveAt
                soldier.Remove(this);
                GameEngine.ResetWorld(player, world.enemies, world.bullets, startlifes, window);
            }
        }

        //Funcion que comprueba si dos GameObject estan colisionando
        // Verbo
        public bool GameObjectColision(GameObject gameObject2)
        {
            // Duplicado de código, coméntame esto en clase
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

                    if (soldiers[i].GameObjectColision(bullets[j]) == true && soldiers[i].status == EnemyStatus.ALIVE)
                    {
                        soldiers[i].status = EnemyStatus.EXPLODED;
                        bullets.Remove(bullets[j]);
                        // No es necesario decrementar los dos, coméntame esto en clase
                        j--;
                        i--;
                        player.kills++;
                    }

                    if (soldiers.Count == 0)
                        break;
                }
            }

            for (int i = 0; i < soldiers.Count; i++)
            {
                // == true?!!?!?!?!? comentame esto en clase
                if (player.GameObjectColision(soldiers[i]) == true && soldiers[i].status == EnemyStatus.ALIVE)
                {
                    player.lifes--;
                    GameEngine.ResetWorld(player, world.enemies, world.bullets, startlifes, window);
                }
            }
        }

        //Funcion de disparo del player

        public void Shoot(ICanvas canvas,List<GameObject> bullets,IAssetManager manager)
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
                bullet.image = manager.LoadImage("resources\\bala.png");
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
