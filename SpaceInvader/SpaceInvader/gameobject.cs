using System;
using DAM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    public enum GameObjects
    {
        MAIN,SOLDIER,CAVALIER,DAVROS,BULLET
    }
    internal class GameObject
    {
        public float x,y,width,height;
        public GameObjects type;
        public float r, g, b, a;
        public Image Image;
        public List<Image> list;

        public void Render(ICanvas canvas)
        {
            canvas.FillRectangle(this.x, this.y, this.width, this.height, this.Image, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        }







    }


}
