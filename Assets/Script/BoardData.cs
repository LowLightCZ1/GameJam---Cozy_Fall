using System.Text;
using UnityEngine;

// The logical 8x8 board. Pure data, no UI.
// Coordinates: x = column (0 = a ... 7 = h), y = row (0 = rank 1 ... 7 = rank 8).
// White starts on rows 0-1, black on rows 6-7.
public class BoardData
{
    public const int Size = 8;

    private readonly Piece[,] cells = new Piece[Size, Size];

    // ---------- Coordinate helpers ----------

    public static bool InBounds(Vector2Int p)
    {
        return p.x >= 0 && p.x < Size && p.y >= 0 && p.y < Size;
    }

    // UI square index (0-63, Grid Layout Group: Lower Left + Horizontal) <-> board coordinate
    public static int ToIndex(Vector2Int p) => p.y * Size + p.x;
    public static Vector2Int FromIndex(int index) => new Vector2Int(index % Size, index / Size);

    // ---------- Reading and writing ----------

    public Piece Get(Vector2Int p) => cells[p.x, p.y];

    public void Set(Vector2Int p, Piece piece) => cells[p.x, p.y] = piece;

    public bool IsEmpty(Vector2Int p) => cells[p.x, p.y].IsEmpty;

    public void Clear()
    {
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
                cells[x, y] = default;
    }

    // Moves whatever is on 'from' to 'to'. A piece already on 'to' is captured (overwritten).
    // It does NOT check whether the move is legal; that comes with the movement logic.
    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        cells[to.x, to.y] = cells[from.x, from.y];
        cells[from.x, from.y] = default;
    }

    // ---------- Starting position ----------

    public void SetupStartingPosition()
    {
        Clear();

        PieceType[] backRank =
        {
            PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen,
            PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook
        };

        for (int x = 0; x < Size; x++)
        {
            cells[x, 0] = new Piece(backRank[x], PieceColor.White);
            cells[x, 1] = new Piece(PieceType.Pawn, PieceColor.White);
            cells[x, 6] = new Piece(PieceType.Pawn, PieceColor.Black);
            cells[x, 7] = new Piece(backRank[x], PieceColor.Black);
        }
    }

    // ---------- Debugging ----------

    // Prints the board with rank 8 at the top, like a real board.
    public string ToDebugString()
    {
        var sb = new StringBuilder();
        for (int y = Size - 1; y >= 0; y--)
        {
            sb.Append(y + 1).Append("  ");
            for (int x = 0; x < Size; x++)
                sb.Append(cells[x, y].ToChar()).Append(' ');
            sb.AppendLine();
        }
        sb.AppendLine();
        sb.Append("   a b c d e f g h");
        return sb.ToString();
    }
}
