// Basic definitions: what a piece is. No MonoBehaviour, so it isn't attached to anything.

public enum PieceType { None, Pawn, Knight, Bishop, Rook, Queen, King }

public enum PieceColor { None, White, Black }

public struct Piece
{
    public PieceType type;
    public PieceColor color;

    public Piece(PieceType type, PieceColor color)
    {
        this.type = type;
        this.color = color;
    }

    // A default Piece has type None and color None, so a fresh array is all empty.
    public bool IsEmpty => type == PieceType.None;

    // Used for the debug printout: uppercase = white, lowercase = black.
    public char ToChar()
    {
        char c;
        switch (type)
        {
            case PieceType.Pawn:   c = 'P'; break;
            case PieceType.Knight: c = 'N'; break;
            case PieceType.Bishop: c = 'B'; break;
            case PieceType.Rook:   c = 'R'; break;
            case PieceType.Queen:  c = 'Q'; break;
            case PieceType.King:   c = 'K'; break;
            default: return '.';
        }
        return color == PieceColor.Black ? char.ToLower(c) : c;
    }
}
