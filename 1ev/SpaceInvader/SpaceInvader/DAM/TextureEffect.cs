using System;
using System.Xml.Linq;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace DAM
{
    public class TextureEffect : Effect
    {
        int uColor;
        int uTexture0;
        int aTexCoords;
        int uCamera;

        public static string GenPixelShader()
        {
            return @"
            #version 330
            out vec4 outputColor;
            in vec2 texCoord;
            uniform sampler2D uTexture0;
            uniform vec4 uColor; 
            void main()
            {
                outputColor = texture(uTexture0, texCoord) * uColor;
            }";
        }


        public static string GenVertexShader()
        {
            return @"
            #version 330 core
            uniform mat4 uCamera; 
            layout(location = 0) in vec3 aPosition;
            layout(location = 1) in vec2 aTexCoords;
            out vec2 texCoord;
            void main(void)
            {
                gl_Position = vec4(aPosition, 1.0) * uCamera;
                texCoord = aTexCoords;
            }";
        }


        public TextureEffect(int vertexBuffer)
        {
            string pixel_shader = GenPixelShader();
            string vertex_shader = GenVertexShader();
            mShader = Shader.CreateFromStrings(vertex_shader, pixel_shader);

            mShader.Use();

            uCamera = GL.GetUniformLocation(mShader.Handle, "uCamera");
            uColor = GL.GetUniformLocation(mShader.Handle, "uColor");
            uTexture0 = GL.GetUniformLocation(mShader.Handle, "uTexture0");
            aPosition = mShader.GetAttribLocation("aPosition");
            aTexCoords = mShader.GetAttribLocation("aTexCoords");

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
                    },
                    new VertexEntry()
                    {
                        Buffer = vertexBuffer,
                        ChannelType = VertexAttribPointerType.Float,
                        ChannelCount = 2,
                        Location = aTexCoords
                    }
                }
            });
        }

        internal void Use(ref Matrix4 cameraMatrix, Texture texture, float r, float g, float b, float a)
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
            texture.Use(TextureUnit.Texture0);
            GL.BindVertexArray(mVertexArrayObject);
        }
    }
}

