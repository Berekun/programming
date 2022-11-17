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
            canvas.Clear(1.0f, 1.0f, 1.0f, 1.0f);
            canvas.SetCamera(world.x, world.y, world.x + world.width, world.y + world.height, true);

            world.Render(canvas);
            
        }

        public void OnKeyboard(IAssetManager manager, IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            
        }

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            //Caracteristicas del mundo
            world = new world();
            world.Image = manager.LoadImage("resources\\fondo.jpg");
            world.height = 20.0f;
            world.width = 15.0f;
            world.y = -10.0f;
            world.x = -7.5f;
            world.a = 1.0f;

            //Caracteristicas de la nave principal
            player = new GameObject();
            player.x = 0.0f;
            player.y = -8.0f;
            player.Image = manager.LoadImage("resources\\main.jpg");
            float arplayer = player.Image.Width / player.Image.Height;
            player.width = 2;
            player.height = player.width/arplayer;
            player.type = GameObjects.MAIN;
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
