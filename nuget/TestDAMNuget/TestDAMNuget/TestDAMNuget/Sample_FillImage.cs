using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_FillImage : IGameDelegate
    {
        DAM.IBuffer? image;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(worldRect);

            if (image != null)
            {
                canvas.FillShader.SetImage(image, new RGBA(1, 1, 1, 1));
                canvas.Transform.SetTranslation(0.0, -1.0);
                canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
            }
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            image = IAtomicDecoder.LoadFromFile("resources/dices.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}


