using System;
using DAM;

namespace ChessApp
{
    public class MyChessGame : IGameDelegate
    {
        public void OnDraw(GameDelegateEvent gameEvent, ICanvas canvas)
        {
        }

        public void OnKeyboard(GameDelegateEvent gameEvent, IKeyboard keyboard, IMouse mouse)
        {
            if (keyboard.IsKeyDown(Keys.Escape))
                gameEvent.window.Close();
        }

        public void OnLoad(GameDelegateEvent gameEvent)
        {
        }

        public void OnUnload(GameDelegateEvent gameEvent)
        {
        }
    }
}

