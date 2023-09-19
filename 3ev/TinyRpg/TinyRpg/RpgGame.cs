using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text.Json;
using TinyRpgLib;
using UDK;
using static UDK.KeyboardJoystick8;

namespace TinyRpgApp
{
    public class RpgGame : IGameDelegate
    {
        #region Atributos
        enum PersonajeStates
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

        enum HeartStates
        {
            FULL_HEART, MID_HEART, VOID_HEART
        }

        ImageDatabase? database;
        SpriteSet? spriteSet;
        SpriteSet? hudSet;
        SpriteSet? mapSet;
        TileMap? layer;
        SpriteInstance? prota;
        SpriteInstance? darkWizzard;
        Personaje mainCharacter = new Personaje(Constants.spawnMainCharacterX, Constants.spawnMainCharacterX, 20);
        KeyboardJoystick8 joystickMovement = new KeyboardJoystick8(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
        KeyboardJoystick8 joystickShoot = new KeyboardJoystick8(Keys.W, Keys.S, Keys.A, Keys.D);
        List<Bala> bullets = new List<Bala>();
        List<SpriteInstance> hearts = new List<SpriteInstance>();
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
        double shootMainCharacterDelay = 0;
        double shootEnemieDelay = 0;
        double protaHitDelay = 1;
        double protaHurtsTimer = 0;
        #endregion
        #region Booleans
        bool isTransitionDone = true;
        bool isTransitioning = false;
        bool isPlayerHurt = false;
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
            //RenderPortal(canvas);
            RenderHearts(canvas);
            RenderProta(canvas);
            RenderEnemies(canvas);
            RenderObstacles(canvas);
            RenderBullets(canvas);


            if (protaHurtsTimer > Constants.hurtsDelay)
                ChangeColorWorld();
        }

        public void OnAnimate(GameDelegateEvent gameEvent)
        {
            CreateHearts();
            MoveEnemies();

            EnemieShoot();
            KillEnemies(currentWorld.enemies, bullets);
            HitToProta(currentWorld.enemies, bullets, gameEvent);
            RemoveBullet(bullets);

            
            prota?.Animate(gameEvent.animationEngine);
            currentWorld.tileWorld?.Animate(gameEvent.animationEngine);
            darkWizzard?.Animate(gameEvent.animationEngine);
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

            if (keyboard.IsKeyPressed(Keys.F11) || keyboard.IsKeyPressed(Keys.F))
                gameEvent.window.ToggleFullscreen();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            if(File.Exists(path))
                File.Delete(path);
            currentWorld = new World(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight);
            FillArrayRepresentative();
            database = new ImageDatabase(gameEvent.canvasContext);
            spriteSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/prota_movetxt/movement_set.json", database, typeof(PersonajeStates));
            hudSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/hud/hud_set.json", database, typeof(HeartStates));
            mapSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/map/map_set.json", database, typeof(TileStates));
            layer = SpriteLoaderUtils.LoadLayerFromFile("resources/map/layers/layer_ground0.json", mapSet, typeof(TileStates), typeof(SpriteClass));
            prota = new SpriteInstance(spriteSet, (int)PersonajeStates.STAY_FRONT, 0, -1);
            currentWorld.tileWorld.AddLayer(layer, 0, 0);
            SetSpriteToEnemys();
            CreateHearts();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {

        }

        #region Movements

        #region Move

        public void MoveEnemies()
        {
            MoveDarkWizzards();
            MoveBullets();
            MoveWolfs();
            MoveGolems();
            MoveMiniGolems();
        }
        public void MoveDarkWizzards()
        {
            foreach (Enemigo e in currentWorld.enemies)
            {
                if (e.enemyType == EnemyType.DARK_WIZZARD)
                {
                    bool stayX = false;
                    bool stayY = false;

                    vec2d_f64 main_pos = new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y);
                    vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                    vec2d_f64 vec = main_pos - enemy_pos;

                    if (vec.x < 9 && 8 < vec.x)
                        stayX = true;

                    if (vec.x > -9 && -8 > vec.x)
                        stayX = true;

                    if (vec.x < 8 && enemy_pos.x < main_pos.x)
                        vec.x = vec.x * -1;

                    if (vec.x > -8 && enemy_pos.x > main_pos.x)
                        vec.x = vec.x * -1;

                    if (vec.y > -9 && -8 > vec.y)
                        stayY = true;

                    if (vec.y < 9 && 8 < vec.y)
                        stayY = true;

                    if (vec.y < 8 && enemy_pos.y < main_pos.y)
                        vec.y = vec.y * -1;

                    if (vec.y > -8 && enemy_pos.y > main_pos.y)
                        vec.y = vec.y * -1;


                    vec2d_f64 normlaized_vec = vec.normalized();
                    vec2d_f64 final_vec = normlaized_vec * 6;

                    if(stayX)
                        final_vec.x = 0;
                    if(stayY)
                        final_vec.y = 0;

                    SetSequenceDarkWizzard(final_vec, main_pos, enemy_pos);

                    e.position.X += final_vec.x * Time.deltaTime;
                    e.position.Y += final_vec.y * Time.deltaTime;

                    LimitedWorld(e.position.X, e.position.Y, e);
                }
            }
        }

