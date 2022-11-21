using System;

namespace DAM
{
    public interface IAssetManager
    {
        Image LoadImage(string path);
        void RemoveImage(Image image);
    }
}

