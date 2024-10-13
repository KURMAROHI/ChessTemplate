using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public GameObject Player = null;
    public Vector2Int PlayerRowColumnDetails = Vector2Int.zero;

    public bool _Input = true;

    GameObject Hightlighter = null;

    ValidPlayerMove _validplayermove = ValidPlayerMove.White;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    float lerpDuration = 0.5f;

    float elapsedTime = 10f;


    public void MoveThePlayer(GameObject HighLighter, Vector2Int _HighLighterRowColumnDetails)
    {
        Hightlighter = HighLighter;
        if (Player != null)
        {
            if (Player.CompareTag("Black") && ISvalidPosition(_HighLighterRowColumnDetails))
            {
                _validplayermove = ValidPlayerMove.White;
                Debug.Log("==>Resetting elapsed Time|" + elapsedTime);
                elapsedTime = 0;
                ChessBoard._validBlackPositions[PlayerRowColumnDetails.x, PlayerRowColumnDetails.y] = false;
                ChessBoard._validBlackPositions[_HighLighterRowColumnDetails.x, _HighLighterRowColumnDetails.y] = true;
            }
            else if (ISvalidPosition(_HighLighterRowColumnDetails))
            {
                _validplayermove = ValidPlayerMove.Black;
                elapsedTime = 0;
                ChessBoard._validWhitePositions[PlayerRowColumnDetails.x, PlayerRowColumnDetails.y] = false;
                ChessBoard._validWhitePositions[_HighLighterRowColumnDetails.x, _HighLighterRowColumnDetails.y] = true;
            }
            Player.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>().row = _HighLighterRowColumnDetails.x;
            Player.GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>().column = _HighLighterRowColumnDetails.y;
        }
    }



    void Update()
    {
        if (Player != null && Hightlighter != null && elapsedTime < lerpDuration)
        {
            _Input = false;
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / lerpDuration;
            t = Mathf.Clamp01(t);
            Player.transform.position = Vector2.Lerp(Player.transform.position, Hightlighter.transform.position, t);
            if (t >= 1.0f)
            {
                _Input = true;
                Debug.Log("===>Serilously it Completed moving|" + elapsedTime + "::" + lerpDuration);
            }
        }
        else
        {
            _Input = true;
        }
    }


    bool ISvalidPosition(Vector2Int Position)
    {
        return Position.x >= 0 && Position.x < 8 && Position.y >= 0 && Position.y < 8;
    }

    public bool ISvalidMove()
    {
        return Player.CompareTag("Black") && _validplayermove == ValidPlayerMove.Black || Player.CompareTag("White") && _validplayermove == ValidPlayerMove.White;
    }
}

public enum ValidPlayerMove
{
    White,
    Black,
}
