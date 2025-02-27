using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal class Saiyan : Persona
    {
        private double _rayo;
        private double _golpe = 5;
        private double _capdodge;
        private double _capblock;

        public Saiyan(string name) : base(name, RaceType.SAIYAN)
        {
            _rayo = Utils.GetRandom(0.3, 0.6);
            _golpe = Utils.GetRandom(0.1, 0.8);
            _capdodge = Utils.GetRandom(0.2, 0.4);
            _capblock = Utils.GetRandom(0.4, 0.9);
        }

        public Saiyan(string name, RaceType race) : base(name, race)
        {

        }
        public override void Atacar(Persona persona)
        {
            double randomAttack = Utils.GetRandom(0, 1);
            if (randomAttack >= 0.7 && Utils.GetRandom(0, 1) < _golpe)
            {
                QuitarEnergia(5);
                persona.RecibirAtaque(2, 7);
                
            }
            else if (Utils.GetRandom(0, 1) < _golpe)
            {
                QuitarEnergia(100);
                persona.RecibirAtaque(25, 300);  
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
