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

        // Javi: Esta función no la puedo perdonar, ..., ibas camino del 9,5, ...., por favor rectifica esta función
        // porque es para poner un cero en el examen
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

        public bool IsScoreGoal()
        {
            foreach (Personaje personaje in _personajes)
            {
                if (personaje is Jugador)
                {
                    Jugador jugador = (Jugador)personaje;

                    // Javi: Estos ifs, hay que mejorarlos
                    if (jugador.Equipo.Nombre == "rojo" && jugador.Y == 19)
                    {
                        if (IsThisPlayerHavingBall(jugador) && jugador is Delantero)
                        {
                            _equipos[0].AddScore(10);
                            return true;
                        }
                        else if (IsThisPlayerHavingBall(jugador) && (jugador is Defensa || jugador is DefensaEspecial))
                        {
                            _equipos[0].AddScore(3);
                            return true;
                        }
                    }
                    else if (jugador.Equipo.Nombre == "azul" && jugador.Y == 0)
                    {
                        if (IsThisPlayerHavingBall(jugador) && jugador is Delantero)
                        {
                            _equipos[1].AddScore(10);
                            return true;
                        }
                        else if (IsThisPlayerHavingBall(jugador) && (jugador is Defensa || jugador is DefensaEspecial))
                        {
                            _equipos[1].AddScore(3);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void ResetWorld()
        {
            // Javi: Este if mejor fuera. Coméntame esto en clase
            if (IsScoreGoal())
            {
                foreach (Personaje personaje in _personajes)
                {
                    MovePersonaje(personaje, personaje.PositionInicial);
                }

            }
        }

        public void Ejecutar()
        {
            Iniciar();
            for (int i = 0; i <= 1000; i++)
            {
                foreach(Personaje personaje in _personajes)
                {
                    personaje.Ejecutar(pelota, this);
                    IsScoreGoal();
                    ResetWorld();
                }
            }
        }

        public void Visit(Visitor visit)
        {
            for (int i = 0; i < GetPersonajesCount(); i++)
                visit(GetPersonajeAt(i));
        }

        public bool IsThisPlayerHavingBall(Personaje personaje)
        {
            if (personaje.X == pelota.X && personaje.Y == pelota.Y)
                return true;
            return false;

        }

        public bool IsPersonajeAt(int x, int y)
        {
            if (x >= 10 || x < 0)
                return true;
            if (y >= 20 || y < 0)
                return true;

            Personaje personaje;

            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                personaje = GetPersonajeAt(i);
                if (x == personaje.X && y == personaje.Y)
                    return true;
            }
            return false;

        }

        public Personaje? GetJugadorAtPosition(int x, int y)
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
                    // Javi: Que!?!?!?!?
                    return positionball = pelota.Position;
                }
            }
            return positionball;
        }

        public Position GetPositionPlayerWhitBall(int x, int y)
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
                // Javi: Esto que es!??!?!!?!?
                if (pelota.X == personaje.X && pelota.Y == personaje.Y)
                    return positionball = pelota.Position;
            }

            return positionball;
        }

        public Jugador WhatPlayerIsNear(Jugador jugador)
        {
            Jugador personaje;
            int minX = 0, minY = 0, maxX, maxY;
            Jugador jugadornear = jugador;

            for (int i = 0; i < GetPersonajesCount(); i++)
            {
                if (GetPersonajeAt(i) is Jugador)
                {
                    personaje = (Jugador)GetPersonajeAt(i);
                    maxX = Math.Abs(personaje.X - jugador.X);
                    maxY = Math.Abs(personaje.Y - jugador.Y);

                    if (personaje.Equipo != jugador.Equipo && maxX < minX && maxY < minY)
                    {
                        minX = maxX;
                        minY = maxY;
                        jugadornear = personaje;
                    }
                }
            }
            return jugadornear;
        }

        public void AproxToPlayer(Jugador jugador)
        {
            int distX = WhatPlayerIsNear(jugador).X - jugador.X;
            int distY = WhatPlayerIsNear(jugador).Y - jugador.Y;

            int compared = Utils.comp(Math.Abs(distX), Math.Abs(distY));

            if (compared == 0)
            {
                if (distX < 0)
                    MovePersonaje(jugador, new Position(jugador.X - 1, jugador.Y));
                MovePersonaje(jugador, new Position(jugador.X + 1, jugador.Y));
            }
            else
            {
                if (distY < 0)
                    MovePersonaje(jugador, new Position(jugador.X, jugador.Y - 1));
                MovePersonaje(jugador, new Position(jugador.X, jugador.Y + 1));
            }
        }

        public void AproxToBall(Jugador jugador)
        {
            int distX = pelota.X - jugador.X;
            int distY = pelota.Y - jugador.Y;

            int compared = Utils.comp(Math.Abs(distX), Math.Abs(distY));

            if (compared == 0)
            {
                if (distX < 0)
                    MovePersonaje(jugador, new Position(jugador.X - 1, jugador.Y));
                else
                    MovePersonaje(jugador, new Position(jugador.X + 1, jugador.Y));
            }
            else
            {
                if (distY < 0)
                    MovePersonaje(jugador, new Position(jugador.X, jugador.Y - 1));
                else
                    MovePersonaje(jugador, new Position(jugador.X, jugador.Y + 1));
            }
        }

        public Jugador GetRandomPersonaje3x3(Partido partido, Personaje personaje)
        {
            List<Jugador> list = new List<Jugador>();

            for (int i = 1; i < -2; i--)
            {
                for (int j = -1; j < 2; j++)
                    if (partido.GetJugadorAtPosition(personaje.X + j, personaje.Y + i) != null)
                        list.Add((Jugador)partido.GetJugadorAtPosition(personaje.X + j, personaje.Y + i));
            }

            if (list.Count == 0)
                return null;
            return list[Utils.GetRandomInt(0, list.Count)];
        }

        public Position GetRandomPosition3x3(Partido partido, Personaje personaje)
        {
            List<Position> list = new List<Position>();

            for (int i = 1; i > -2; i--)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!partido.IsPersonajeAt(personaje.X + j, personaje.Y + i))
                        list.Add(new Position(personaje.X + j, personaje.Y + i));
                }
            }

            if (list.Count == 0)
                return new Position(personaje.X, personaje.Y);
            return list[(int)Utils.GetRandomDouble(0, list.Count)];
        }

        public Jugador MostNearPlayer5x5(Personaje personaje)
        {
            List<Jugador> list = new List<Jugador>();
            Jugador newjugador = null;

            for (int i = 2; i > -3; i--)
            {
                for (int j = -2; j < 3; j++)
                {
                    if (GetJugadorAtPosition(personaje.X - j, personaje.Y - i) is Jugador)
                    {
                        newjugador = (Jugador)GetJugadorAtPosition(personaje.X - j, personaje.Y - i);

                        if (personaje.X - newjugador.X == 2 && personaje.Y - newjugador.Y <= 2 || personaje.Y - newjugador.Y == 2 && personaje.X - newjugador.X <= 2)
                        {
                            int distance = int.MaxValue;
                            if (distance > Math.Abs((personaje.X - newjugador.X) + (personaje.Y - newjugador.Y)))
                            {
                                distance = Math.Abs((personaje.X - newjugador.X) + (personaje.Y - newjugador.Y));
                                list.Add(newjugador);
                            }
                        }
                    }
                }
            }

            if (list.Count == 0)
                return null;
            return list[list.Count - 1];
        }

        public int GetScore(Jugador jugador)
        {
            int scoreup = 0;
            int scoremid = 0;
            int scoredown = 0;

            for (int i = -1; i < 2; i++)
            {
                if (!IsPersonajeAt(jugador.X + i, jugador.Y + 1))
                    scoreup += 2;
            }
            for (int i = -1; i < 2; i++)
            {
                if (!IsPersonajeAt(jugador.X + i, jugador.Y))
                    scoremid++;
            }
            for (int i = -1; i < 2; i++)
            {
                if (!IsPersonajeAt(jugador.X + i, jugador.Y - 1))
                    scoredown++;
            }

            if (scoreup > scoremid && scoreup > scoredown)
                return 0;
            else if (scoremid > scoreup && scoremid > scoredown)
                return 1;
            else if (scoredown > scoreup && scoredown > scoremid)
                return 2;
            else if (scoredown > 0)
                return 2;
            else if (scoremid > 0)
                return 1;
            else
                return -1;
                
        }

        public Position GetPositionAt(int x, int y)
        {
            if (!IsPersonajeAt(x, y))
                return new Position(x, y);
            return new Position(-100,-100);
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
                _personajes.Add(new Delantero(_equipos[1], i, 18));
            }

            for (int i = 3; i < 7; i++)
            {
                _personajes.Add(new Defensa(_equipos[1], i, 19));
            }

            for (int i = 2; i < 8; i += 5)
            {
                _personajes.Add(new DefensaEspecial(_equipos[1], i, 19));
            }
        }

        public void CreateDementors()
        {
            for (int i = 0; i < 4; i++)
            {
                _personajes.Add(new Dementor((int)Utils.GetRandomDouble(0, 9), (int)Utils.GetRandomDouble(0, 19)));
            }
        }

        public void MovePersonaje(Personaje personaje,Position newPosition)
        {
            if(newPosition.x < 0 || newPosition.x > 9)
                return;
            if (newPosition.y < 0 || newPosition.y > 19)
                return;

            personaje.ChangePosition(newPosition);
        }

        public void MoveBall(Position newPosition)
        {
            if (newPosition.x < 0 || newPosition.x > 9)
                return;
            if (newPosition.y < 0 || newPosition.y > 19)
                return;

            pelota.ChangePosition(newPosition);
        }
    }
}
