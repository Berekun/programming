using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal class Namekiano : Persona
    {
        private double _rayo;
        private double _golpe;
        private double _capdodge;
        private double _capblock;

        public Namekiano(string name) : base(name, RaceType.NAMEK)
        {
            _rayo = Utils.GetRandom(0.1, 0.4);
            _golpe = Utils.GetRandom(0.3, 0.8);
            _capdodge = Utils.GetRandom(0.5, 0.7);
            _capblock = Utils.GetRandom(0.3, 0.6);
        }
        public override void Atacar(Persona persona)
        {
            if (Utils.GetRandom(0, 1) <= 0.6 && Utils.GetRandom(0,1) <= _golpe)
            {
                QuitarEnergia(10);
                persona.RecibirAtaque(7, 19);
            }
            else if(Utils.GetRandom(0,1) <= _rayo)
            {
                QuitarEnergia(50);
                persona.RecibirAtaque(20, 100);
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
