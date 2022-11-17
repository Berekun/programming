using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    internal class SpaceInvader : DAM.IGameDelegate
    {
        world world;
        GameObject player;
        public void OnDraw(IAssetManager manager, IWindow window, ICanvas canvas)
        {
            canvas.Clear(0.0f, 0.0f, 0.0f, 0.0f);
            canvas.SetCamera(world.x, world.y, world.x + world.width, world.y + world.height, true);

            world.Render(canvas);
            player.Render(canvas);
            player.Limited(canvas,world.x,world.y);
            
        }

        public void OnKeyboard(IAssetManager manager, IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            player.Move(keyboard);
        }

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            //Caracteristicas del mundo
            world = new world();
            world.Image = manager.LoadImage("resources\\fondo.jpg");
            world.height = 10.0f;
            world.width = 7.5f;
            world.y = 0f;
            world.x = 0f;
            world.a = 1.0f;

            //Caracteristicas de la nave principal
            player = new GameObject();
            player.Image = manager.LoadImage("resources\\player.png");
            float arplayer = player.Image.Width / player.Image.Height;
            player.width = 1;
            player.height = player.width/arplayer;
            player.x = 0.0f;
            player.y = 0.0f + player.height/2;
            player.type = GameObjectsType.PLAYER;
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
