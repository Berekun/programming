﻿using System.Text.Json;
using System.Text.Json.Nodes;
using TinyRpgLib;
using UDK;
using static UDK.RoomRT;

namespace TinyRpgApp
{
    public class RpgGame : IGameDelegate, IRpgGame
    {
        #region Atributos
        Personaje main = new Personaje(10, 10);
        List<Bala> bullets = new List<Bala>();
        World currentWorld;
        string path = @"D:\\Documentos\\Deberes de Dani\\Programacion\\programminga\\programming\\Research\\TinyRpg\\TinyRpg\\resouces\\";
        int[,] representativeWorld = new int[3, 3];
        int maxWorldWidth = 40;
        int maxWorldHeight = 40;
        int minWorldWidth = 0;
        int minWorldHeight = 0;
        double transitiondelay = 0;
        double ChangeMoveStep = 0;
        double circlePathing = 0;
        double shootDelay = 0;
        double shootEnemieDelay = 0;
        bool IsTransitionDone = true;
        bool isTransitioning = false;
        #endregion
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            Time.UpdateDeltaTime();
            canvas.Clear(new rgba_f64(0.5, 0.3, 0.1, 1));
            canvas.Camera.SetRect(rect2d_f64.FromMinMax(-10 + main.position.X, -10 + main.position.Y, 10 + main.position.X, 10 + main.position.Y), true);

            DetectedChangeWorld(main.position.X, main.position.Y);

            RenderWorld(canvas, currentWorld);
            RenderPortal(canvas);
            RenderProta(canvas);
            RenderEnemies(canvas);
            KillEnemies(bullets, currentWorld.enemies);
            RenderBullets(canvas);

            if (!IsTransitionDone)
            {
                Transition(canvas);
            }
        }

