using System;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        [SerializeField] public int row, column;

        [SerializeField] public ChessPieceType _checkPiceType = ChessPieceType.None;
        private void Start()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            if (_checkPiceType == ChessPieceType.White)
            {
                ChessBoard._validWhitePositions[row, column] = true;
            }
            else if(_checkPiceType == ChessPieceType.Black)
            {
                  ChessBoard._validBlackPositions[row, column] = true;
            }
        }

        public Vector2Int RowColumnDetails()
        {
            return new Vector2Int(row, column);
        }
    }

}

public enum ChessPieceType
{
    None,
    White,
    Black
}