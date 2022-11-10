using System;

namespace DAM
{
    public class Image
    {
        private int code, width, height;
        private bool shouldBlend;

        public int InternalCode { get { return code; } }
        public bool IsValid { get { return code >= 0; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool ShouldBlend { get { return shouldBlend; } }

        public Image(int hash, int width, int height, bool shouldBlend)
        {
            this.code = hash;
            this.width = width;
            this.height = height;
            this.shouldBlend = shouldBlend;
        }
    }
}

