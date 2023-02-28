using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDragonBall
{
    internal abstract class Persona
    {
        private string _name;
        public enum RaceType
        {
            HUMAN, SAIYAN, SUPERSAIYAN, NAMEK
        }
        private RaceType _race;
        private double _energy;
        private double _dodgedesire;
        public string Name => _name;
        public double Energy => _energy;
        public RaceType Race => _race;
        public double DodgeDesire => _dodgedesire;

        public Persona(string name, RaceType race)
        {
            _name = name;
            _race = race;
            _energy = Utils.GetRandom(1000, 2000);
            _dodgedesire = Utils.GetRandom(0.1, 0.9);
        }
        public void QuitarEnergia(double energy)
        {
            _energy = _energy - energy;
        }

        public abstract void Atacar(Persona persona);

        public abstract double ObtenerCapacidadDeEsquiva();

        public abstract double ObtenerCapacidadDeParada();

        public bool QuiereEsquivar()
        {
            double random = Utils.GetRandom(0,1);
            if (random >= _dodgedesire) return false;
            return true;
        }

        public void RecibirAtaque(double blockdmg, double maxdmg)
        {
            if (QuiereEsquivar() && ObtenerCapacidadDeEsquiva() > Utils.GetRandom(0, 1)) return;
            if (!QuiereEsquivar() && ObtenerCapacidadDeParada() > Utils.GetRandom(0, 1))
                QuitarEnergia(blockdmg);
            else
                QuitarEnergia(maxdmg);
        }
    }
}
