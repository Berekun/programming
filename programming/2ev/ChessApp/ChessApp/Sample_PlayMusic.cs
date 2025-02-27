using System;
using DAM;

/*namespace TestDAMNuget
{
    public class Sample_PlayMusic : IGameDelegate
    {
        DAM.Sound? music;
        DAM.IPlayer? player;

        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {

        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)pp
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();

            if (keyboard.IsKeyPressed(Keys.P) && player != null)
                player.SetPaused(!player.IsPaused);
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
            music = gameEvent.audioContext.LoadFromFile("resources/sample.ogg", SoundLoadMode.STREAM);

            player = gameEvent.audioThread.AcquirePlayer();
            player?.SetSound(music, true, false).Play();
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}*/

