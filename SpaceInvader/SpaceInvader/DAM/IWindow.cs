using System;

namespace DAM
{
    public interface IWindow
    {
        int Width { get; }
        int Height { get; }

        void Close();
        void ToggleFullscreen();
    }
}

