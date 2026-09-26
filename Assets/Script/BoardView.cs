using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    public Transform squereParents;
    public Sprite[] whiteSprites;
    public Sprite[] blackSprites;

    public BoardData Board { get; private set; }

    bool hasSelection;
    Vector2Int selected;

    PieceType type;
    PieceColor color;
    Piece piece;

    Image[] pieceImages = new Image[64];

    public PiecesMovement movement;

    private void Start()
    {
        for (int i = 0; i <64; i++)
        {
            pieceImages[i] = squereParents.GetChild(i).Find("Piece").GetComponent<Image>();
            int index = i;
            squereParents.GetChild(i).GetComponent<Button>()
                .onClick.AddListener(() => movement.OnSquareClicked(index));

        }

        Board = new BoardData();
        Board.SetupStartingPosition();
        Debug.Log("Starting position:\n" + Board.ToDebugString());
        Refresh();

    }

    public void Refresh()
    {
        for (int i = 0; i < 64; i++){
            Piece piece = Board.Get(BoardData.FromIndex(i));
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
