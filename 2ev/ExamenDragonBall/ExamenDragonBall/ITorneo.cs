using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal interface ITorneo
    {
        public void Init();

        public List<string> Execute();

        public void Visit();
    }
}
