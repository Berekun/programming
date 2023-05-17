using TinyRpgLib;
using UDK;

namespace TinyRpgApp
{
    public interface IRpgGame
    {
        void CircularPathing(Personaje p);
        void DetectedChangeWorld(double x, double y);
        void EnemieShoot();
        void FillArrayRepresentative();
        void GenerateEnemies();
        void GeneratePathing(List<Enemigo> enemies);
        void HitBulletToMainCharacter(List<Bala> balas, GameDelegateEvent gameDelegate);
        void HitEnemieToMainCharacter(List<Enemigo> enemigos, GameDelegateEvent gameDelegate);
        void HorizontalPathing(Personaje p);
        void KillEnemies(List<Bala> balas, List<Enemigo> enemigos);
        void LimitedWorld(double x, double y, Personaje p);
        void Move(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse);
        void MoveBullets();
        void OnAnimate(GameDelegateEvent gameEvent);
        void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas);
        void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse);
        void OnLoad(GameDelegateEvent gameEvent);
        void OnUnload(GameDelegateEvent gameEvent);
        void RandomMoveNpcs();
        void RemoveBullet(List<Bala> bullets);
        void RenderBullets(ICanvas canvas);
        void RenderEnemies(ICanvas canvas);
        void RenderPortal(ICanvas canvas);
        void RenderProta(ICanvas canvas);
        void RenderWorld(ICanvas canvas, World world);
        int SelectWorld(int id);
        void Shoot(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse);
        void Transition(ICanvas canvas);
        void VerticalPathing(Personaje p);
        int WhatPortalMainCharacterIs();
    }
}