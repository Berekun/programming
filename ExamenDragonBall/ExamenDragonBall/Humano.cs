using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal class Humano : Persona
    {
        private double _golpe;
        private double _capdodge;
        private double _capblock;

        public Humano(string name) : base(name, RaceType.HUMAN)
        {
            _golpe = Utils.GetRandom(0.1, 0.8);
            _capdodge = Utils.GetRandom(0.4, 0.6);
            _capblock = Utils.GetRandom(0.7, 0.9);
        }
        public override void Atacar(Persona persona)
        {
            QuitarEnergia(1);
            //if (Utils.GetRandom(0, 1) > _golpe) return;
            persona.RecibirAtaque(0.5, 3);
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
