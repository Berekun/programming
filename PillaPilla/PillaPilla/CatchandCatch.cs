using DAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PillaPilla
{
    public class CatchandCatch : DAM.IGameDelegate
    {
        //float blue = 0.0f;
        //float horizontal = -0.02f;
        //float vertical = 0.0f;
        private double time = 0.0;
        world world;
        Character cat;
        Character mouse;

        public void OnDraw(IAssetManager manager, IWindow window,ICanvas canvas)
        {
            this.time += 1.0f / 60.0f;
            double sin = Math.Sin(this.time);
            double correction = (sin + 1) / 2;
            cat.a = (float)correction;

            canvas.Clear(1.0f,1.0f,1.0f,1.0f);
            canvas.SetCamera(world.x,world.y,world.x + world.width,world.y + world.heigth, true);

            world.Render(canvas);
            cat.Render(canvas);
            mouse.Render(canvas);

            if (colisions.IsColision(cat.x, cat.y, cat.width, cat.heigth, mouse.x, mouse.y, mouse.width, mouse.heigth) == true)
                window.Close();
        }

        public void OnKeyboard(IAssetManager manager,IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Right))
                this.mouse.x += 0.1f;
            if (keyboard.IsKeyDown(Keys.Left))
                this.mouse.x -= 0.1f;
            if (keyboard.IsKeyDown(Keys.Up))
                this.mouse.y += 0.1f;
            if (keyboard.IsKeyDown(Keys.Down))
                this.mouse.y -= 0.1f;

            if (keyboard.IsKeyDown(Keys.D))
                this.cat.x += 0.05f;
            if (keyboard.IsKeyDown(Keys.A))
                this.cat.x -= 0.05f;
            if (keyboard.IsKeyDown(Keys.W))
                this.cat.y += 0.05f;
            if (keyboard.IsKeyDown(Keys.S))
                this.cat.y -= 0.05f;



            //Cierra el programa
            if (keyboard.IsKeyDown(Keys.Escape))
                window.Close();
        }   

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            //Pone el juego en Pantalla completa
            //window.ToggleFullscreen();

            //Caracteristicas del mundo
            world = new world();
            world.Image = manager.LoadImage("resources\\fondo.jpg");
            float arimg = world.Image.Width / world.Image.Height;
            world.width = 20.0f;
            world.heigth = world.width/arimg;
            world.x = -10.0f;
            world.y = -7.5f;
            world.a = 1.0f;

            //caracteristicas gato
            float arcat;
            cat = new Character();
            cat.Image = manager.LoadImage("resources\\gato.png");
            cat.x = 7.0f;
            cat.y = 0.0f;
            cat.width = 4.0f;
            arcat = (float)cat.Image.Width / cat.Image.Height;
            cat.heigth = cat.width/arcat;
            cat.Type = CharacterType.Cat;
            cat.r = 0.3f;
            cat.b = 0.45f;
            cat.g = 0.7f;
            cat.a = 1.0f;

            //caracteristicas raton
            float armouse;
            mouse = new Character();
            mouse.Image = manager.LoadImage("resources\\raton.png");
            mouse.x = -7.0f;
            mouse.y = 0.0f;
            mouse.width = 3.0f;
            armouse = (float)(mouse.Image.Width / mouse.Image.Height);
            mouse.heigth = mouse.width/armouse;
            mouse.Type = CharacterType.Cat;
            mouse.r = 0.3f;
            mouse.b = 0.55f;
            mouse.g = 0.2f;
            mouse.a = 0.99f;
            
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
