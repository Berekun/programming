using System;
using DAM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace SpaceInvader
{
    public enum GameObjectsType
    {
        PLAYER,SOLDIER,CAVALIER,DAVROS,BULLET
    }
    internal class GameObject
    {
        public float x,y,width,height;
        public GameObjectsType type;
        public float r, g, b, a;
        public Image Image;
        public List<Image> list;

        public void Render(ICanvas canvas)
        {
            canvas.FillRectangle(this.x - this.width/2, this.y - this.height/2, this.width, this.height, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        }

        public void Move(IKeyboard keyboard)
        {
            if(this.type == GameObjectsType.PLAYER)
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

        public void Limited(ICanvas canvas,float x,float y)
        {
            if(this.type == GameObjectsType.PLAYER)
            {
                if()
            }
        }







    }


}
