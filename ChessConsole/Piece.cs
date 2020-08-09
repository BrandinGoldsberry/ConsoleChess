using System.Collections.Generic;

namespace ChessConsole
{
    /// <summary>
    /// Represents an abstract chess piece
    /// </summary>
    public abstract class Piece
    {
        /// <summary>
        /// Color of piece
        /// </summary>
        public PlayerColor Color
        {
            private set;
            get;
        }

        /// <summary>
        /// False by default, set to true upon first move
        /// </summary>
        public bool Moved
        {
            protected set;
            get;
        }

        /// <summary>
        /// All the moves possible to make with this piece
        /// </summary>
        public abstract IEnumerable<ChessBoard.Cell> PossibleMoves
        {
            get;
        }

        /// <summary>
        /// All the moves legal to make with this piece. It's a subset of <see cref="PossibleMoves"/>.
        /// See also <seealso cref="ChessBoard.isMoveLegal(Piece, ChessBoard.Cell)"/>.
        /// </summary>
        public List<ChessBoard.Cell> LegalMoves
        {
            private set;
            get;
        }

        public ChessBoard.Cell Parent
        {
            private set;
            get;
        }

        public Piece(PlayerColor color)
        {
            Color = color;
            Moved = false;
            LegalMoves = new List<ChessBoard.Cell>();
        }

        /// <summary>
        /// Called when the piece is first placed or when the piece is replaced after promotion.
        /// Does not recalculate just yet, you have to call <see cref="Recalculate"/> for that.
        /// </summary>
        public void OnPlace(ChessBoard.Cell cell)
        {
            Parent = cell;
        }

        /// <summary>
        /// Called when the piece is moved.
        /// Does not recalculate just yet, you have to call <see cref="Recalculate"/> for that.
        /// </summary>
        public void OnMove(ChessBoard.Cell cell)
        {
            Parent = cell;
            Moved = true;
        }

        /// <summary>
        /// Recalculates the possible moves and updates the hit graph
        /// </summary>
        public abstract void Recalculate();

        /// <summary>
        /// Checks if by moving from the current cell to the desired cell caused the capture target to be uncapturable
        /// </summary>
        /// <param name="CurrentCell">Where the piece stands right now</param>
        /// <param name="DesiredCell">Where the piece is moved</param>
        /// <param name="CaptureTarget">Hit tests this piece</param>
        /// <returns>If blocked is capturable by another piece after moving the piece</returns>
        public abstract bool CheckIfTargetCapturable(ChessBoard.Cell CurrentCell, ChessBoard.Cell DesiredCell, ChessBoard.Cell CaptureTarget);

        public abstract char Char { get; }

        protected virtual bool canHit(ChessBoard.Cell cell)
        {
            return cell != null && cell.Piece != null && cell.Piece.Color != Color;
        }
    }
}
