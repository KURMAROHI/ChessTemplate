using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChessBoard : MonoBehaviour
{
    public static bool[,] _validBlackPositions = new bool[8, 8]; 
    public static bool[,] _validWhitePositions = new bool[8, 8]; 
     //Which Contains info reagrding the Positin And Color Type Now there is no movment logic if we Are moving then 
     //We Need to Update the Vliadpositions Accroding to move
    public static List<Vector2Int> _HighlightPositions = new List<Vector2Int>();
}
