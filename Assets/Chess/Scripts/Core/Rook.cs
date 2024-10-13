using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rook : ChessPiece, IPointerDownHandler
{
    Vector2Int RowColumnDetails;
    Chess.Scripts.Core.ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    void Start()
    {

        chessPlayerPlacementHandler = transform.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        // Debug.Log("==>Row Column|" + RowColumnDetails);
    }


    List<Vector2Int> possibleBishopPosition = new List<Vector2Int>();
    void RookpossibleMoves()
    {
        RowColumnDetails = chessPlayerPlacementHandler.RowColumnDetails();
        List<Vector2Int> possibleMovesPosition = new List<Vector2Int>();
        BishopPossibleMoves();
        int[] directions = { 0, 1, -1 };
        foreach (int xDir in directions)
        {
            foreach (int yDir in directions)
            {
                Vector2Int newPosition = RowColumnDetails;
                while (true)
                {
                    newPosition += new Vector2Int(xDir, yDir);
                    if (ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType) && !possibleBishopPosition.Contains(newPosition))
                    {
                        possibleMovesPosition.Add(newPosition);
                    }
                    else
                    {
                        break;
                    }


                    //Checking the Condition For Enemy in the Path Exist or not if Enemy Found the Highighter not move Forwrd to Enemy
                    //But For Rook its Different We Added Extra Condition  that !possibleBishopPosition.Contains(newPosition).The new position
                    //Does not Contain in posiibleBishop List there is no need of breaking there becuase we Are not Adding those values
                    //possibleMovesposition Which Are using For Highlight
                    if (!possibleBishopPosition.Contains(newPosition) &&
                    CheckEnemyForOther(newPosition, chessPlayerPlacementHandler._checkPiceType))
                    {
                        break;
                    }


                }
            }
        }

        //We can get all possble move of Queeen From that We Are Removing the Bishop moves So that we can Desired Moves For Rook
        foreach (Vector2Int newPosition in possibleMovesPosition)
        {
            ISHighLight?.Invoke(newPosition.x, newPosition.y,
            CheckEnemyForKnightKingRook(newPosition, chessPlayerPlacementHandler._checkPiceType));
        }

        possibleBishopPosition.Clear();
    }

    void BishopPossibleMoves()
    {
        int[] directions = { 1, -1 };
        foreach (int xDir in directions)
        {
            foreach (int yDir in directions)
            {
                Vector2Int newPosition = RowColumnDetails;
                while (true)
                {
                    newPosition += new Vector2Int(xDir, yDir);
                    if ((ISvalidMove(newPosition, chessPlayerPlacementHandler._checkPiceType)) &&
                    !possibleBishopPosition.Contains(newPosition))
                    {
                        possibleBishopPosition.Add(newPosition);
                    }
                    else
                    {
                        break;
                    }

                }
            }
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
            RookpossibleMoves();
            PlayerMovement.Instance.PlayerRowColumnDetails = RowColumnDetails;
        }
    }
}
