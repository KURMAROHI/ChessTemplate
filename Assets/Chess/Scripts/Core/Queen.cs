using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Queen : ChessPiece, IPointerDownHandler
{
    Vector2Int RowColumnDetails;
    Chess.Scripts.Core.ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    void Start()
    {
        chessPlayerPlacementHandler = transform.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        // Debug.Log("==>Row Column|" + RowColumnDetails);
    }
    void QueenpossibleMoves()
    {
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        int[] directions = { 0, 1, -1 };
        foreach (int xDir in directions)
        {
            foreach (int yDir in directions)
            {
                Vector2Int newPosition = RowColumnDetails;
                while (true)
                {
                    newPosition += new Vector2Int(xDir, yDir);
                    //  if (ISvalidMove(newPosition))
                    if (ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType))
                    {
                        // Debug.Log("==>is valid move" + newPosition);
                        ISHighLight?.Invoke(newPosition.x, newPosition.y, false);
                    }
                    else
                    {
                        break;
                    }

                    if (CheckEnemyForOther(newPosition, chessPlayerPlacementHandler._checkPiceType))
                    {
                        break;
                    }


                }
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
            QueenpossibleMoves();
            PlayerMovement.Instance.PlayerRowColumnDetails = RowColumnDetails;
        }
    }
}