        public void MoveWolfs()
        {
            foreach (Enemigo e in currentWorld.enemies)
            {
                if (e.enemyType == EnemyType.WOLF)
                {
                    vec2d_f64 main_pos = new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y);
                    vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                    vec2d_f64 vec = main_pos - enemy_pos;
                    vec2d_f64 normlaized_vec = vec.normalized();
                    vec2d_f64 final_vec = normlaized_vec * 6;

                    e.position.X += final_vec.x * Time.deltaTime;
                    e.position.Y += final_vec.y * Time.deltaTime;

                    LimitedWorld(e.position.X, e.position.Y, e);
                }
            }
        }

        public void MoveGolems()
        {
            foreach (Enemigo e in currentWorld.enemies)
            {
                if (e.enemyType == EnemyType.GOLEM)
                {
                    vec2d_f64 main_pos = new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y);
                    vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                    vec2d_f64 vec = main_pos - enemy_pos;
                    vec2d_f64 normlaized_vec = vec.normalized();
                    vec2d_f64 final_vec = normlaized_vec * 3;

                    e.position.X += final_vec.x * Time.deltaTime;
                    e.position.Y += final_vec.y * Time.deltaTime;

                    LimitedWorld(e.position.X, e.position.Y, e);
                }
            }
        }

        public void MoveMiniGolems()
        {
            foreach (Enemigo e in currentWorld.enemies)
            {
                if (e.enemyType == EnemyType.MINI_GOLEM)
                {
                    vec2d_f64 main_pos = new vec2d_f64(mainCharacter.position.X, mainCharacter.position.Y);
                    vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                    vec2d_f64 vec = main_pos - enemy_pos;
                    vec2d_f64 normlaized_vec = vec.normalized();
                    vec2d_f64 final_vec = normlaized_vec * 8;

                    e.position.X += final_vec.x * Time.deltaTime;
                    e.position.Y += final_vec.y * Time.deltaTime;

                    LimitedWorld(e.position.X, e.position.Y, e);
                }
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

            LimitedObstacle();
            LimitedWorld(mainCharacter.position.X, mainCharacter.position.Y, mainCharacter);
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

        #endregion

        #region MoveTools

        public void SetSequenceRight(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT)
                {
                    EndId = (int)PersonajeStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.DOWN_RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT)
                {
                    EndId = (int)PersonajeStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP_RIGHT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT)
                {
                    EndId = (int)PersonajeStates.STAY_RIGHT
                });

                mainCharacter.position.X += 8 * Time.deltaTime;
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }
        }

        public void SetSequenceLeft(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT)
                {
                    EndId = (int)PersonajeStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.DOWN_LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT)
                {
                    EndId = (int)PersonajeStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP_LEFT)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT)
                {
                    EndId = (int)PersonajeStates.STAY_LEFT
                });

                mainCharacter.position.X -= 8 * Time.deltaTime;
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }

        }

        public void SetSequenceUpOrDown(KeyboardJoystick8.State state)
        {
            if (state == KeyboardJoystick8.State.DOWN)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_FRONT)
                {
                    EndId = (int)PersonajeStates.STAY_FRONT
                });
                mainCharacter.position.Y -= 8 * Time.deltaTime;
            }

            if (state == KeyboardJoystick8.State.UP)
            {
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_BACK)
                {
                    EndId = (int)PersonajeStates.STAY_BACK
                });
                mainCharacter.position.Y += 8 * Time.deltaTime;
            }
            
        }

        public void SetSequenceReleased(KeyboardJoystick8.State lastState)
        {
            if (lastState == KeyboardJoystick8.State.UP)
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.STAY_BACK));

            if (lastState == KeyboardJoystick8.State.DOWN)
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.STAY_FRONT));

            if (lastState == KeyboardJoystick8.State.LEFT || lastState == KeyboardJoystick8.State.DOWN_LEFT || lastState == KeyboardJoystick8.State.UP_LEFT)
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.STAY_LEFT));

            if (lastState == KeyboardJoystick8.State.RIGHT || lastState == KeyboardJoystick8.State.DOWN_RIGHT || lastState == KeyboardJoystick8.State.UP_RIGHT)
                prota?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.STAY_RIGHT));
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

        public void LimitedObstacle()
        {
            foreach (Obstacle obstacle in currentWorld.obstacles)
            {
                if (DoesIntersectPos1WithPos2(mainCharacter.position, 1, obstacle.position, 1))
                {
                    mainCharacter.position.X = (int)mainCharacter.position.X;
                    mainCharacter.position.Y = (int)mainCharacter.position.Y;
                }

            }
        }

        public void SetSequenceDarkWizzard(vec2d_f64 final_vec, vec2d_f64 main_pos, vec2d_f64 enemy_pos)
        {
            if (final_vec.x > 0 && final_vec.x > final_vec.y)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT));
            else if (final_vec.x < 0 && final_vec.x < final_vec.y)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT));
            else if (final_vec.x == 0 && final_vec.y == 0 && main_pos.x > enemy_pos.x)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT));
            else if (final_vec.x == 0 && final_vec.y == 0 && main_pos.x < enemy_pos.x)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT));
            else if (final_vec.y > 0 && final_vec.y > final_vec.x)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_BACK));
            else if (final_vec.y < 0 && final_vec.y < final_vec.x)
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_FRONT));

            if (final_vec.y == 0  && (main_pos.y > enemy_pos.y))
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_BACK));
            else if (final_vec.y == 0 && (main_pos.y < enemy_pos.y))
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_FRONT));
            else if (final_vec.x == 0 && (main_pos.x > enemy_pos.x))
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_RIGHT));
            else if (final_vec.x == 0 && (main_pos.x < enemy_pos.x))
                darkWizzard?.SetSequence(new SpriteSequenceSelector((int)PersonajeStates.MOVE_LEFT));
        }

        #endregion

        #endregion

        #region Renders

        public void SetSpriteToEnemys()
        {
            spriteSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/enemy_movetxt/enemy_movement_set.json", database, typeof(PersonajeStates));
            darkWizzard = new SpriteInstance(spriteSet, (int)PersonajeStates.MOVE_FRONT, 0, -1);
        }
        public void RenderWorld(ICanvas canvas, World world)
        {
            currentWorld.tileWorld?.Draw(canvas, 0.0, 0.0);
        }

        //public void RenderPortal(ICanvas canvas)
        //{
        //    foreach (Portal p in currentWorld.portals)
        //    {
        //        canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 0.0, 1.0));
        //        canvas.Transform.SetTranslation(0, 0);
        //        canvas.DrawRectangle(p.aabb);

        //    }
        //}

        public void RenderHearts(ICanvas canvas)
        {
            double distanceHearts = 0;
            foreach (SpriteInstance sprite in hearts)
            {
                sprite.Draw(canvas, mainCharacter.position.X - 0.65 + distanceHearts, mainCharacter.position.Y + 2, 1, 1);
                distanceHearts += 1.25;
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
            foreach (Enemigo e in currentWorld.enemies)
            {
                if(e.enemyType == EnemyType.DARK_WIZZARD)
                    darkWizzard?.Draw(canvas, e.position.X - Constants.middleSprite, e.position.Y - Constants.middleSprite, 2.0, 2.0);
                else
                {
                    canvas.FillShader.SetColor(new rgba_f64(1.0, 0.0, 0.0, 1.0));
                    canvas.Transform.SetTranslation(e.position.X, e.position.Y);
                    canvas.DrawRectangle(new rect2d_f64(0, 0, 1, 1));
                }
            }
        }

        public void RenderObstacles(ICanvas canvas)
        {
            foreach (Obstacle obstacle in currentWorld.obstacles)
            {
                canvas.FillShader.SetColor(new rgba_f64(0.0, 0.0, 1.0, 1.0));
                canvas.Transform.SetTranslation(obstacle.position.X, obstacle.position.Y);
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
                    if (e.enemyType == EnemyType.DARK_WIZZARD)
                    {
                        vec2d_f64 enemy_pos = new vec2d_f64(e.position.X, e.position.Y);
                        vec2d_f64 vec = main_pos - enemy_pos;
                        vec2d_f64 normlaized_vec = vec.normalized();
                        vec2d_f64 final_vec = normlaized_vec * 7;
                        bullets.Add(new Bala(e.position.X, e.position.Y, final_vec, Shooter.ENEMIE));
                    }
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

                if (bullets.Count > 0)
                {
                    foreach (Obstacle obstacle in currentWorld.obstacles)
                    {
                        if (DoesIntersectPos1WithPos2(bullets[i].position, 1, obstacle.position, 1))
                        {
                            bullets.Remove(bullets[i]);
                            break;
                        }

                    }
                }
            }
        }
        #endregion

        #region ThingsWithCharacters

        public bool DoesIntersectPos1WithPos2(Position pos1, double enemySize, Position pos2, double bulleSize)
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

        public void KillEnemies(List<Enemigo> enemigos, List<Bala> bullets)
        {
            bool RemoveBullet = false;

            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemigos.Count; j++)
                {
                    if (bullets[i].shooter == Shooter.MAIN)
                    {
                        if (DoesIntersectPos1WithPos2(enemigos[j].position, 1.0, bullets[i].position, 0.25))
                        {
                            enemigos[j].vida -= Constants.mainBulletDamage;
                            RemoveBullet = true;
                        }
                        
                        if (enemigos[j].vida <= 0)
                        {
                            if (enemigos[j].enemyType == EnemyType.GOLEM)
                                GenerateMiniGolems();

                            enemigos.Remove(enemigos[j]);
                        }
                    }
                }

                if (RemoveBullet)
                {
                    bullets.RemoveAt(i);
                    RemoveBullet = false;
                }   
            }
        }

        public void HitToProta(List<Enemigo> enemigos, List<Bala> balas, GameDelegateEvent gameDelegate)
        {
            if (protaHitDelay > 1)
            {
                HitEnemieToMainCharacter(enemigos, gameDelegate);
                HitBulletToMainCharacter(balas, gameDelegate);
            }
        }

        public void HitEnemieToMainCharacter(List<Enemigo> enemigos, GameDelegateEvent gameDelegate)
        {
            for (int j = 0; j < enemigos.Count; j++)
            {
                if (DoesIntersectPos1WithPos2(mainCharacter.position, 1.0, enemigos[j].position, 1.0))
                {
                    protaHurtsTimer = 0;
                    mainCharacter.vida -= Constants.enemyBulletDamage;
                    protaHitDelay = 0;
                    ChangeColorWorld();
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
                    if (DoesIntersectPos1WithPos2(mainCharacter.position, 1.0, bullets[i].position, 0.25))
                    {
                        protaHurtsTimer = 0;
                        mainCharacter.vida -= Constants.enemyBulletDamage;
                        protaHitDelay = 0;
                        ChangeColorWorld();
                        bullets.Remove(bullets[i]);
                    }

                    if (mainCharacter.vida <= 0)
                        gameDelegate.window.Close();
                }
            }
        }

        public void GenerateMiniGolems()
        {
            for(int j = 0; j < currentWorld.enemies.Count; j++)
            {
                Enemigo e = currentWorld.enemies[j];

                Position[] positions = new Position[] { new Position(e.position.X - 1, e.position.Y + 1), new Position(e.position.X + 1, e.position.Y + 1), new Position(e.position.X - 1, e.position.Y - 1), new Position(e.position.X + 1, e.position.Y - 1) };

                if (currentWorld.enemies[j].enemyType == EnemyType.GOLEM && currentWorld.enemies[j].vida <= 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        currentWorld.enemies.Add(new Enemigo(positions[i].X, positions[i].Y, 10, EnemyType.MINI_GOLEM));
                    }
                }
            }
        }

        public void ChangeColorWorld()
        {
            if (protaHurtsTimer < Constants.hurtsDelay)
            {
                currentWorld.tileWorld = new TileWorld(20, 20, new aabb2d_f64(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight));
                mapSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/map/map_set_2.json", database, typeof(TileStates));
                layer = SpriteLoaderUtils.LoadLayerFromFile("resources/map/layers/layer_ground0.json", mapSet, typeof(TileStates), typeof(SpriteClass));
                currentWorld.tileWorld.AddLayer(layer, 0, 0);
            }
            else
            {
                currentWorld.tileWorld = new TileWorld(20, 20, new aabb2d_f64(minWorldWidth, minWorldHeight, maxWorldWidth, maxWorldHeight));
                mapSet = SpriteLoaderUtils.LoadSpriteSetFromFile("resources/map/map_set.json", database, typeof(TileStates));
                layer = SpriteLoaderUtils.LoadLayerFromFile("resources/map/layers/layer_ground0.json", mapSet, typeof(TileStates), typeof(SpriteClass));
                currentWorld.tileWorld.AddLayer(layer, 0, 0);
            }
        }

        public void CreateHearts()
        {
            hearts.Clear();
            double vida = mainCharacter.vida / 10.0;

            while (vida >= 1)
            {
                hearts.Add(new SpriteInstance(hudSet, (int)HeartStates.FULL_HEART, 0, -1));
                vida--;
            }
                
            
            if(vida > 0)
                hearts.Add(new SpriteInstance(hudSet, (int)HeartStates.MID_HEART, 0, -1));

        }
        #endregion

        #region ToolsFunctions
        public void UpdateTimers()
        {
            shootMainCharacterDelay += Time.deltaTime;
            shootEnemieDelay += Time.deltaTime;
            protaHitDelay += Time.deltaTime;
            protaHurtsTimer += Time.deltaTime;
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
