using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public class Dementor : Personaje
    {
        public Dementor(int x, int y) : base(x, y)
        {
        }

        public override void Ejecutar(Pelota pelota, Partido partido)
        {
            throw new NotImplementedException();
        }
    }
}
