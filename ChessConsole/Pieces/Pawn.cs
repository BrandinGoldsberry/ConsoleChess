using System.Collections.Generic;

namespace ChessConsole.Pieces
{
    public class Pawn : Piece
    {
        /// <summary>
        /// Represents the forward direction moves of the pawn
        /// </summary>
        private Direction forward = null;

        /// <summary>
        /// Represents the moveables of the pawn
        /// </summary>
        private ChessBoard.Cell[] moveables = new ChessBoard.Cell[2];

        public Pawn(PlayerColor color)
            : base(color)
        {
            moveables[0] = moveables[1] = null;
        }

        public override IEnumerable<ChessBoard.Cell> PossibleMoves
        {
            get
            {
                foreach (ChessBoard.Cell cell in forward.GetPossibleMoves(false))
                {
                    yield return cell;
                }

                if (canHit(moveables[0]))
                    yield return moveables[0];
                if (canHit(moveables[1]))
                    yield return moveables[1];
            }
        }

        public override void Recalculate()
        {
            //Open forward direction and listen to it
            forward = new Direction(this, 0, (Color == PlayerColor.White) ? 1 : -1, Moved ? 1 : 2, false);

            moveables[0] = Parent.GetRelativeCell(-1, (Color == PlayerColor.White) ? 1 : -1);
            moveables[1] = Parent.GetRelativeCell( 1, (Color == PlayerColor.White) ? 1 : -1);

            if (moveables[0] != null)
                moveables[0].PiecesThatCanMoveHere.Add(this);
            if (moveables[1] != null)
                moveables[1].PiecesThatCanMoveHere.Add(this);
        }

        public override bool CheckIfTargetCapturable(ChessBoard.Cell from, ChessBoard.Cell to, ChessBoard.Cell blocked)
        {
            //The pawn's hits cannot be blocked
            return moveables[0] != blocked && moveables[1] != blocked;
        }

        public override char Char => 'P';

        protected override bool canHit(ChessBoard.Cell cell)
        {
            //Handling en passant over here
            return base.canHit(cell) || (cell != null && cell == cell.Parent.EnPassant);
        }
    }
}
