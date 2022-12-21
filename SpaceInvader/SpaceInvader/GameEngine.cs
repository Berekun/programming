using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    internal class GameEngine
    {
        //Crea el primer bloque de enemigos
        public static void CreateFirstRound(List<GameObject> enemies,IAssetManager manager,World world)
        {
            float round = 0;
            float startx = -6.5f;
            if (round == 0)
            {
                for (int i = 0; i < 7; i++, startx += 2)
                {
                    GameObject soldier = new GameObject();
                    soldier.x = startx;
                    soldier.y = 9.0f;
                    soldier.image = world.spritesEnemies[Getrandom(0, 3)];
                    soldier.height = 1.5f;
                    soldier.width = 1.5f;
                    soldier.type = GameObjectType.SOLDIER;
                    soldier.status = EnemyStatus.ALIVE;
                    
                    soldier.r = 1f;
                    soldier.g = 1f;
                    soldier.b = 1f;
                    soldier.a = 0.9f;

                    enemies.Add(soldier);
                }
            }
        }

        //Si te quitan una vida, es lo que resetea el mundo

        public static void ResetWorld(GameObject player,List<GameObject> enemies,List<GameObject> bullets,float startlifes,IWindow window)
        {
            if (player.lifes == 0)
            {
                window.Close();
            }

            if (startlifes > player.lifes)
            {
                player.x = 0.0f;
                bullets.Clear();
                startlifes--;

                foreach(GameObject soldier in enemies)
                {
                    // La x da igual?
                    soldier.y = 9f;
                }
            }
        }

        //Es una funcion Random

        private static Random random = new Random();

        public static int Getrandom(int min, int max)
        {
            double r = random.NextDouble();
            double dis = max - min;
            double result = r * dis + min;
            return (int)result;
        }
    }
}
