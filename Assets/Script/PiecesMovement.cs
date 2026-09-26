using UnityEngine;
using System.Collections.Generic;

public class PiecesMovement : MonoBehaviour
{
    public BoardView boardView;
    public Sprite[] moveBorders;
    List<Vector2Int> legalMoves = new List<Vector2Int>();

    bool hasSelection;
    Vector2Int selected; //Coordinates of the selected piece
    Vector2Int cell;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


    public void OnSquareClicked(int index)
    {
         cell = BoardData.FromIndex(index); //Block where you're going


        if (!hasSelection)
        {
            if (!boardView.Board.IsEmpty(cell))
            {
                hasSelection = true;
                selected = cell;
                legalMoves = PawnMove();
            }
        }
        else
        {
            if (legalMoves.Contains(cell))
            {
                Piece movingPiece = boardView.Board.Get(selected);
                boardView.Board.MovePiece(selected, cell);
                boardView.Refresh();            
                Debug.Log("Selected " + movingPiece.type + ", moved to" + cell);
            }
            hasSelection = false;


        }

    }


    List <Vector2Int> PawnMove()
    {
        
        Piece pawnPiece = boardView.Board.Get(selected);
        int direction = (pawnPiece.color == PieceColor.White) ? 1 : -1;
        List<Vector2Int> moves = new List<Vector2Int>();

        bool OnStartRow = (pawnPiece.color == PieceColor.White && selected.y == 1) 
            || (pawnPiece.color == PieceColor.Black && selected.y == 6);

        Vector2Int oneStep = selected + new Vector2Int(0, direction);
        bool oneStepOpen = boardView.Board.IsEmpty(oneStep);
        if(oneStepOpen)
        {
            moves.Add(oneStep);
        }

        if(OnStartRow && oneStepOpen)
        {
            Vector2Int twoStep = selected + new Vector2Int(0, direction * 2);
            if(boardView.Board.IsEmpty(twoStep))
            {
                moves.Add(twoStep);
            }
        }

        Vector2Int captureLeft = selected + new Vector2Int(-1, direction);
        if (BoardData.InBounds(captureLeft) && !boardView.Board.IsEmpty(captureLeft)) 
        {
            Piece target = boardView.Board.Get(captureLeft);
            if (target.color != pawnPiece.color)
            {
                moves.Add(captureLeft);
            }
        }

        Vector2Int captureRight = selected + new Vector2Int(1, direction);
        if (BoardData.InBounds(captureRight) && !boardView.Board.IsEmpty(captureRight))
        {
            Piece target = boardView.Board.Get(captureRight);
            if (target.color != pawnPiece.color)
            {
                moves.Add(captureRight);
            }
        }

        return moves;
    }



}
