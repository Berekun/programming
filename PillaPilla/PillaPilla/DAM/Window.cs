using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Audio.OpenAL;

namespace DAM
{
    // Be warned, there is a LOT of stuff here. It might seem complicated, but just take it slow and you'll be fine.
    // OpenGL's initial hurdle is quite large, but once you get past that, things will start making more sense.
    public class Window : GameWindow, ICanvas, IKeyboard, IWindow, IMouse
    {
        const string mPixelShader = @"
#version 330
out vec4 outputColor;
uniform vec4 ourColor; 
void main()
{
    outputColor = ourColor;
}";
        const string mVertexShader = @"
#version 330 core
layout(location = 0) in vec3 aPosition;
void main(void)
{
    gl_Position = vec4(aPosition, 1.0);
}";

        private readonly float[] mRectangleBuffer = new float[8];


        // Create the vertices for our triangle. These are listed in normalized device coordinates (NDC)
        // In NDC, (0, 0) is the center of the screen.
        // Negative X coordinates move to the left, positive X move to the right.
        // Negative Y coordinates move to the bottom, positive Y move to the top.
        // OpenGL only supports rendering in 3D, so to create a flat triangle, the Z coordinate will be kept as 0.

        // These are the handles to OpenGL objects. A handle is an integer representing where the object lives on the
        // graphics card. Consider them sort of like a pointer; we can't do anything with them directly, but we can
        // send them to OpenGL functions that need them.

        // What these objects are will be explained in OnLoad.
        private int _vertexBufferObject;

        private int _vertexArrayObject;

        // This class is a wrapper around a shader, which helps us manage it.
        // The shader class's code is in the Common project.
        // What shaders are and what they're used for will be explained later in this tutorial.
        private Shader _shader;

        private KeyboardState mKeyboardState;
        private MouseState mMouseState;

        private IGameDelegate mDelegate;

        public float X => mMouseState.X;
        public float Y => mMouseState.Y;
        public float PrevX => mMouseState.PreviousX;
        public float PrevY => mMouseState.PreviousY;
        public float DX => mMouseState.Delta.X;
        public float DY => mMouseState.Delta.Y;
        public float ScrollX => mMouseState.Scroll.X;
        public float ScrollY => mMouseState.Scroll.Y;
        public float ScrollPrevX => mMouseState.PreviousScroll.X;
        public float ScrollPrevY => mMouseState.PreviousScroll.Y;
        public float ScrollDX => mMouseState.ScrollDelta.X;
        public float ScrollDY => mMouseState.ScrollDelta.Y;

        public Window(IGameDelegate del, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            mDelegate = del;
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad()
        {
            base.OnLoad();
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            float[] p = null;
            GL.BufferData(BufferTarget.ArrayBuffer, 2 * 4 * sizeof(float), p, BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            //_shader = Shader.CreateFromFiles("Shaders/shader.vert", "Shaders/shader.frag");
            _shader = Shader.CreateFromStrings(mVertexShader, mPixelShader);

            mDelegate.OnLoad();
        }

        private void PrepareShader(float[] vertices, float r, float g, float b, float a)
        {
            // Bind the shader
            _shader.Use();
            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, r, g, b, a);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);
            //GL.BufferSubData(BufferTarget.ArrayBuffer, System.IntPtr.Zero, vertices.Length * sizeof(float), vertices);

            GL.BindVertexArray(_vertexArrayObject);
        }

        public void FillConvexPolygon(float[] vertices, float r, float g, float b, float a)
        {
            PrepareShader(vertices, r, g, b, a);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length >> 1);
        }

        public void FillRectangle(float x, float y, float w, float h, float r, float g, float b, float a)
        {
            mRectangleBuffer[0] = x;
            mRectangleBuffer[1] = y;
            mRectangleBuffer[2] = x;
            mRectangleBuffer[3] = y + h;
            mRectangleBuffer[4] = x + w;
            mRectangleBuffer[5] = y;
            mRectangleBuffer[6] = x + w;
            mRectangleBuffer[7] = y + h;

            PrepareShader(mRectangleBuffer, r, g, b, a);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }

        public void Clear(float r, float g, float b, float a)
        {
            GL.ClearColor(r, g, b, a);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            mDelegate.OnDraw(this);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            mKeyboardState = KeyboardState;
            mMouseState = MouseState;

            mDelegate.OnKeyboard(this, this, this);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // When the window gets resized, we have to call GL.Viewport to resize OpenGL's viewport to match the new size.
            // If we don't, the NDC will no longer be correct.
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        // Now, for cleanup.
        // You should generally not do cleanup of opengl resources when exiting an application
        // as that is handled by the driver and operating system when the application exits.
        // 
        // There are reasons to delete opengl resources but exiting the application is not one of them.
        // This is provided here as a reference on how resoruce cleanup is done in opengl but
        // should not be done when exiting the application.
        //
        // Places where cleanup is appropriate would be to delete textures that are no
        // longer used for whatever reason (e.g. a new scene is loaded that doesn't use a texture).
        // This would free up video ram (VRAM) that can be used for new textures.
        //
        // The comming chapters will not have this code.
        protected override void OnUnload()
        {
            mDelegate.OnUnload();
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);

            base.OnUnload();
        }

        public bool IsKeyDown(Keys keys)
        {
            return mKeyboardState.IsKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)keys);
        }

        public bool IsPressed(MouseButton button)
        {
            return mMouseState.IsButtonPressed((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }
    }
}
