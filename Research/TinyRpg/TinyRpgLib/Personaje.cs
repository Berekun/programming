using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRpgLib
{
    public class Personaje
    {
        public Position position { get; set; }
        public int vida { get; set; } = 0;
        public double exp { get; set; } = 0.0;
        public string texto { get; set; } = "";
        public int pathingRoute { get; set; } = 0;

        private List<Ataque> ataques = new List<Ataque>();

        public Personaje()
        {

        }
        public Personaje(int x, int y)
        {
            position = new Position(x, y);
        }

        public Personaje(int x, int y, int pathingRoute)
        {
            position = new Position(x,y);
            this.pathingRoute = pathingRoute;
        }

        //Sprites Spites de animacions de personajes y de ataques

        //Mana pa las habilidades

        //Critico pa las habilidades

        //Stats basicos (Ataque, prob critico, mana) para poder hacer calculos pa las habilidades
    }
}
