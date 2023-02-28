using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rugby
{
    public class Equipo
    {
        public Equipo(string nombre)
        {
            _nombre = nombre;
        }
        private string _nombre = "";
        private List<Personaje> _personajesequipo = new List<Personaje>();

        public Personaje GetPersonajeAt(int index)
        {
            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                if (i == index)
                {
                    return _personajesequipo[i];
                }
            }

            return null;
        }
        public int GetPersonajesCount()
        {
            return _personajesequipo.Count;
        }
    }
}
