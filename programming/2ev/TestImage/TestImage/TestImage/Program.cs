using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using DAM;

namespace TestImage
{
    internal class Program
    {
        //public static int GetFieldOffset(this FieldInfo fi) => GetFieldOffset(fi.FieldHandle);
        public static int GetFieldOffset(RuntimeFieldHandle h) => Marshal.ReadInt32(h.Value + (4 + IntPtr.Size)) & 0xFFFFFF;

        static void Main(string[] args)
        {
            /*
            var r = new Rect2D();
            Type? t = r.GetType();

            var p = t.GetProperties();
            var m = t.GetMembers();
            FieldInfo[] f = t.GetFields();

            var o = GetFieldOffset(f[1].FieldHandle);

            unsafe
            {
                void* ptr = (void*)&r;
                var ptr2 = (Type*)t;
            }
            */


            DAM.Game.Launch(new MyGame());
        }
    }
}