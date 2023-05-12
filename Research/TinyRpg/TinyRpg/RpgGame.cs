using OpenTK.Core;
using TinyRpgLib;
using UDK;

namespace TinyRpgApp
{
    public class RpgGame : IGameDelegate
    {
        #region Atributos
        Personaje main = new Personaje(10,10);
        List<Personaje> npcs = new List<Personaje>();
        World currentWorld;
        int[,] representativeWorld = new int[3, 3];
        int maxWorldWidth = 40;
        int maxWorldHeight = 40;
        int minWorldWidth = 0;
        int minWorldHeight = 0;
        double transitiondelay = 0;
        double ChangeMoveStep = 0;
        double circlePathing = 0;
        bool IsTransitionDone = true;
        bool isTransitioning = false;
        #endregion
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Time.UpdateDeltaTime();
            canvas.Clear(new rgba_f64(0.5, 0.3, 0.1, 1));
            canvas.Camera.SetRect(rect2d_f64.FromMinMax(-10 + main.position.x, -10 + main.position.y, 10 + main.position.x, 10 + main.position.y), true);

            DetectedChangeWorld(main.position.x, main.position.y);

            RenderWorld(canvas, currentWorld);
            RenderPortal(canvas);
            RenderProta(canvas);
            RenderPersonajes(canvas);
            

            if (!IsTransitionDone)
            {
                Transition(canvas);
            }
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            ChangeMoveStep += Time.deltaTime;
            circlePathing += Time.deltaTime;
            Move(gameEvent, keyboard, mouse);
            RandomMoveNpcs();

            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            if (pos.x < 0.0 || pos.y < 0.0)
                return;

            int x = (int)pos.x;
            int y = (int)pos.y;

            if(keyboard.IsKeyPressed(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, 4);
            GenerateNpc();
            GeneratePathing(npcs);
            FillArrayRepresentative();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
            
        }

        public void RandomMoveNpcs()
        {
            foreach (Personaje p in npcs)
            {
                if (p.pathingRoute == 1)
                {
                    HorizontalPathing(p);
                }
                else if (p.pathingRoute == 2)
                {
                    VerticalPathing(p);
                }
                else if (p.pathingRoute == 3)
                {
                    CircularPathing(p);
                }

                LimitedWorld(p.position.x, p.position.y, p);
            }
        }

