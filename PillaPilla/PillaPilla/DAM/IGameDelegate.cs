using System;

namespace DAM
{
    public interface IGameDelegate
    {
        void OnLoad();
        void OnDraw(ICanvas canvas);
        void OnKeyboard(IWindow window, IKeyboard keyboard, IMouse mouse);
        void OnUnload();
    }
}

