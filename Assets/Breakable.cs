using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public List<GameObject> breakablePieces;
    public RemoteBreakable secondaryBreakable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var piece in breakablePieces)
        {
            piece.SetActive(false);
        }
    }

    public void Break()
    {
        foreach (var piece in breakablePieces)
        {
            piece.SetActive(true);
            piece.transform.parent = null;
        }
       
        //  Debug.Log("Vase Broke");
        gameObject.SetActive(false);

        if (secondaryBreakable != null)
        {
            secondaryBreakable.TriggerBreak();
        }

    }
}
