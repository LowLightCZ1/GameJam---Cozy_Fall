using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Transform squereParents;
    public Sprite[] whiteSprites;
    public Sprite[] blackSprites;


    BoardData board;
    Image[] pieceImages = new Image[64];

    private void Start()
    {
        for (int i = 0; i <64; i++)
        {
            pieceImages[i] = squereParents.GetChild(i).Find("Piece").GetComponent<Image>();
        }

        board = new BoardData();
        board.SetupStartingPosition();
        Debug.Log("Starting position:\n" + board.ToDebugString());
        Refresh();
    }

    void Refresh()
    {
        for (int i = 0; i < 64; i++){
            Piece piece = board.Get(BoardData.FromIndex(i));
            if (piece.IsEmpty)
            {
                pieceImages[i].gameObject.SetActive(false);
                
            }
            else
            {
                Sprite[] sprites;
                if(piece.color == PieceColor.White)
                {
                    sprites = whiteSprites;
                }
                else
                {
                    sprites = blackSprites;
                }
                int slot = (int)piece.type - 1;
                pieceImages[i].sprite = sprites[slot];
                pieceImages[i].gameObject.SetActive(true);
            }
        }
    }
}