        public void OnAnimate(GameDelegateEvent gameEvent)
        {

        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            ChangeMoveStep += Time.deltaTime;
            circlePathing += Time.deltaTime;
            shootDelay += Time.deltaTime;
            shootEnemieDelay += Time.deltaTime;
            Move(gameEvent, keyboard, mouse);
            Shoot(gameEvent, keyboard, mouse);
            EnemieShoot();

            MoveBullets();
            RemoveBullet(bullets);
            RandomMoveNpcs();

            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            if (pos.x < 0.0 || pos.y < 0.0)
                return;

            int x = (int)pos.x;
            int y = (int)pos.y;

            HitEnemieToMainCharacter(currentWorld.enemies, gameEvent);
            HitBulletToMainCharacter(bullets, gameEvent);

            if (keyboard.IsKeyPressed(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, 4);
            GeneratePathing(currentWorld.enemies);
            FillArrayRepresentative();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
            string voidjson = "";

            File.WriteAllText(path + "world.json", voidjson);
        }

        public void RandomMoveNpcs()
        {
            foreach (Enemigo e in currentWorld.enemies)
            {
                if (e.pathingRoute == 1)
                {
                    HorizontalPathing(e);
                }
                else if (e.pathingRoute == 2)
                {
                    VerticalPathing(e);
                }
                else if (e.pathingRoute == 3)
                {
                    CircularPathing(e);
                }

                LimitedWorld(e.position.X, e.position.Y, e);
            }
        }

        public void Move(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (!isTransitioning)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    main.position.X += 8 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Left))
                {
                    main.position.X -= 8 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Up))
                {
                    main.position.Y += 8 * Time.deltaTime;
                }

                if (keyboard.IsKeyDown(Keys.Down))
                {
                    main.position.Y -= 8 * Time.deltaTime;
                }
            }


            LimitedWorld(main.position.X, main.position.Y, main);
        }

        public void MoveBullets()
        {
            foreach (Bala b in bullets)
            {
                b.position.X += b.direction.x * Time.deltaTime;
                b.position.Y += b.direction.y * Time.deltaTime;
            }

            LimitedWorld(main.position.X, main.position.Y, main);
        }

        public void LimitedWorld(double x, double y, Personaje p)
        {
            if (x < minWorldWidth)
            {
                p.position.X = minWorldWidth;
            }

            if (x > maxWorldWidth - 1)
            {
                p.position.X = maxWorldWidth - 1;
            }

            if (y < minWorldHeight)
            {
                p.position.Y = minWorldHeight;
            }

            if (y > maxWorldHeight - 1)
            {
                p.position.Y = maxWorldHeight - 1;
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
            Position[] positions = new Position[] { new Position(maxWorldWidth - 2, main.position.Y), new Position(main.position.X, minWorldHeight + 2), new Position(minWorldWidth + 2, main.position.Y), new Position(main.position.X, maxWorldHeight - 2) };

            int portalTouched = WhatPortalMainCharacterIs();

            if (currentWorld.enemies.Count <= 0)
            {
                if (portalTouched != -1)
                {
                    string worldsJson = File.ReadAllText(path + "world.json");

                    if (worldsJson != "")
                    {
                        List<World> worlds = new List<World>();

                        try
                        {
                            worlds = JsonSerializer.Deserialize<List<World>>(worldsJson);
                        }
                        catch (Exception)
                        {
                            worlds.Add(JsonSerializer.Deserialize<World>(worldsJson));
                        }

                        for (int i = 0; i < worlds.Count; i++)
                        {
                            if (worlds[i].ideidentifier == SelectWorld(portalTouched))
                            {
                                currentWorld.IsWorldClearFuncion();
                                SerializerJsonWorld(currentWorld);
                                currentWorld = worlds[i];
                                currentWorld.GeneratePortals(currentWorld.ideidentifier);
                                main.position = positions[portalTouched];
                                if (!currentWorld.IsWorldClear)
                                    GenerateEnemies();
                                bullets.Clear();
                                IsTransitionDone = false;
                            }
                        }
                    }
                    else
                    {
                        currentWorld.IsWorldClearFuncion();
                        SerializerJsonWorld(currentWorld);
                        currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched));
                        main.position = positions[portalTouched];
                        if (!currentWorld.IsWorldClear)
                            GenerateEnemies();
                        bullets.Clear();
                        IsTransitionDone = false;
                        
                    }
                }
            }
        }

        public int WhatPortalMainCharacterIs()
        {
            foreach (Portal p in currentWorld.portals)
            {
                if (main.position.X + 0.5 >= p.aabb.x && p.aabb.MaxX >= main.position.X + 0.5)
                {
                    if (main.position.Y + 0.5 >= p.aabb.y && p.aabb.MaxY >= main.position.Y + 0.5)
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

        public void GenerateEnemies()
        {
            currentWorld.enemies.Clear();

            for (int i = 0; i < Tools.GetRandomInt(1, 10); i++)
            {
                currentWorld.enemies.Add(new Enemigo(Tools.GetRandomInt(minWorldWidth, maxWorldWidth), Tools.GetRandomInt(minWorldHeight, maxWorldHeight), Tools.GetRandomInt(1, 4)));
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
            canvas.Transform.SetTranslation(main.position.X, main.position.Y);
            canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 1, 1));
            canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1, 1));
            canvas.Mask.Pop();
        }

        public void RenderEnemies(ICanvas canvas)
        {
            foreach (Personaje p in currentWorld.enemies)
            {
                canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(p.position.X, p.position.Y);
                canvas.Mask.PushCircle(new rect2d_f64(0.0, 0.0, 1, 1));
                canvas.DrawRectangle(new rect2d_f64(0.0, 0.0, 1, 1));
                canvas.Mask.Pop();
            }
        }

        public void RenderBullets(ICanvas canvas)
        {
            foreach (Bala b in bullets)
            {
                if (b.shooter == Shooter.MAIN)
                {
                    canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
                    canvas.Transform.SetTranslation(b.position.X, b.position.Y);
                    canvas.DrawRectangle(new rect2d_f64(0.25, 0.25, 0.5, 0.5));
                }
                else
                {
                    canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                    canvas.Transform.SetTranslation(b.position.X, b.position.Y);
                    canvas.DrawRectangle(new rect2d_f64(0.25, 0.25, 0.5, 0.5));
                }

            }
        }

        public void GeneratePathing(List<Enemigo> enemies)
        {
            foreach (Enemigo e in enemies)
            {
                int random = Tools.GetRandomInt(1, 4);
                e.pathingRoute = random;
            }
        }

        public void HorizontalPathing(Personaje p)
        {
            if (ChangeMoveStep < 2)
                p.position.X += 10 * Time.deltaTime;
            else if (ChangeMoveStep < 4)
                p.position.X -= 10 * Time.deltaTime;
            else
                ChangeMoveStep = 0;
        }

        public void VerticalPathing(Personaje p)
        {
            if (ChangeMoveStep < 2)
                p.position.Y += 10 * Time.deltaTime;
            else if (ChangeMoveStep < 4)
                p.position.Y -= 10 * Time.deltaTime;
            else
                ChangeMoveStep = 0;
        }

        public void CircularPathing(Personaje p)
        {
            if (circlePathing < 1)
                p.position.X += 10 * Time.deltaTime;
            else if (circlePathing < 2)
                p.position.Y += 10 * Time.deltaTime;
            else if (circlePathing < 3)
                p.position.X -= 10 * Time.deltaTime;
            else if (circlePathing < 4)
                p.position.Y -= 10 * Time.deltaTime;
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
                        return representativeWorld[(int)positions[id].X, (int)positions[id].Y];
                }
            }

            return -1;
        }

        public void Shoot(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (shootDelay > 0.5)
            {
                if (keyboard.IsKeyDown(Keys.A))
                    bullets.Add(new Bala(main.position.X, main.position.Y, new vec2d_f64(-1, 0) * 10, Shooter.MAIN));

                if (keyboard.IsKeyDown(Keys.W))
                    bullets.Add(new Bala(main.position.X, main.position.Y, new vec2d_f64(0, 1) * 10, Shooter.MAIN));

                if (keyboard.IsKeyDown(Keys.D))
                    bullets.Add(new Bala(main.position.X, main.position.Y, new vec2d_f64(1, 0) * 10, Shooter.MAIN));

                if (keyboard.IsKeyDown(Keys.S))
                    bullets.Add(new Bala(main.position.X, main.position.Y, new vec2d_f64(0, -1) * 10, Shooter.MAIN));

                shootDelay = 0;
            }
        }

        public void EnemieShoot()
        {
            if (shootEnemieDelay > 1)
            {
                vec2d_f64 main_pos = new vec2d_f64(main.position.X, main.position.Y);

                foreach (Enemigo e in currentWorld.enemies)
                {
                    vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                    vec2d_f64 vec = main_pos - enemy_pos;
                    vec2d_f64 normlaized_vec = vec.normalized();
                    vec2d_f64 final_vec = normlaized_vec * 7;
                    bullets.Add(new Bala(e.position.X, e.position.Y, final_vec, Shooter.ENEMIE));
                }
                shootEnemieDelay = 0;
            }
        }


        public void RemoveBullet(List<Bala> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].position.X < minWorldWidth)
                {
                    bullets.Remove(bullets[i]);
                }
                else if (bullets[i].position.X > maxWorldWidth - 1)
                {
                    bullets.Remove(bullets[i]);
                }
                else if (bullets[i].position.Y < minWorldHeight)
                {
                    bullets.Remove(bullets[i]);
                }
                else if (bullets[i].position.Y > maxWorldHeight - 1)
                {
                    bullets.Remove(bullets[i]);
                }
            }
        }

        public void KillEnemies(List<Bala> balas, List<Enemigo> enemigos)
        {
            for (int i = 0; i < balas.Count; i++)
            {
                for (int j = 0; j < enemigos.Count; j++)
                {
                    if (balas[i].shooter == Shooter.MAIN)
                    {
                        if (balas[i].position.X + 0.5 >= enemigos[j].position.X && enemigos[j].position.maxX >= balas[i].position.X + 0.5 && balas[i].position.Y + 0.5 >= enemigos[j].position.Y && enemigos[j].position.maxY >= balas[i].position.Y + 0.5)
                            enemigos.Remove(enemigos[j]);
                    }
                }
            }
        }

        public void HitEnemieToMainCharacter(List<Enemigo> enemigos, GameDelegateEvent gameDelegate)
        {
            for (int j = 0; j < enemigos.Count; j++)
            {
                if (main.position.X + 0.5 >= enemigos[j].position.X && enemigos[j].position.maxX >= main.position.X + 0.5 && main.position.Y + 0.5 >= enemigos[j].position.Y && enemigos[j].position.maxY >= main.position.Y + 0.5)
                {
                    gameDelegate.window.Close();
                    break;
                }
            }
        }

        public void HitBulletToMainCharacter(List<Bala> balas, GameDelegateEvent gameDelegate)
        {
            for (int i = 0; i < balas.Count; i++)
            {
                if (balas[i].shooter == Shooter.ENEMIE)
                {
                    if (main.position.X + 0.5 >= balas[i].position.X && balas[i].position.maxX >= main.position.X + 0.5 && main.position.Y + 0.5 >= balas[i].position.Y && balas[i].position.maxY >= main.position.Y + 0.5)
                    {
                        gameDelegate.window.Close();
                        break;
                    }
                }
            }
        }

        public void SerializerJsonWorld(World world)
        {
            string worldsJson = File.ReadAllText(path + "world.json");

            string json = JsonSerializer.Serialize<World>(currentWorld);

            File.WriteAllText(path + "world.json",worldsJson + json);
        }
    }
}
