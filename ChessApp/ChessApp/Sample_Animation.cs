using System;
using DAM;

namespace TestDAMNuget
{

    public class Sample_Animation : IGameDelegate
    {
        double x = 0.0;
        double x2 = 0.0;
        double y = 0.0;
        int starSides = 3;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Rect2D worldRect = Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0);
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.Camera.SetRect(worldRect);

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(x + x2, y);
            canvas.Mask.Push(true).SetStar(new Rect2D(0.0, 0.0, 1.0, 1.0), starSides, 0.5);
            canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
            canvas.Mask.Pop();
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
            if (keyboard.IsKeyDown(Keys.Right))
                x += 0.01f;
            if (keyboard.IsKeyDown(Keys.Left))
                x -= 0.01f;
            if (keyboard.IsKeyDown(Keys.Up))
                y += 0.01f;
            if (keyboard.IsKeyDown(Keys.Down))
                y -= 0.01f;
            if (keyboard.IsKeyDown(Keys.F))
                gameEvent.window.ToggleFullscreen();

            if (keyboard.IsKeyPressed(Keys.M))
            {
                gameEvent.animationEngine.Add(5.0, (in AnimationEvent ae, ref AnimationAction action) =>
                {
                    x = Math.Cos(ae.t * 2.0 * Math.PI);
                    y = Math.Sin(ae.t * 2.0 * Math.PI);
                });
            }
            if (keyboard.IsKeyPressed(Keys.L))
            {
                gameEvent.animationEngine.Add(10.0, (in AnimationEvent ae, ref AnimationAction action) =>
                {
                    x2 = 0.25 * Math.Cos(ae.t * 2.0 * Math.PI * 20.0);
                });
            }
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}

