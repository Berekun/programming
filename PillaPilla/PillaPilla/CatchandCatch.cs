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
        float room_width = 20.0f;
        float room_height = 15.0f;
        double time = 0.0;
        Character cat;
        Character mouse;

        public void OnDraw(IAssetManager manager, IWindow window,ICanvas canvas)
        {
            this.time += 1.0f / 60.0f;
            double sin = Math.Sin(this.time);
            double correction = (sin + 1) / 2;
            cat.a = (float)correction;

            canvas.Clear(1.0f,1.0f,1.0f,1.0f);
            canvas.SetCamera(-10.0f, -7.5f, 10.0f, 7.5f, true);

            cat.Render(canvas);
            mouse.Render(canvas);
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

            if (this.mouse.x + (this.mouse.width /2) > room_width/2)
                this.mouse.x = (room_width / 2) - (this.mouse.width/2);

            if (this.cat.x + (this.cat.width / 2) > room_width / 2)
                this.cat.x = room_width / 2 - (this.cat.width / 2);

            if (this.mouse.x - (this.mouse.width / 2) < room_width / -2)
                this.mouse.x = room_width / -2 + (this.mouse.width / 2);

            if (this.cat.x - (this.cat.width / 2) < room_width / -2)
                this.cat.x = room_width / -2 + (this.cat.width / 2);

            if (this.mouse.y + (this.mouse.heigth / 2) > room_height / 2)
                this.mouse.y = room_height / 2 - (this.mouse.heigth / 2);

            if (this.cat.y + (this.cat.heigth / 2) > room_height / 2)
                this.cat.y = room_height / 2 - (this.cat.heigth / 2);

            if (this.mouse.y - (this.mouse.heigth / 2) < room_height / -2)
                this.mouse.y = room_height / -2 + (this.mouse.heigth / 2);

            if (this.cat.y - (this.cat.heigth / 2) < room_height / -2)
                this.cat.y = room_height / -2 + (this.cat.heigth / 2);
        }   

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            cat = new Character();
            cat.x = 0.0f;
            cat.y = 0.0f;
            cat.width = 4.0f;
            cat.heigth = 4.0f;
            cat.Type = CharacterType.Cat;
            cat.r = 0.3f;
            cat.b = 0.45f;
            cat.g = 0.7f;
            cat.a = 1.0f;
            cat.Image = manager.LoadImage("C:\\Users\\danberinf\\Desktop\\Images\\gato.png");

            mouse = new Character();
            mouse.x = 0.0f;
            mouse.y = 0.0f;
            mouse.width = 3.0f;
            mouse.heigth = 3.0f;
            mouse.Type = CharacterType.Cat;
            mouse.r = 0.3f;
            mouse.b = 0.55f;
            mouse.g = 0.2f;
            mouse.a = 0.99f;
            mouse.Image = manager.LoadImage("C:\\Users\\danberinf\\Desktop\\Images\\raton.png");
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }

        //public float dist1(float x1,float x2,float width1,float width2)
        //{
        //    float dist = (x1 + (width1 / 2)) - (x2 - (width2 / 2));
        //    return dist;
        //}

        //public float dist2(float x1, float x2, float width1, float width2)
        //{
        //    float dist = (x1 - (width1 / 2)) - (x2 + (width2 / 2));
        //    return dist;
        //}
        //public float dist3(float y1, float y2, float heigth1, float heigth2)
        //{
        //    float dist = (y1 - (heigth1 / 2)) - (y2 + (heigth2 / 2));
        //   return dist;
        //}
        //public float dist4(float y1, float y2, float heigth1, float heigth2)
        //{
        //    float dist = (y1 - (heigth1 / 2)) - (y2 + (heigth2 / 2));
        //    return dist;
        //}
    }
}
