using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;
using System.Drawing;

namespace DAM
{
    // Be warned, there is a LOT of stuff here. It might seem complicated, but just take it slow and you'll be fine.
    // OpenGL's initial hurdle is quite large, but once you get past that, things will start making more sense.
    public class Window : GameWindow, ICanvas, IKeyboard, IWindow, IMouse, IAssetManager
    {

        private readonly float[] mRectangleBuffer = new float[64];

        private int mVertexBufferObject;

        private Matrix4 mCamera;

        private ColorEffect? mColorEffect;
        private TextureEffect? mTextureEffect;

        private List<Texture?> mTextureList = new List<Texture?>();

        private KeyboardState? mKeyboardState;
        private MouseState? mMouseState;

        private IGameDelegate mDelegate;

        public float X => mMouseState == null ? 0.0f : mMouseState.X;
        public float Y => mMouseState == null ? 0.0f : mMouseState.Y;
        public float PrevX => mMouseState == null ? 0.0f : mMouseState.PreviousX;
        public float PrevY => mMouseState == null ? 0.0f : mMouseState.PreviousY;
        public float DX => mMouseState == null ? 0.0f : mMouseState.Delta.X;
        public float DY => mMouseState == null ? 0.0f : mMouseState.Delta.Y;
        public float ScrollX => mMouseState == null ? 0.0f : mMouseState.Scroll.X;
        public float ScrollY => mMouseState == null ? 0.0f : mMouseState.Scroll.Y;
        public float ScrollPrevX => mMouseState == null ? 0.0f : mMouseState.PreviousScroll.X;
        public float ScrollPrevY => mMouseState == null ? 0.0f : mMouseState.PreviousScroll.Y;
        public float ScrollDX => mMouseState == null ? 0.0f : mMouseState.ScrollDelta.X;
        public float ScrollDY => mMouseState == null ? 0.0f : mMouseState.ScrollDelta.Y;

        public int Width => Size.X;

        public int Height => Size.Y;

        public bool mIsFullscreen = false;


        public Window(IGameDelegate del, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            mDelegate = del;
        }

        public void ToggleFullscreen()
        {
            if (mIsFullscreen)
            {
                WindowBorder = WindowBorder.Resizable;
                WindowState = WindowState.Normal;
                //ClientSize = new Size(800, 600);
            }
            else
            {
                WindowBorder = WindowBorder.Hidden;
                WindowState = WindowState.Fullscreen;
            }
            mIsFullscreen = !mIsFullscreen;
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad()
        {
            base.OnLoad();
            mVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mRectangleBuffer.Length * sizeof(float), IntPtr.Zero, BufferUsageHint.StaticDraw);
            //GL.BufferStorage(BufferTarget.ArrayBuffer, 2 * 4 * sizeof(float), p, BufferStorageFlags.DynamicStorageBit);

            mColorEffect = new ColorEffect(mVertexBufferObject);
            mTextureEffect = new TextureEffect(mVertexBufferObject);

            mDelegate.OnLoad(this, this);
        }

