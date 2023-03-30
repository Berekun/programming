using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace BuscaMinasApp
{       
    public class CanvasTest : IGameDelegate
    {
            double x = 0.0f;
            double y = 0.0f;
            UDK.IImage? image1;
            UDK.IImage? image2;
            UDK.ISound? sound;
            UDK.IMusic? music;
            UDK.IFontFace? fontFace;
            UDK.IPlayer? player;
            string? posString;

            public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
            {
                canvas.Clear(new rgba_f64(0.2, 0.3, 0.3, 1.0));
                canvas.Camera.SetRect(rect2d_f64.FromMinMax(-2.0, -2.0, 10.0, 2.0), true);

            //canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 1.0, 1.0));
            //canvas.Transform.SetIdentity();
            //canvas.DrawRectangle(new rect2d_f64(-2.0, -2.0, 12.0, 4.0));

            //canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
            //canvas.Transform.SetTranslation(x, y + 1.0f);
            //canvas.Mask.PushRoundedRect(new rect2d_f64(0.0, 0.0, 1.0, 1.0), 0.2, 0.3, 0.4, 0.5);
            //canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1.0, 1.0));
            //canvas.Mask.Pop();

            //canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
            //canvas.Transform.SetTranslation(x + 1.0f, y + 1.0f);
            //canvas.Mask.PushRoundedRect(new rect2d_f64(0.0, 0.0, 1.0, 1.0), RectMode.Default, new ShapeMode() { LineWidth = 0.02 }, 0.2, 0.3, 0.4, 0.5);
            //canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1.0, 1.0));
            //canvas.Mask.Pop();

            //canvas.Transform.SetTranslation(x + 2.0f, y);
            //canvas.FillShader.SetRadialGradient(new rect2d_f64(0.0, 0.0, 1.0, 1.0), new RectMode(), new rgba_f64(1.0, 0.0, 0.0, 1.0), new rgba_f64(0.0, 1.0, 0.0, 1.0));
            //canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1.0, 1.0));

            //canvas.Transform.SetTranslation(x + 4.0f, y + 2.0f);
            //canvas.FillShader.SetRadialGradient(new rect2d_f64(0.0, 0.0, 1.0, 1.0), new RectMode(), new rgba_f64(1.0, 0.0, 0.0, 1.0), new rgba_f64(0.0, 1.0, 0.0, 1.0));
            //canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 1.0, 1.0));
            //canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1.0, 1.0));
            //canvas.Mask.Pop();

            canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 0.0, 1.0));
            canvas.Transform.SetIdentity();
            canvas.Transform.Rotate(1);
            canvas.Transform.Translate(x, y);
            canvas.Mask.PushCircle(new rect2d_f64(0.2, 0.2, 1.0, 1.0));
            canvas.DrawRectangle(new rect2d_f64(0.2, 0.2, 1.0, 1.0));
            canvas.Mask.Pop();


            if (posString != null)
                {
                    {
                        var line = canvas.AcquirePath();
                        line.MoveTo(new vec2d_f64(0.0, 0.0));
                        line.LineTo(new vec2d_f64(10.0, 0.0));

                        canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                        canvas.Transform.SetTranslation(x + 3.0, y);
                        canvas.StrokeOnePixelLine(line, true);
                    }
                    canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                    canvas.Transform.SetTranslation(x + 3.0, y);
                    canvas.DrawText(new vec2d_f64(0, 0), posString, fontFace, new TextMode() { height = 0.2, bottomCoords = false });
                }

                if (image1 != null)
                {
                    canvas.FillShader.SetImage(image1, new rect2d_f64(0.5, 0.5, 0.5, 0.5), new rgba_f64(1, 1, 1, 1));
                    canvas.Transform.SetTranslation(x, y - 1.0f);
                    canvas.Mask.PushCircle(new rect2d_f64(0.2, 0.2, 0.8, 0.8));
                    canvas.DrawRectangle(new rect2d_f64(0, 0, 1, 1));
                    canvas.Mask.Pop();
                }
                if (image2 != null)
                {
                    var dim = image2.View.Size2D;

                    canvas.FillShader.SetImage(image2, new rgba_f64(1, 1, 1, 1.0));
                    canvas.Transform.SetTranslation(x, y);
                    canvas.DrawRectangle(canvas.FitDimensionsToRect(dim, new rect2d_f64(0, 0, 1, 1)));
                }


                canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x + 1.0f, y - 2.0f);
                canvas.Mask.PushRoundedRect(new rect2d_f64(0.0, 0.0, 1.0, 1.0), 0.3, 0.3, 0.3, 0.3);
                canvas.DrawRectangle(new rect2d_f64(0, 0, 1, 1));
                canvas.Mask.Pop();

                canvas.FillShader.SetColor(new rgba_f64(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x + 3.0f, y - 3.0f);
                canvas.Mask.PushStar(new rect2d_f64(0.0, 0.0, 1.0, 1.0), 4, 0.5);
                canvas.DrawRectangle(new rect2d_f64(0, 0, 1, 1));
                canvas.Mask.Pop();

            }

            public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
            {
                var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
                posString = "Hg (" + mouse.X + "," + mouse.Y + ") - (" + pos.x + "," + pos.y + ")";

                if (keyboard.IsKeyPressed(Keys.S))
                    gameEvent.audioThread.AcquirePlayer()?.SetSound(sound, false, true).Play();

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
                if (keyboard.IsKeyPressed(Keys.P) && player != null)
                    player.SetPaused(!player.IsPaused);
            }

            public void OnLoad(GameDelegateEvent gameEvent)
            {
                image2 = gameEvent.canvasContext.LoadImageFromFile("resources/david.png");
                image1 = gameEvent.canvasContext.LoadImageFromFile("resources/dices.png");
                fontFace = gameEvent.canvasContext.CreateFont(CODEC.LoadFontFromFiles("resources/ArialCE.ttf"))?.CreateFontFace(64.0);

                sound = gameEvent.audioContext.LoadSoundFromFile("resources/mixkit.wav");
                music = gameEvent.audioContext.LoadMusicFromFile("resources/sample.ogg");

                player = gameEvent.audioThread.AcquirePlayer();
                player?.SetSound(music, true, false).Play();
            }

            public void OnUnload(GameDelegateEvent gameEvent)
            {
            }
    }
}


