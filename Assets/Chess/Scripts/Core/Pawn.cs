using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pawn : ChessPiece, IPointerDownHandler
{
    Vector2Int RowColumnDetails;
    Chess.Scripts.Core.ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    void Start()
    {
        chessPlayerPlacementHandler = transform.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        //Debug.Log("==>Row Column|" + RowColumnDetails);
    }


    void PawnpossibleMoves()
    {
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        Vector2Int[] moveOffsets = new Vector2Int[4];
        Debug.Log("==>" + chessPlayerPlacementHandler._checkPiceType);
        if (chessPlayerPlacementHandler._checkPiceType == ChessPieceType.White)
        {
            Debug.Log("==>Enter For if Condition");
            moveOffsets[0] = new Vector2Int(-1, -1);
            moveOffsets[1] = new Vector2Int(-1, 1);
            moveOffsets[2] = new Vector2Int(-1, 0);
            if (RowColumnDetails.x == 6)
            {
                moveOffsets[3] = new Vector2Int(-2, 0);
            }
        }
        else
        {
            Debug.Log("==>Enter For else Condition");
            moveOffsets[0] = new Vector2Int(1, -1);
            moveOffsets[1] = new Vector2Int(1, 1);
            moveOffsets[2] = new Vector2Int(1, 0);
            if (RowColumnDetails.x == 1)
            {
                moveOffsets[3] = new Vector2Int(2, 0);
            }

        }


        for (int i = 0; i < moveOffsets.Length; i++)
        {
            Vector2Int newPosition = RowColumnDetails + moveOffsets[i];
            //the pawn Can move if Enemy is present So i Am move offset And Index position 0,1 first i will Check Cross positions
            //the First if Condityion it Check the Crosse Enemy(White)
            // Debug.Log("----->" + ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType)+"::" +newPosition);
            if (moveOffsets[i].y != 0 && ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType))
            {
                Debug.Log("<color=red>==>Moveoffset</color>|" + RowColumnDetails + "::" + moveOffsets[i]);
                CheckEnemyForOther(newPosition, chessPlayerPlacementHandler._checkPiceType);
            }
            else if (ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType))
            {
                ISHighLight?.Invoke(newPosition.x, newPosition.y, false);
            }

            if (moveOffsets[i].y == 0 && CheckEnemyForOther(newPosition, chessPlayerPlacementHandler._checkPiceType))
            {
                break;
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
            PawnpossibleMoves();
            PlayerMovement.Instance.PlayerRowColumnDetails = RowColumnDetails;
        }
    }
}
