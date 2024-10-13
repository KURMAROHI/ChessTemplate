using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : ChessPiece, IPointerDownHandler
{

    Vector2Int RowColumnDetails;
    Chess.Scripts.Core.ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    void Start()
    {
        chessPlayerPlacementHandler = transform.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        // Debug.Log("==>Row Column|" + RowColumnDetails);
    }



    void KnightpossibleMoves()
    {
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        List<Vector2Int> possibleMovesPosition = new List<Vector2Int>();
        Debug.Log("Calling possible moves");
        Vector2Int[] moveOffsets = {
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, 2), new Vector2Int(1, -2),
            new Vector2Int(-1, 2), new Vector2Int(-1, -2)
        };

        foreach (var offset in moveOffsets)
        {
            Vector2Int newPosition = RowColumnDetails + offset;
            if (ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType))
            {
                possibleMovesPosition.Add(newPosition);
            }
        }

        foreach (Vector2Int newPosition in possibleMovesPosition)
        {
            ISHighLight?.Invoke(newPosition.x, newPosition.y,
            CheckEnemyForKnightKingRook(newPosition, chessPlayerPlacementHandler._checkPiceType));
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("===>" + transform.name);
        PlayerMovement.Instance.Player = gameObject;
        if (PlayerMovement.Instance._Input && PlayerMovement.Instance.ISvalidMove())
        {
            if (ChessBoard._HighlightPositions.Count != 0)
            {
                ChessBoardPlacementHandler.Instance.ClearHighlightsAtSpeciFicpositions();
            }
            KnightpossibleMoves();
            PlayerMovement.Instance.PlayerRowColumnDetails = RowColumnDetails;
        }

    }

}
