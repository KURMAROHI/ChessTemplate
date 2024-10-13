using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HIghlighter : MonoBehaviour, IPointerDownHandler
{

    public Vector2Int RowColumnDetails; //Row Column Details of Highlighter
    void OnEnable()
    {
        //  Debug.Log("Hihglighter");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("===>" + transform.name + "::" + transform.position);
       // PlayerMovement.Instance.Hightlighter = gameObject;
       // PlayerMovement.Instance.HighLighterRowColumnDetails = RowColumnDetails;
        PlayerMovement.Instance.MoveThePlayer(gameObject,RowColumnDetails);
    }

}
