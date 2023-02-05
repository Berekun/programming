
using DAM;


namespace TestImage
{

    public class MyGame : IGameDelegate
    {
        float x = 0.0f;
        float y = 0.0f;
        double time = 0.0;
        DAM.IBuffer? image;
        DAM.Sound? sound;
        DAM.Sound? music;
        DAM.IFont? font;
        DAM.IPlayer? player;
        string? posString;
        int starSides = 3;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            canvas.Clear(new RGBA(0.2, 0.3, 0.3, 1.0));
            canvas.SetCamera(Rect2D.FromMinMax(-2.0, -2.0, 10.0, 2.0), true);

            canvas.FillShader.SetColor(new RGBA(0.0, 0.0, 1.0, 1.0));
            canvas.Transform.SetIdentity();
            canvas.FillRectangle(new Rect2D(-2.0, -2.0, 12.0, 4.0));

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(x, y + 1.0f);
            canvas.Mask.Push(true).SetRoundedRect(new Rect2D(0.0, 0.0, 1.0, 1.0), 0.2f, 0.3f, 0.4f, 0.5f);
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(x + 1.0f, y + 1.0f);
            canvas.Mask.Push(true).SetLineWidth(0.02).SetRoundedRect(new Rect2D(0.0, 0.0, 1.0, 1.0), 0.2f, 0.3f, 0.4f, 0.5f);
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();

            {
                canvas.FillShader.SetRadialGradient(new Rect2D(0.0, 0.0, 1.0, 1.0), new RGBA(1.0, 0.0, 0.0, 1.0), new RGBA(0.0, 1.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(x + 2.0f, y);
                canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            }
            {
                canvas.FillShader.SetRadialGradient(new Rect2D(0.0, 0.0, 1.0, 1.0), new RGBA(1.0, 0.0, 0.0, 1.0), new RGBA(0.0, 1.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(x + 2.0f, y - 1);
                canvas.Mask.Push(true).SetRectangle(new Rect2D(0.5 + Math.Cos(time) * 0.2, 0.25, 1.0, 0.5));
                canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
                canvas.Mask.Pop();
            }

            canvas.FillShader.SetRadialGradient(new Rect2D(0.0, 0.0, 1.0, 1.0), new RGBA(1.0, 0.0, 0.0, 1.0), new RGBA(0.0, 1.0, 0.0, 1.0));
            canvas.Transform.SetTranslation(x + 4.0f, y + 2.0f);
            canvas.Mask.Push(true).SetCircle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.FillRectangle(new Rect2D(0.0, 0.0, 1.0, 1.0));
            canvas.Mask.Pop();

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 0.0, 1.0));
            canvas.Transform.SetIdentity();
            canvas.Transform.Rotate(1);
            canvas.Transform.Translate(x, y);
            canvas.Mask.Push(true).SetCircle(new Rect2D(0.2, 0.2, 1.0, 1.0));
            canvas.FillRectangle(new Rect2D(0.2, 0.2, 1.0, 1.0));
            canvas.Mask.Pop();

            if (posString != null)
            {
                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x + 3.0f, y);
                canvas.FillText(new Point2D(0, 0), 0.2f, font, posString);
            }

            if (image != null)
            {
                var dim = image.Dimensions;

                canvas.FillShader.SetImage(image, new RGBA(1, 1, 1, 0.5));
                canvas.Transform.SetTranslation(x, y);
                canvas.FillRectangle(new Rect2D(0, 0, dim.Width, dim.Height).FitInto(new Rect2D(0, 0, 1, 1)));

                canvas.FillShader.SetImage(image, new Rect2D(0.5, 0.5, 0.5, 0.5), new RGBA(1, 1, 1, 1));
                canvas.Transform.SetTranslation(x, y - 1.0f);
                canvas.Mask.Push().SetEnable(true).SetCircle(new Rect2D(0.2, 0.2, 0.8, 0.8));
                canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
                canvas.Mask.Pop();
            }

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(x + 1.0f, y - 2.0f);
            canvas.Mask.Push(true).SetRoundedRect(new Rect2D(0.0, 0.0, 1.0, 1.0), 0.3f, 0.3f, 0.3f, 0.3f);
            canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
            canvas.Mask.Pop();

            canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
            canvas.Transform.SetTranslation(x + 3.0f, y - 3.0f);
            canvas.Mask.Push(true).SetStar(new Rect2D(0.0, 0.0, 1.0, 1.0), starSides, 0.5);
            canvas.FillRectangle(new Rect2D(0, 0, 1, 1));
            canvas.Mask.Pop();

            {
                var line = canvas.AcquirePath();
                line.MoveTo(new Point2D(0.0, 0.0));
                line.LineTo(new Point2D(1.0, 0.0));
                line.LineTo(new Point2D(1.0, 1.0));
                line.LineTo(new Point2D(0.0, 1.0));
                line.Close();

                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x + 2, y + 2);
                canvas.StrokeOnePixelLine(line, true);
            }

            {
                var line = canvas.AcquirePath();
                line.MoveTo(new Point2D(0.0, 0.0));
                line.LineTo(new Point2D(0.0, 1.0));
                line.MoveTo(new Point2D(1.0, 0.0));
                line.LineTo(new Point2D(1.0, 1.0));

                canvas.FillShader.SetColor(new RGBA(1.0, 1.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(x + 3.1f, y + 2);
                canvas.StrokeOnePixelLine(line, true);
            }
            time += 0.01;
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            posString = " (" + mouse.X + "," + mouse.Y + ") - (" + pos.x + "," + pos.y + ")";

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

            if (keyboard.IsKeyPressed(Keys.R))
                starSides++;

            if (keyboard.IsKeyPressed(Keys.M))
            {
                var st = x;
                gameEvent.animationEngine.Add(5.0, (in AnimationEvent ae, ref AnimationAction action) =>
                {
                    if (ae.iteration % 2 == 0)
                        x = st + (float)ae.t;
                    else
                        x = st - (float)ae.t;
                    if (action == AnimationAction.PAUSE)
                    {
                        if (ae.pausedt >= 0.5)
                            action = AnimationAction.RESUME;
                    }
                    if (ae.pausedCount == 0 && ae.t >= 0.5)
                        action = AnimationAction.PAUSE;
                    if (ae.iteration < 1 && action == AnimationAction.CANCEL)
                        action = AnimationAction.RESTART;
                });
            }

            if (keyboard.IsKeyPressed(Keys.P) && player != null)
                player.SetPaused(!player.IsPaused);
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            image = IAtomicDecoder.LoadFromFile("resources/dices.png").CloneToBuffer(gameEvent.canvasContext, new CreateBufferParams(), true);

            font = gameEvent.canvasContext.LoadFont("resources/font.json", "resources/font.png");

            sound = gameEvent.audioContext.LoadFromFile("resources/mixkit.wav", SoundLoadMode.FULL);
            music = gameEvent.audioContext.LoadFromFile("resources/sample.ogg", SoundLoadMode.STREAM);

            player = gameEvent.audioThread.AcquirePlayer();
            player?.SetSound(music, true, false).Play();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}

