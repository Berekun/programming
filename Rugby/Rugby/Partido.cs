using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rugby.Personaje;

namespace Rugby
{
    public delegate void Visitor(Personaje personaje);
    public class Partido
    {
        private List<Personaje> _personajes = new List<Personaje>();
        private List<Equipo> _equipos = new List<Equipo>();
        Pelota pelota = new Pelota();

        public Personaje GetPersonajeAt(int index)
        {
            for(int i = 0; i < GetPersonajesCount(); i++)
            {
                if (i == index)
                {
                    return _personajes[i];
                }
            }

            return null;
        }
        public int GetPersonajesCount()
        {
            return _personajes.Count;
        }
        public void Iniciar()
        {
            CreateTeams();
            CreatePersonajesAbajo();
            CreatePersonajesArriba();
            CreateDementors();
        }

        public void Ejecutar()
        {

        }

        public void Visit(Visitor visit)
        {
            for (int i = 0; i < GetPersonajesCount(); i++)
                visit(GetPersonajeAt(i));

        }

        public bool IsPersonajeAt(int x, int y)
        {
            if (x > 10 || x < 0)
                return true;
            if (y > 20 || y < 0)
                return true;

            Personaje personaje;

            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                personaje = GetPersonajeAt(i);
                if (x == GetPersonajeAt(i).X && y == GetPersonajeAt(i).Y)
                    return true;
            }
            return false;

        }

        public Personaje? GetPersonajeAtPosition(int x, int y)
        {
            if (x > 10 || x < 0)
                return null;
            if (y > 20 || y < 0)
                return null;

            Personaje personaje;
                
            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                personaje = GetPersonajeAt(i);
                if (x == personaje.X && y == personaje.Y && personaje is Jugador)
                    return GetPersonajeAt(i);
            }
            return null;
        }

        public Position GetPositionOfBallWithoutPlayer(int x, int y)
        {
            Position positionball = new Position();

            if (x > 10 || x < 0)
                return positionball;
            if (y > 20 || y < 0)
                return positionball;

            Personaje personaje;

            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                personaje = GetPersonajeAt(i);
                if (pelota.X == personaje.X && pelota.Y == personaje.Y)
                    return positionball;
                if(pelota.X == x && pelota.Y == y)
                {
                    return positionball = pelota.Position;
                }
            }
            return positionball;
        }

        public void CreateTeams()
        {
            _equipos.Add(new Equipo("rojo"));
            _equipos.Add(new Equipo("azul"));
        }

        public void CreatePersonajesAbajo()
        {
            for (int i = 3; i < 7; i++)
            {
                _personajes.Add(new Delantero(_equipos[0], i, 1));
            }

            for (int i = 3; i < 7; i++)
            {
                _personajes.Add(new Defensa(_equipos[0], i, 0));
            }

            for (int i = 2; i < 8; i += 5)
            {
                _personajes.Add(new DefensaEspecial(_equipos[0], i, 0));
            }
        }

        public void CreatePersonajesArriba()
        {
            for (int i = 3; i < 7; i++)
            {
                _personajes.Add(new Delantero(_equipos[0], i, 19));
            }

            for (int i = 3; i < 7; i++)
            {
                _personajes.Add(new Defensa(_equipos[0], i, 18));
            }

            for (int i = 2; i < 8; i += 5)
            {
                _personajes.Add(new DefensaEspecial(_equipos[0], i, 19));
            }
        }

        public void CreateDementors()
        {
            for (int i = 0; i < 4; i++)
            {
                _personajes.Add(new Dementor((int)Utils.GetRandomDouble(0, 9), (int)Utils.GetRandomDouble(0, 19)));
            }
        }

        public void Move(Position oldPosition,Position newPosition)
        {
            oldPosition.x = newPosition.x;
            oldPosition.y = newPosition.y;
        }
    }
}
