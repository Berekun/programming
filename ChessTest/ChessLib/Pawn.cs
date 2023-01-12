using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    internal class Pawn : FigureInternal
    {

        public Pawn(int x, int y, FigureColor color):base(x, y ,color)
        { 
        
        }
        public override FigureType GetType()
        {
            return FigureType.PAWN;
        }

        public override List<Position> GetAvaliablePosition(IBoard board)
        {
            List<Position> positions = new List<Position>();
            
            if(Y == 1 && board.GetFigureAt(X,Y+2) == null)
                positions.Add(new Position(0,Y+2));
            




            return positions;
        }
    }
}
