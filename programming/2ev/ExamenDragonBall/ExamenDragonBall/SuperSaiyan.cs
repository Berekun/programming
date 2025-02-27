using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal class SuperSaiyan : Saiyan
    {
        private double _rayo;
        private double _golpe;
        private double _capdodge;
        private double _capblock;

        public SuperSaiyan(string name) : base(name, RaceType.SUPERSAIYAN)
        {
            _rayo = Utils.GetRandom(0.3, 0.6);
            _golpe = Utils.GetRandom(0.1, 0.8);
            _capdodge = Utils.GetRandom(0.2, 0.4);
            _capblock = Utils.GetRandom(0.4, 0.9);
        }
        public override void Atacar(Persona persona)
        {
            double action = (int)Utils.GetRandom(1, 4);
            while (action > 0)
            {
                
                if (Utils.GetRandom(0, 1) <= 0.7 && Utils.GetRandom(0, 1) < _golpe)
                {
                    QuitarEnergia(5);
                    persona.RecibirAtaque(4, 14);
                }
                else if (Utils.GetRandom(0, 1) < _golpe)
                {
                    QuitarEnergia(100);
                    persona.RecibirAtaque(50, 600);
                }
                action--;
            }        
        }

        public override double ObtenerCapacidadDeEsquiva()
        {
            return _capdodge;
        }

        public override double ObtenerCapacidadDeParada()
        {
            return _capblock;
        }
    }

}

