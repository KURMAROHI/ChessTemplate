using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    private GameObject[,] _chessBoard;


    internal static ChessBoardPlacementHandler Instance;

    private void Awake()
    {
        Instance = this;
        GenerateArray();
    }


    void OnEnable()
    {
        ChessPiece.ISHighLight += Highlight;
    }

    void OnDisable()
    {
        ChessPiece.ISHighLight -= Highlight;
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
                //  ChessBoard.Instance._validPositions[i, j] = _rowsArray[i].transform.GetChild(j).position;
            }
        }
    }

    internal GameObject GetTile(int i, int j)
    {
        try
        {
            return _chessBoard[i, j];
        }
        catch (Exception)
        {
            Debug.LogError("Invalid row or column." +i + "::" +j);
            return null;
        }
    }

    internal void Highlight(int row, int col, bool isEnemy = false)
    {
        var tile = GetTile(row, col).transform;
        if (tile == null)
        {
            Debug.LogError("Invalid row or column." +row + "::" +col);
            return;
        }

        // Debug.Log("==>" + row + "::" + col);
        GameObject HighLighterObject = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        HighLighterObject.transform.GetComponent<HIghlighter>().RowColumnDetails = new Vector2Int(row, col);
        if (isEnemy)
        {
            HighLighterObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        ChessBoard._HighlightPositions.Add(new Vector2Int(row, col));
    }



    internal void ClearHighlights()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform)
                {
                    Destroy(childTransform.gameObject);
                }
            }
        }

    }

    //this is new Function rather than Running 64 times loop just i am Storing the Posiible positions in a List of vector2Int
    //So that i Can iterate throgh that loop only For Destroying the using Same Above logic
    internal void ClearHighlightsAtSpeciFicpositions()
    {
        foreach (var Pos in ChessBoard._HighlightPositions)
        {
            var tile = GetTile(Pos.x, Pos.y);
            if (tile.transform.childCount <= 0) continue;
            foreach (Transform childTransform in tile.transform)
            {
                Destroy(childTransform.gameObject);
            }
        }
        ChessBoard._HighlightPositions.Clear();  //We Are Clearing the List After Destorying 
    }


    #region Highlight Testing

    // private void Start()
    // {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing()
    // {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(5f);
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(3, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(5f);
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(5f);
    // }


    #endregion
}