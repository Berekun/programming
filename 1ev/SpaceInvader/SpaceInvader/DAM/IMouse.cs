using System;

namespace DAM
{
    public enum MouseButton
    {
        Button1 = 0,
        Button2 = 1,
        Button3 = 2,
        Button4 = 3,
        Button5 = 4,
        Button6 = 5,
        Button7 = 6,
        Button8 = 7,
        Left = 0,
        Right = 1,
        Middle = 2,
        Last = 7
    }

    public interface IMouse
    {
        public float X { get; }
        public float Y { get; }
        public float PrevX { get; }
        public float PrevY { get; }
        public float DX { get; }
        public float DY { get; }
        public float ScrollX { get; }
        public float ScrollY { get; }
        public float ScrollPrevX { get; }
        public float ScrollPrevY { get; }
        public float ScrollDX { get; }
        public float ScrollDY { get; }

        bool IsPressed(MouseButton button);
    }
}

