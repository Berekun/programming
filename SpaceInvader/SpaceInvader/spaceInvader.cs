﻿using DAM;
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
        float startlifes = 3;
        public void OnDraw(IAssetManager manager, IWindow window, ICanvas canvas)
        {
            Time.UpdateDeltaTime();
            canvas.Clear(0.0f, 0.0f, 0.0f, 0.0f);
            canvas.SetCamera(world.minX, world.minY,world.maxX,world.maxY, true);

            player.Shoot(canvas, world.bullets);
            world.Render(canvas, world,window,player);
            player.Render(canvas);
            GameEngine.ResetWorld(player, world.enemies, world.bullets, startlifes,window);
            startlifes = player.lives;
        }

        public void OnKeyboard(IAssetManager manager, IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            player.Move(keyboard,world,world.bullets,world.enemies);
        }

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            //Caracteristicas del mundo
            world = new World();
            world.Image = manager.LoadImage("resources\\fondo.jpg");
            world.maxY = 10.0f;
            world.maxX = 7.5f;
            world.minY = -world.maxY;
            world.minX = -world.maxX;
            world.a = 1.0f;

            //Caracteristicas de la nave principal
            player = new GameObject();
            player.Image = manager.LoadImage("resources\\player.png");
            float arplayer = player.Image.Width / player.Image.Height;
            player.width = 2;
            player.height = player.width/arplayer;
            player.x = 0.0f;
            player.y = world.minY + 2;
            player.type = GameObjectType.PLAYER;

            //crear soldiers
            GameEngine.CreateEnemies(world.enemies);
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
