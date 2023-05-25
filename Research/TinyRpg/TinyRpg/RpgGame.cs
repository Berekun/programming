using System.Reflection.Emit;
using System.Text.Json;
using TinyRpgLib;
using UDK;
using static UDK.KeyboardJoystick8;

namespace TinyRpgApp
{
    public class RpgGame : IGameDelegate
    {
        #region Atributos
        enum ProtaStates
        {
            STAY_FRONT, STAY_BACK, STAY_LEFT, STAY_RIGHT,
            MOVE_FRONT, MOVE_BACK, MOVE_LEFT, MOVE_RIGHT
        }
        enum SpriteClass
        {
            UNKNOWN, GRASS_CLAIR, GRASS_DARK
        }
        enum TileStates
        {
            GRASS_DARK, IN_GRASS_CLAIR_OUT_GRASS_DARK
        }

        ImageDatabase? database;
        SpriteSet? spriteSet;
        SpriteInstance? prota;
        Personaje mainCharacter = new Personaje(Constants.spawnMainCharacterX, Constants.spawnMainCharacterX, 20);
        KeyboardJoystick8 joystickMovement = new KeyboardJoystick8(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
        KeyboardJoystick8 joystickShoot = new KeyboardJoystick8(Keys.W, Keys.S, Keys.A, Keys.D);
        List<Bala> bullets = new List<Bala>();
        World currentWorld;
        string path = "resources/worlds.json";
        int[,] representativeWorld = new int[3, 3];
        #region WorldSizes
        int maxWorldWidth = 40;
        int maxWorldHeight = 40;
        int minWorldWidth = 0;
        int minWorldHeight = 0;
        #endregion
        #region Timers
        double transitiondelay = 0;
        double enemyChangeMoveStep = 0;
        double enemycirclePathing = 0;
        double shootMainCharacterDelay = 0;
        double shootEnemieDelay = 0;
        #endregion
        #region Booleans
        bool IsTransitionDone = true;
        bool isTransitioning = false;
        #endregion
        #endregion
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
            if (prota == null)
                return;

            Time.UpdateDeltaTime();
            canvas.Clear(new rgba_f64(0.5, 0.3 * mainCharacter.transparency, 0.1 * mainCharacter.transparency, 1.0));
            currentWorld.tileWorld.SetupCamera(canvas, new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y), Constants.radiusViewWorld, 0.0, 0.0, true);

            RenderWorld(canvas, currentWorld);
            RenderPortal(canvas);
            RenderProta(canvas);
            RenderEnemies(canvas);
            RenderBullets(canvas);

            if (!IsTransitionDone)                
                Transition(canvas);
        }

        public void OnAnimate(GameDelegateEvent gameEvent)
        {
            DetectedChangeWorld(mainCharacter.position.X, mainCharacter.position.Y);

            RandomMoveNpcs();
            MoveBullets();

            EnemieShoot();
            KillEnemies(currentWorld.enemies);
            HitEnemieToMainCharacter(currentWorld.enemies, gameEvent);
            HitBulletToMainCharacter(bullets, gameEvent);
            currentWorld.IsWorldClearFuncion();
            RemoveBullet(bullets);

            prota?.Animate(gameEvent.animationEngine);
            currentWorld.tileWorld?.Animate(gameEvent.animationEngine);
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            UpdateTimers();
            Move(gameEvent, keyboard, mouse);
            Shoot(gameEvent, keyboard, mouse);

            var pos = gameEvent.coordinateConversor.ViewToWorld(mouse.X, mouse.Y);
            if (pos.x < 0.0 || pos.y < 0.0)
                return;

            int x = (int)pos.x;
            int y = (int)pos.y;

            if (keyboard.IsKeyPressed(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            if(File.Exists(path))
                File.Delete(path);
            currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, 4, true);
            FillArrayRepresentative();
            database = new ImageDatabase(gameEvent.canvasContext);
            spriteSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/movement_set.json", database, typeof(ProtaStates));
            
            var mapSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/map/map_set.json", database, typeof(TileStates));
            var layer = SpriteLoaderUtils.LoadLayerFromFile("resources/map/layer_ground.json", mapSet, typeof(TileStates), typeof(SpriteClass));
            prota = new SpriteInstance(spriteSet, (int)ProtaStates.STAY_FRONT, 0, -1);
            currentWorld.tileWorld.AddLayer(layer, 0, 0);
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {

        }
       
        #region Movements
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
                var state = joystickMovement.Update(keyboard);
                var lastState = joystickMovement.Last;

                if (state == KeyboardJoystick8.State.UP)
                    SetSequenceUpOrDown(state);

                if (state == KeyboardJoystick8.State.DOWN)
                    SetSequenceUpOrDown(state);

                if (state == KeyboardJoystick8.State.LEFT || state == KeyboardJoystick8.State.DOWN_LEFT || state == KeyboardJoystick8.State.UP_LEFT)
                    SetSequenceLeft(state);

                if (state == KeyboardJoystick8.State.RIGHT || state == KeyboardJoystick8.State.DOWN_RIGHT || state == KeyboardJoystick8.State.UP_RIGHT)
                    SetSequenceRight(state);

                if (state == KeyboardJoystick8.State.RELEASED)
                    SetSequenceReleased(lastState);
            }

            LimitedWorld(mainCharacter.position.X, mainCharacter.position.Y, mainCharacter);
        }

