using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    internal class GameEngine
    {
        public static void CreateEnemies(List<GameObject> enemies)
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
                    soldier.width = 1f;
                    soldier.height = 1f;
                    soldier.type = GameObjectType.SOLDIER;
                    soldier.r = 0f;
                    soldier.g = 0f;
                    soldier.b = 0f;
                    soldier.a = 1f;
                    enemies.Add(soldier);
                }
            }
        }

        public static void ResetWorld(GameObject player,List<GameObject> enemies,List<GameObject> bullets,float startlifes,IWindow window)
        {
            if (startlifes == 0)
            {
                Console.WriteLine(player.lives);
                window.Close();
            }

            if (startlifes > player.lives)
            {
                player.x = 0.0f;
                bullets.Clear();

                foreach(GameObject soldier in enemies)
                {
                    soldier.y = 9f;
                }
            }
        }
    }
}
