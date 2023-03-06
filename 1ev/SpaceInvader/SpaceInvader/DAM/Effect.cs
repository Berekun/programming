using System;
using OpenTK.Graphics.OpenGL4;

namespace DAM
{
    public class Effect : IDisposable
    {
        protected struct VertexEntry
        {
            public VertexAttribPointerType ChannelType { get; set; }
            public int Buffer { get; set; }
            public int ChannelCount { get; set; }
            public bool Normalized { get; set; }
            public int Offset { get; set; }
            public int Location { get; set; }

            public int GetStride()
            {
                int ret = 0;
                switch (ChannelType)
                {
                    case VertexAttribPointerType.Float:
                        ret = 4;
                        break;
                }
                ret *= ChannelCount;

                return ret;
            }
        }
        protected struct VertexConfig
        {
            public int Size { get; set; }
            public int Stride { get; set; }
            public VertexEntry[] entry { get; set; }

            public int GetStride()
            {
                if (Stride > 0)
                    return Stride;
                int ret = 0;
                foreach (var e in entry)
                    ret += e.GetStride();
                return ret;
            }

            public int GetOffset(int entryIndex)
            {
                int ret = 0;
                for (int i = 0; i < entryIndex; i++)
                {
                    ret += entry[i].GetStride();
                }
                return ret;
            }
        }

        protected Shader? mShader;
        protected int mVertexArrayObject = 0;
        protected int aPosition;

        public Shader? Shader { get { return mShader; } }

        public Effect()
        {
        }

        protected void ConfigVertexView(VertexConfig config)
        {
            int stride = config.GetStride();

            mVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(mVertexArrayObject);


            for (int i = 0; i < config.entry.Length; i++)
            {
                var e = config.entry[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, e.Buffer);
                GL.VertexAttribPointer(e.Location, e.ChannelCount, e.ChannelType, e.Normalized, stride, config.GetOffset(i));
                GL.EnableVertexAttribArray(e.Location);
            }
        }

        public virtual void Dispose()
        {
            if (mVertexArrayObject != 0)
                GL.DeleteVertexArray(mVertexArrayObject);
            if (mShader != null)
                GL.DeleteProgram(mShader.Handle);
        }
    }
}

