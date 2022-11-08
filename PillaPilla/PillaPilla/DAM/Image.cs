using System;

namespace DAM
{
    public class Image
    {
        private int hash;
        public int Hash { get { return hash; } }
        public bool IsValid { get { return hash >= 0; } }
        public Image(int hash) { this.hash = hash; }
    }
}

