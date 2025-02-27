using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDK;

namespace TinyRpgLib
{
    public class Portal
    {
        public int id { get; set; }

        public rect2d_f64 aabb  = new rect2d_f64();

        public Portal()
        {

        }
        public Portal(int id, rect2d_f64 aabb)
        {
            this.id = id;
            this.aabb = aabb;
        }
    }
}