        public void Move(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        { 
            if (!isTransitioning)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    main.position.x += 20 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Left))
                {
                    main.position.x -= 20 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Up))
                {
                    main.position.y += 20 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Down))
                {
                    main.position.y -= 20 * Time.deltaTime;
                }
            }
            

            LimitedWorld(main.position.x, main.position.y, main);
        }

        public void LimitedWorld(double x, double y, Personaje p)
        {
            if (x < minWorldWidth)
            {
                p.position.x = minWorldWidth;
            }
            
            if (x > maxWorldWidth - 1)
            {
                p.position.x = maxWorldWidth - 1;
            }
            
            if(y < minWorldHeight)
            {
                p.position.y = minWorldHeight;
            }
            
            if (y > maxWorldHeight - 1)
            {
                p.position.y = maxWorldHeight - 1;
            }
        }

        public void RenderWorld(ICanvas canvas, World world)
        {
            if (world.ideidentifier == 4)
            {
                canvas.FillShader.SetColor(new rgba_f64(0.0, 0.56, 0.22, 1.0));
                canvas.Transform.SetTranslation(0, 0);
                canvas.DrawRectangle(new rect2d_f64(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight));
            }
            else
            {
                canvas.FillShader.SetColor(new rgba_f64(0.5, 0.56, 0.22, 1.0));
                canvas.Transform.SetTranslation(0, 0);
                canvas.DrawRectangle(new rect2d_f64(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight));
            }
        }

        public void DetectedChangeWorld(double x, double y) //Mejorable sabes como
        {
            int portalTouched = WhatPortalMainCharacterIs();

            if (portalTouched == 0)
            {
                currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched));
                main.position.x = maxWorldWidth - 2;
                IsTransitionDone= false;
            }
            else if (portalTouched == 1)
            {
                currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched));
                main.position.y = minWorldHeight + 2;
                IsTransitionDone = false;
            }
            else if (portalTouched == 2)
            {
                currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched));
                main.position.x = minWorldWidth + 2;
                IsTransitionDone = false;
            }
            else if (portalTouched == 3)
            {
                currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched));
                main.position.y = maxWorldHeight - 2;
                IsTransitionDone = false;
            }


        }

        public int WhatPortalMainCharacterIs()
        {
            foreach (Portal p in currentWorld.portals)
            {
                if (main.position.x >= p.aabb.x && p.aabb.MaxX >= main.position.x)
                {
                    if (main.position.y >= p.aabb.y && p.aabb.MaxY >= main.position.y)
                    {
                        return p.id;
                    }
                }
            }

            return -1;
        }

        public void Transition(ICanvas canvas)
        {
            isTransitioning = true;
            transitiondelay += Time.deltaTime;
            if (transitiondelay < 0.2)
                canvas.Clear(new rgba_f64(0, 0, 0, 1));
            else
            {
                IsTransitionDone = true;
                isTransitioning = false;
                transitiondelay = 0;
            }
                
        }

        public void GenerateNpc()
        {
            for (int i = 0; i < Tools.GetRandomInt(1, 4); i++)
            {
                npcs.Add(new Personaje(Tools.GetRandomInt(minWorldWidth, maxWorldWidth), Tools.GetRandomInt(minWorldHeight, maxWorldHeight), Tools.GetRandomInt(1, 4)));
            }
        }

        public void RenderPortal(ICanvas canvas)
        {
            foreach (Portal p in currentWorld.portals)
            {
                canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(0, 0);
                canvas.DrawRectangle(p.aabb);
                
            }
        }

        public void RenderProta(ICanvas canvas)
        {
            canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
            canvas.Transform.SetTranslation(main.position.x, main.position.y);
            canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 1, 1));
            canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1, 1));
            canvas.Mask.Pop();
        }

        public void RenderPersonajes(ICanvas canvas)
        {
            foreach (Personaje p in npcs)
            {
                canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(p.position.x, p.position.y);
                canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 1, 1));
                canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1, 1));
                canvas.Mask.Pop();
            }
        }

        public void GeneratePathing(List<Personaje> npcs)
        {
            foreach (Personaje p in npcs)
            {
                int random = Tools.GetRandomInt(1, 4);
                p.pathingRoute = random;
            }
        }

        public void HorizontalPathing(Personaje p)
        {
            if (ChangeMoveStep < 2)
                p.position.x += 10 * Time.deltaTime;
            else if (ChangeMoveStep < 4)
                p.position.x -= 10 * Time.deltaTime;
            else
                ChangeMoveStep = 0;
        }

        public void VerticalPathing(Personaje p)
        {
            if (ChangeMoveStep < 2)
                p.position.y += 10 * Time.deltaTime;
            else if (ChangeMoveStep < 4)
                p.position.y -= 10 * Time.deltaTime;
            else
                ChangeMoveStep = 0;
        }

        public void CircularPathing(Personaje p)
        {
            if (circlePathing < 1)
                p.position.x += 10 * Time.deltaTime;
            else if (circlePathing < 2)
                p.position.y += 10 * Time.deltaTime;
            else if (circlePathing < 3)
                p.position.x -= 10 * Time.deltaTime;
            else if (circlePathing < 4)
                p.position.y -= 10 * Time.deltaTime;
            else
                circlePathing = 0;
        }

        public void FillArrayRepresentative()
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    representativeWorld[j, i] = count;
                    count++;
                }
            }
        }

        public int SelectWorld(int id)
        {
            Position[] positions;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    positions = new Position[] { new Position(j - 1, i), new Position(j, i - 1), new Position(j + 1, i), new Position(j, i + 1) };

                    if (representativeWorld[j, i] == currentWorld.ideidentifier)
                        return representativeWorld[(int)positions[id].x, (int)positions[id].y];
                }
            }

            return -1;
        }
    }
}