        public void SetSequenceRight(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_RIGHT)
                {
                    EndId = (int)ProtaStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.DOWN_RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_RIGHT)
                {
                    EndId = (int)ProtaStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP_RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_RIGHT)
                {
                    EndId = (int)ProtaStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }
        }

        public void SetSequenceLeft(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_LEFT)
                {
                    EndId = (int)ProtaStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.DOWN_LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_LEFT)
                {
                    EndId = (int)ProtaStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP_LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_LEFT)
                {
                    EndId = (int)ProtaStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }

        }

        public void SetSequenceUpOrDown(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.DOWN)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_FRONT)
                {
                    EndId = (int)ProtaStates.STAY_FRONT
                });
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.MOVE_BACK)
                {
                    EndId = (int)ProtaStates.STAY_BACK
                });
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }
            
        }

        public void SetSequenceReleased(KeyboardJoystick8.State lastState)
        {
            if (lastState == KeyboardJoystick8.State.UP)
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.STAY_BACK));

            if (lastState == KeyboardJoystick8.State.DOWN)
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.STAY_FRONT));

            if (lastState == KeyboardJoystick8.State.LEFT || lastState == KeyboardJoystick8.State.DOWN_LEFT || lastState == KeyboardJoystick8.State.UP_LEFT)
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.STAY_LEFT));

            if (lastState == KeyboardJoystick8.State.RIGHT || lastState == KeyboardJoystick8.State.DOWN_RIGHT || lastState == KeyboardJoystick8.State.UP_RIGHT)
                prota?.SetSequence(new SpriteSequenceSelector((int)ProtaStates.STAY_RIGHT));
        }
        public void MoveBullets()
        {
            foreach (Bala b in bullets)
            {
                b.position.X += b.direction.x * Time.deltaTime;
                b.position.Y += b.direction.y * Time.deltaTime;
            }

            LimitedWorld(mainCharacter.position.X, mainCharacter.position.Y, mainCharacter);
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

        public void HorizontalPathing(Personaje p)
        {
            if (enemyChangeMoveStep < 2)
                p.position.X += 10 * Time.deltaTime;
            else if (enemyChangeMoveStep < 4)
                p.position.X -= 10 * Time.deltaTime;
            else
                enemyChangeMoveStep = 0;
        }

        public void VerticalPathing(Personaje p)
        {
            if (enemyChangeMoveStep < 2)
                p.position.Y += 10 * Time.deltaTime;
            else if (enemyChangeMoveStep < 4)
                p.position.Y -= 10 * Time.deltaTime;
            else
                enemyChangeMoveStep = 0;
        }

        public void CircularPathing(Personaje p)
        {
            if (enemycirclePathing < 1)
                p.position.X += 10 * Time.deltaTime;
            else if (enemycirclePathing < 2)
                p.position.Y += 10 * Time.deltaTime;
            else if (enemycirclePathing < 3)
                p.position.X -= 10 * Time.deltaTime;
            else if (enemycirclePathing < 4)
                p.position.Y -= 10 * Time.deltaTime;
            else
                enemycirclePathing = 0;
        }
        #endregion

        #region Renders
        public void RenderWorld(ICanvas canvas, World world)
        {
            currentWorld.tileWorld?.Draw(canvas, 0.0, 0.0);
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
            //double transparency = 1.0;
            //prota?.Color = vec4d_f64(1.0, 1.0, 1.0, transparency);
            prota?.Draw(canvas, mainCharacter.position.X - Constants.middleSprite, mainCharacter.position.Y - Constants.middleSprite, 2.0, 2.0);
        }

        public void RenderEnemies(ICanvas canvas)
        {
            foreach (Personaje p in currentWorld.enemies)
            {
                //prota?.Draw(canvas, p.position.X - MagicNumbers.middleSprite, p.position.Y - MagicNumbers.middleSprite, 2.0, 2.0);

                canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                canvas.Transform.SetTranslation(p.position.X, p.position.Y);
                canvas.DrawRectangle(new rect2d_f64(0, 0, 1, 1));
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

        #endregion

        #region WorldChange
        public void DetectedChangeWorld(double x, double y)
        {
            Position[] positions = new Position[] { new Position(maxWorldWidth - 2, mainCharacter.position.Y), new Position(mainCharacter.position.X, minWorldHeight + 2), new Position(minWorldWidth + 2, mainCharacter.position.Y), new Position(mainCharacter.position.X, maxWorldHeight - 2) };

            int portalTouched = WhatPortalMainCharacterIs();

            string worldsJson = "";

            if (currentWorld.IsWorldClear)
            {
                if (portalTouched != -1)
                {
                    if(File.Exists(path))
                        worldsJson = File.ReadAllText(path);

                    if (worldsJson != "")
                    {
                        Dictionary<int, World> worlds = JsonSerializer.Deserialize<Dictionary<int, World>>(worldsJson);

                        if (!worlds.ContainsKey(SelectWorld(portalTouched)))
                            GenerateWorld(positions, portalTouched);
                        else
                        {
                            foreach (var world in worlds)
                            {
                                World newWorld = world.Value;

                                if (newWorld.ideidentifier == SelectWorld(portalTouched))
                                    ReplaceWorld(positions, portalTouched, newWorld);
                            }
                        }  
                    }
                    else
                        GenerateWorld(positions, portalTouched);
                }
            }
        }

        public int WhatPortalMainCharacterIs()
        {
            foreach (Portal p in currentWorld.portals)
            {
                if (mainCharacter.position.X + 0.5 >= p.aabb.x && p.aabb.MaxX >= mainCharacter.position.X + 0.5)
                {
                    if (mainCharacter.position.Y + 0.5 >= p.aabb.y && p.aabb.MaxY >= mainCharacter.position.Y + 0.5)
                    {
                        return p.id;
                    }
                }
            }

            return -1;
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

        public void GenerateWorld(Position[] positions, int portalTouched)
        {
            SerializerJsonWorld(currentWorld);
            currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight, SelectWorld(portalTouched), false);
            SelectMap(currentWorld.ideidentifier);
            mainCharacter.position = positions[portalTouched];
            bullets.Clear();
            IsTransitionDone = false;
        }

        public void ReplaceWorld(Position[] positions, int portalTouched, World newWorld)
        {
            SerializerJsonWorld(currentWorld);
            currentWorld = newWorld;
            currentWorld.GeneratePortals(currentWorld.ideidentifier);
            SelectMap(newWorld.ideidentifier);
            mainCharacter.position = positions[portalTouched];
            bullets.Clear();
            IsTransitionDone = false;
        }

        public void SelectMap(int id)
        {
            currentWorld.tileWorld = new TileWorld(20, 20, new aabb2d_f64(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight));
            var mapSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/map/mapstxt/" + id + ".json", database, typeof(TileStates));
            var layer = SpriteLoaderUtils.LoadLayerFromFile("resources/map/layer_ground.json", mapSet, typeof(TileStates), typeof(SpriteClass));
            currentWorld.tileWorld.AddLayer(layer, 0, 0);
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
        #endregion

        #region ThingsWithBullets
        public void Shoot(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (shootMainCharacterDelay > 0.5)
            {
                var state = joystickShoot.Update(keyboard);

                switch (state)
                {
                    case KeyboardJoystick8.State.UP:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(0, 1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.DOWN:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(0, -1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.UP_LEFT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(-1, 1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.DOWN_LEFT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(-1, -1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.LEFT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(-1, 0) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.UP_RIGHT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(1, 1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.DOWN_RIGHT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(1, -1) * 10, Shooter.MAIN));
                        break;
                    case KeyboardJoystick8.State.RIGHT:
                        bullets.Add(new Bala(mainCharacter.position.X, mainCharacter.position.Y, new vec2d_f64(1, 0) * 10, Shooter.MAIN));
                        break;
                }

                shootMainCharacterDelay = 0;
            }
        }

        public void EnemieShoot()
        {
            if (shootEnemieDelay > 1)
            {
                vec2d_f64 main_pos = new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y);

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
        #endregion

        #region ThingsWithCharacters

        public bool DoesIntersectEnemyWithBullet(Position pos1, double enemySize, Position pos2, double bulleSize)
        {
            if (pos1.X > pos2.maxX)
                return false;
            if (pos2.X > pos1.maxX)
                return false;

            if (pos1.Y > pos2.maxY)
                return false;
            if (pos2.Y > pos1.maxY)
                return false;

            return true;
        }

        public void KillEnemies(List<Enemigo> enemigos)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemigos.Count; j++)
                {
                    if (bullets[i].shooter == Shooter.MAIN)
                    {
                        if (DoesIntersectEnemyWithBullet(enemigos[j].position, 1.0, bullets[i].position, 0.25))
                        {
                            enemigos[j].vida -= Constants.mainBulletDamage;
                            bullets.Remove(bullets[i]);
                        }
                        
                        if (enemigos[j].vida <= 0)
                            enemigos.Remove(enemigos[j]);
                    }
                }
            }
        }

        public void HitEnemieToMainCharacter(List<Enemigo> enemigos, GameDelegateEvent gameDelegate)
        {
            for (int j = 0; j < enemigos.Count; j++)
            {
                if (DoesIntersectEnemyWithBullet(mainCharacter.position, 1.0, enemigos[j].position, 1.0))
                {
                    gameDelegate.animationEngine.Add(new AnimationOptions()
                    {
                        Duration = 0.1
                    },
                    (in AnimationEvent ae, ref AnimationAction action) =>
                    {
                        var transparency = Math.Cos(ae.u * 5.0) * 0.5 + 0.5;
                        mainCharacter.transparency = transparency;
                        if (ae.u == 1.0)
                            mainCharacter.transparency = 1.0;
                    }
                    );
                    mainCharacter.vida -= Constants.enemyBulletDamage;
                }

                if (mainCharacter.vida <= 0)
                    gameDelegate.window.Close();
            }
        } 

        public void HitBulletToMainCharacter(List<Bala> balas, GameDelegateEvent gameDelegate)
        {
            for (int i = 0; i < balas.Count; i++)
            {
                if (balas[i].shooter == Shooter.ENEMIE)
                {
                    if (DoesIntersectEnemyWithBullet(mainCharacter.position, 1.0, bullets[i].position, 0.25))
                    {
                        gameDelegate.animationEngine.Add(new AnimationOptions()
                            {
                                Duration = 0.1
                            },
                        (in AnimationEvent ae, ref AnimationAction action) =>
                            {
                                var transparency = Math.Cos(ae.u * 5.0) * 0.5 + 0.5;
                                mainCharacter.transparency = transparency;
                                if (ae.u == 1.0)
                                    mainCharacter.transparency = 1.0;
                            }
                        );
                        mainCharacter.vida -= Constants.enemyBulletDamage;
                        bullets.Remove(bullets[i]);
                    }

                    if (mainCharacter.vida <= 0)
                        gameDelegate.window.Close();
                }
            }
        }
        #endregion

        #region JsonFunctions
        public void SerializerJsonWorld(World world)
        {
            Dictionary<int, World>? worlds = null;

            if (File.Exists(path))
            {
                string jsonIfNotExistFile = File.ReadAllText(path);
                worlds = JsonSerializer.Deserialize<Dictionary<int, World>>(jsonIfNotExistFile);
            }
            if (worlds == null)
                worlds = new Dictionary<int, World>();

            if (worlds.ContainsKey(world.ideidentifier))
            {
                File.Delete(path);
            }


            worlds[world.ideidentifier] = world;

            string json = JsonSerializer.Serialize(worlds);
            File.WriteAllText(path, json);
        }
        #endregion

        #region ToolsFunctions
        public void UpdateTimers()
        {
            enemyChangeMoveStep += Time.deltaTime;
            enemycirclePathing += Time.deltaTime;
            shootMainCharacterDelay += Time.deltaTime;
            shootEnemieDelay += Time.deltaTime;
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
        #endregion
    }
}
