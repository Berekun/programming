using DAM;
using System;

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
            world.AnimateExplosions();
            world.RemoveDeads();

            if(world.enemies.Count == 0) 
            {
                world.bullets.Clear();
                GameEngine.CreateFirstRound(world.enemies, manager);
            }

            if(player.kills >= 28)
            {
                window.Close();
            }
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
            player.image = manager.LoadImage("resources\\player.png");
            float arplayer = player.image.Width / player.image.Height;
            player.width = 4;
            player.height = player.width/arplayer;
            player.x = 0.0f;
            player.y = world.minY + 2;
            player.type = GameObjectType.PLAYER;

            //crear soldiers
            GameEngine.CreateFirstRound(world.enemies,manager);
            world.FillExplosionsSprites(manager);
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