        private void PrepareBuffer(float[] vertices, int byteCount)
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, byteCount, vertices);
        }

        public void FillConvexPolygon(float[] vertices, float r, float g, float b, float a)
        {
            if (mColorEffect != null)
            {
                PrepareBuffer(vertices, vertices.Length * sizeof(float));
                mColorEffect.Use(ref mCamera, r, g, b, a);
                GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length >> 1);
            }
        }

        public void FillRectangle(float x, float y, float w, float h, float r, float g, float b, float a)
        {
            if (mColorEffect != null)
            {
                mRectangleBuffer[0] = x;
                mRectangleBuffer[1] = y;
                mRectangleBuffer[2] = x;
                mRectangleBuffer[3] = y + h;
                mRectangleBuffer[4] = x + w;
                mRectangleBuffer[5] = y;
                mRectangleBuffer[6] = x + w;
                mRectangleBuffer[7] = y + h;

                PrepareBuffer(mRectangleBuffer, 8 * sizeof(float));
                mColorEffect.Use(ref mCamera, r, g, b, a);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            }
        }

        public void FillRectangle(float x, float y, float w, float h, Image? image, float ix, float iy, float iw, float ih, float r, float g, float b, float a)
        {
            if (mTextureEffect != null)
            {
                Texture? t = null;
                if (image != null && image.IsValid)
                {
                    t = mTextureList[image.InternalCode];
                }
                if (t != null)
                {
                    mRectangleBuffer[0] = x;
                    mRectangleBuffer[1] = y;
                    mRectangleBuffer[2] = ix;
                    mRectangleBuffer[3] = iy;

                    mRectangleBuffer[4] = x;
                    mRectangleBuffer[5] = y + h;
                    mRectangleBuffer[6] = ix;
                    mRectangleBuffer[7] = iy + ih;

                    mRectangleBuffer[8] = x + w;
                    mRectangleBuffer[9] = y;
                    mRectangleBuffer[10] = ix + iw;
                    mRectangleBuffer[11] = iy;

                    mRectangleBuffer[12] = x + w;
                    mRectangleBuffer[13] = y + h;
                    mRectangleBuffer[14] = ix + iw;
                    mRectangleBuffer[15] = iy + ih;

                    PrepareBuffer(mRectangleBuffer, 16 * sizeof(float));

                    mTextureEffect.Use(ref mCamera, t, r, g, b, a);
                    GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
                }
            }
        }


        public void Clear(float r, float g, float b, float a)
        {
            GL.Disable(EnableCap.Blend);
            GL.ClearColor(r, g, b, a);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            Matrix4.CreateOrthographicOffCenter(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f, out mCamera);
            mDelegate.OnDraw(this, this, this);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            mKeyboardState = KeyboardState;
            mMouseState = MouseState;
            mDelegate.OnKeyboard(this, this, this, this);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }

        protected override void OnUnload()
        {
            mDelegate.OnUnload(this, this);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(mVertexBufferObject);

            if (mColorEffect != null)
                mColorEffect.Dispose();
            if (mTextureEffect != null)
                mTextureEffect.Dispose();

            base.OnUnload();
        }

        public bool IsKeyDown(Keys keys)
        {
            return mKeyboardState == null ? false : mKeyboardState.IsKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)keys);
        }

        public bool IsPressed(MouseButton button)
        {
            return mMouseState == null ? false : mMouseState.IsButtonPressed((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        public Image LoadImage(string path)
        {
            var text = Texture.LoadFromFile(path);
            if (text == null)
                return new Image(-1, 0, 0, false);
            for (int i = 0; i < mTextureList.Count; i++)
            {
                if (mTextureList[i] == null)
                {
                    mTextureList[i] = text;
                    return new Image(i, text.Width, text.Height, text.ContainsBlend);
                }
            }
            mTextureList.Add(text);
            return new Image(mTextureList.Count - 1, text.Width, text.Height, text.ContainsBlend);
        }

        public void RemoveImage(Image image)
        {
            if (image.IsValid)
            {
                int index = image.InternalCode;
                mTextureList[index] = null;
            }
        }

        public void SetCamera(float minX, float minY, float maxX, float maxY, bool onlyFocus)
        {
            if (onlyFocus)
            {
                double width = ClientSize.X;
                double height = ClientSize.Y;
                double canvas_ar = width / height;
                double camera_width = maxX - minX;
                double camera_height = maxY - minY;
                double camera_ar = camera_width / camera_height;
                if (canvas_ar > camera_ar)
                {
                    double camera_midx = (minX + maxX) * 0.5;
                    camera_width = (camera_height * canvas_ar) * 0.5;
                    minX = (float)(camera_midx - camera_width);
                    maxX = (float)(camera_midx + camera_width);
                }
                else
                {
                    double camera_midy = (minY + maxY) * 0.5;
                    camera_height = (camera_width / canvas_ar) * 0.5;
                    minY = (float)(camera_midy - camera_height);
                    maxY = (float)(camera_midy + camera_height);
                }
            }
            Matrix4.CreateOrthographicOffCenter(minX, maxX, minY, maxY, -1.0f, 1.0f, out mCamera);
        }
    }
}
