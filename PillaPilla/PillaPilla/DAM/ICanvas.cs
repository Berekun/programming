
namespace DAM
{
    public interface ICanvas
    {
        void Clear(float r, float g, float b, float a);
        void FillConvexPolygon(float[] vertices, float r, float g, float b, float a);
        void FillRectangle(float x, float y, float w, float h, float r, float g, float b, float a);
    }
}

