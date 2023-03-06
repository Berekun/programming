using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace DAM
{
    public class ColorEffect : Effect
    {
        int uColor;
        int uCamera;


        public static string GenPixelShader()
        {
            return @"
            #version 330
            out vec4 outputColor;
            uniform vec4 uColor; 
            void main()
            {
                outputColor = uColor;
            }";
        }

        public static string GenVertexShader()
        {
            return @"
            #version 330 core
            layout(location = 0) in vec3 aPosition;
            uniform mat4 uCamera; 
            void main(void)
            {
                gl_Position = vec4(aPosition, 1.0) * uCamera;
            }";
        }

        public ColorEffect(int vertexBuffer)
        {
            string pixel_shader = GenPixelShader();
            string vertex_shader = GenVertexShader();
            mShader = Shader.CreateFromStrings(vertex_shader, pixel_shader);

            mShader.Use();
            uCamera = GL.GetUniformLocation(mShader.Handle, "uCamera");
            aPosition = mShader.GetAttribLocation("aPosition");
            uColor = GL.GetUniformLocation(mShader.Handle, "uColor");

            ConfigVertexView(new VertexConfig()
            {
                entry = new VertexEntry[]
                {
                    new VertexEntry()
                    {
                        Buffer = vertexBuffer,
                        ChannelType = VertexAttribPointerType.Float,
                        ChannelCount = 2,
                        Location = aPosition
                    }
                }
            });

        }

        internal void Use(ref Matrix4 cameraMatrix, float r, float g, float b, float a)
        {
            if (mShader == null)
                return;

            mShader.Use();
            GL.Uniform4(uColor, r, g, b, a);
            GL.UniformMatrix4(uCamera, true, ref cameraMatrix);
            if (a < 1.0)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.One, BlendingFactorDest.Zero);
            }
            else
            {
                GL.Disable(EnableCap.Blend);
            }
            GL.BindVertexArray(mVertexArrayObject);
        }
    }
}

