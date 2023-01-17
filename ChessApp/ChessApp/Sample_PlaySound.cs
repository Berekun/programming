using System;
using DAM;

namespace TestDAMNuget
{
    public class Sample_PlaySound : IGameDelegate
    {
        DAM.Sound? sound;
        DAM.IPlayer? player;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyPressed(Keys.S))
                gameEvent.audioThread.AcquirePlayer()?.SetSound(sound, false, true).Play();

            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();

            if (keyboard.IsKeyPressed(Keys.P) && player != null)
                player.SetPaused(!player.IsPaused);
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            sound = gameEvent.audioContext.LoadFromFile("resources/mixkit.wav", SoundLoadMode.FULL);
            player = gameEvent.audioThread.AcquirePlayer();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}

