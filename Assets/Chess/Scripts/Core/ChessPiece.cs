using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
 
    public static Action<int, int, bool> ISHighLight;
    protected bool ISvalidMove(Vector2Int newPosition, ChessPieceType chessPiece)
    {
        if (newPosition.x < 0 && newPosition.x >= 8 && newPosition.y < 0 && newPosition.y >= 8)
        {
            return false;
        }
        try
        {
            bool ISvalidMove = false;
            // if (chessPiece == ChessPieceType.White)
            // {
            //     ISvalidMove= !ChessBoard._validWhitePositions[newPosition.x, newPosition.y];
            // }
            // else if (chessPiece == ChessPieceType.Black)
            // {
            //      ISvalidMove= !ChessBoard._validBlackPositions[newPosition.x, newPosition.y];
            // }
            ISvalidMove = (chessPiece == ChessPieceType.White)?!ChessBoard._validWhitePositions[newPosition.x, newPosition.y]:!ChessBoard._validBlackPositions[newPosition.x, newPosition.y];
            return ISvalidMove;
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckEnemyForOther(Vector2Int newPosition, ChessPieceType chessPiece)
    {
       // Debug.Log("<color=green>==>CheckEnemyForother</color>|" +chessPiece +"::" +newPosition);
        if (chessPiece == ChessPieceType.Black && ChessBoard._validBlackPositions[newPosition.x, newPosition.y]
         || chessPiece == ChessPieceType.White && ChessBoard._validWhitePositions[newPosition.x, newPosition.y])
        {
            return true;
        }
        else if (chessPiece == ChessPieceType.White && ChessBoard._validBlackPositions[newPosition.x, newPosition.y]
        || chessPiece == ChessPieceType.Black && ChessBoard._validWhitePositions[newPosition.x, newPosition.y])
        {
            ISHighLight?.Invoke(newPosition.x, newPosition.y, true);
            return true;
        }
        return false;
    }

    //Which Fucntion SpeciFic For KnightRookKing Which Checks Enemy Only the Above Function For Other than knight,king And Rook
    protected bool CheckEnemyForKnightKingRook(Vector2Int newPosition, ChessPieceType chessPiece)
    {
        //Debug.Log("<color=yellow>==>CheckEnemyForKnightKingRook</color>|" +chessPiece +"::" +newPosition);
        if (chessPiece== ChessPieceType.Black && ChessBoard._validWhitePositions[newPosition.x, newPosition.y]
        || chessPiece == ChessPieceType.White && ChessBoard._validBlackPositions[newPosition.x, newPosition.y])
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
}


