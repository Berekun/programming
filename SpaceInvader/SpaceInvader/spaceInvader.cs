using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    internal class SpaceInvader : DAM.IGameDelegate
    {
        World world;
        GameObject player;
        int startlifes = 3;
        public void OnDraw(IAssetManager manager, IWindow window, ICanvas canvas)
        {
            Time.UpdateDeltaTime();
            canvas.Clear(0.0f, 0.0f, 0.0f, 0.0f);
            canvas.SetCamera(world.minX, world.minY,world.maxX,world.maxY, true);

            player.Shoot(canvas, world.bullets, manager);
            world.Render(canvas, world,window,player,startlifes);
            player.Render(canvas);
            startlifes = player.lifes;
        }

        public void OnKeyboard(IAssetManager manager, IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            player.Move(keyboard,world,world.bullets,world.enemies,player,window,startlifes);

            if (keyboard.IsKeyDown(Keys.Escape))
                window.Close();
        }

        public void OnLoad(IAssetManager manager, IWindow window)
        {

            window.ToggleFullscreen();

            //Caracteristicas del mundo
            world = new World();
            world.Image = manager.LoadImage("resources\\fondo.png");
            world.maxY = 10.0f;
            world.maxX = 7.5f;
            world.minY = -world.maxY;
            world.minX = -world.maxX;
            world.a = 1.0f;

            //Caracteristicas de la nave principal
            player = new GameObject();
            player.Image = manager.LoadImage("resources\\player.png");
            float arplayer = player.Image.Width / player.Image.Height;
            player.width = 3;
            player.height = player.width/arplayer;
            player.x = 0.0f;
            player.y = world.minY + 2;
            player.type = GameObjectType.PLAYER;

            //crear soldiers
            GameEngine.CreateFirstRound(world.enemies,manager);
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
