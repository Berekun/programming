using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MontyHall
{
    enum Door
    {
        CAR,CABRA
    }

    enum GameResult
    {
        WIN, LOOSE, NOTGAME
    }

    internal class Show
    {
        List<Door> _doors = new List<Door>();

        public void Init(int numberOfDoors)
        {
            _doors.Add(Door.CAR);

            for (int i = 0; i <= numberOfDoors; i++)
            {
                _doors.Add(Door.CABRA);
            }
        }

        public int Execute(int numberOfGames)
        {
            int Wins = 0;
            GameResult result;

            if (numberOfGames < 1)
                return -1;

            for (int i = 0; i < numberOfGames; i++)
            {
                Init(15);
                result = DoAGame(true);
                if (result == GameResult.WIN)
                    Wins++;
            }

            return Wins;
        }

        public GameResult DoAGame(bool wantsChange)
        {
            int choose = Utils.GetIntRandom(0, _doors.Count - 1);

                if (_doors[choose] == Door.CAR)
                {
                    for(int i = 0; _doors.Count > 2; i++)
                        _doors.RemoveAt(2);

                    if (wantsChange)
                        return GameResult.LOOSE;
                    else
                        return GameResult.WIN;
                }
                else
                {
                    for (int i = 0; _doors.Count > 2; i++)
                        _doors.RemoveAt(2);

                    if (wantsChange)
                        return GameResult.WIN;
                    else
                        return GameResult.LOOSE;
                }
        }
    }
}
