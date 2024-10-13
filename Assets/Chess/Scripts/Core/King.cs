using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class King : ChessPiece, IPointerDownHandler
{
    // Start is called before the first frame update
    Vector2Int RowColumnDetails;
    Chess.Scripts.Core.ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    void Start()
    {
        chessPlayerPlacementHandler = transform.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        //        Debug.Log("==>Row Column|" + RowColumnDetails);
    }

    void KingpossibleMoves()
    {
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        Vector2Int[] moveOffsets = {
            new Vector2Int(1, 0), new Vector2Int(0, 1),
            new Vector2Int(-1, 0), new Vector2Int(0, -1),
            new Vector2Int(-1, 1), new Vector2Int(1, 1),
            new Vector2Int(-1, -1), new Vector2Int(1, -1),
        };

        foreach (var offset in moveOffsets)
        {
            Vector2Int newPosition = RowColumnDetails + offset;
            if (ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType))
            {
                ISHighLight?.Invoke(newPosition.x, newPosition.y, CheckEnemyForKnightKingRook(newPosition, chessPlayerPlacementHandler._checkPiceType));
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerMovement.Instance.Player = gameObject;
        if (PlayerMovement.Instance._Input && PlayerMovement.Instance.ISvalidMove())
        {
            if (ChessBoard._HighlightPositions.Count != 0)
            {
                ChessBoardPlacementHandler.Instance.ClearHighlightsAtSpeciFicpositions();
            }
            KingpossibleMoves();
            PlayerMovement.Instance.PlayerRowColumnDetails = RowColumnDetails;
        }
    }
}
