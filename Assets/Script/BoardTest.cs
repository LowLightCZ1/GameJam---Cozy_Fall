using UnityEngine;

// Temporary test script. Attach to any GameObject in the scene, press Play, read the Console.
public class BoardTest : MonoBehaviour
{
    void Start()
    {
        var board = new BoardData();
        board.SetupStartingPosition();
        Debug.Log("Starting position:\n" + board.ToDebugString());

        // Try a move: e2 -> e4  (e = column 4, rank 2 = row 1, rank 4 = row 3)
        board.MovePiece(new Vector2Int(4, 1), new Vector2Int(4, 3));
        Debug.Log("After e2-e4:\n" + board.ToDebugString());
    }
}
