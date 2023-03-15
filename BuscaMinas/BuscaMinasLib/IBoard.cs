using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaMinasLib
{
    internal interface IBoard
    {
        void CreateBoard(int width, int height);

        void Init(int x, int y, int bombCount);

        bool IsBombAt(Position pos);

        int GetBombProximity(int x, int y);

        bool IsFlagAt(Position pos);

        void SetFlagAt(int x, int y);

        void DeleteFlagAt(int x, int y);

        //public bool HasWin()
        //{

        //}

        bool IsOpen(Position pos);

        void OpenCell(int x, int y);
    }
}
