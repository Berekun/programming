﻿using System;
using DAM;
using OpenTK.Graphics.ES11;

namespace ChessApp
{
    public class MyChessGame : IGameDelegate
    {
        IBuffer? image;
        Rect2D worldRect = Rect2D.FromMinMax(0.0,0.0,8.0,8.0);
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new RGBA(0.38, 0.28, 0.15, 1.0));
            canvas.Camera.SetRect(worldRect);

            DrawBorad(canvas);

            canvas.FillShader.SetImage(image);
            canvas.Transform.Translate(0, 0);
            canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
           image = IAtomicDecoder.LoadFromFile("resources\\torre.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }

        public void DrawBorad(ICanvas canvas)
        {
            bool aux = true;
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(aux)
                        canvas.FillShader.SetColor(new RGBA(0.2, 0.2, 0.2, 1));
                    else
                        canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                    canvas.Transform.SetIdentity();
                    canvas.FillRectangle(new Rect2D(j, i, 1.0, 1.0));
                    aux = !aux;
                }
                aux = !aux;
            }
        }

    }
}

