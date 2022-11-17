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
        public void OnDraw(IAssetManager manager, IWindow window, ICanvas canvas)
        {
            canvas.Clear(1.0f, 1.0f, 1.0f, 1.0f);
            canvas.SetCamera(world.x, world.y, world.x + world.width, world.y + world.height, true);
            
        }

        public void OnKeyboard(IAssetManager manager, IWindow window, IKeyboard keyboard, IMouse mouse)
        {
            
        }

        public void OnLoad(IAssetManager manager, IWindow window)
        {
            //Caracteristicas del mundo
            world = new world();
            world.Image = manager.LoadImage("resource\\fondo.png");
            float arimage = world.Image.Width / world.Image.Height;
            world.height = 20.0f;
            world.width = world.height / arimage;
            world.y = -10.0f;
            world.x = -7.5f;
            world.a = 1.0f;
        }

        public void OnUnload(IAssetManager manager, IWindow window)
        {
            
        }
    }
}
