using DAM;
using OpenTK.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillaPilla
{
    public enum CharacterType
    {
        Cat, Mouse
    }
    public class Character
    {
        public float x,y,width,heigth;
        public CharacterType Type;
        public float r, g, b, a;
        public DAM.Image Image;


        public void Render(ICanvas canvas)
        {
            canvas.FillRectangle(this.x,this.y, this.width, this.heigth,this.Image,0.0f,0.0f,1.0f,1.0f,1.0f,1.0f,1.0f,this.a);
        }
    }
}
