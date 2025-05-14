using System.Collections.Generic;
using UnityEngine;

public class RemoteBreakable : MonoBehaviour
{
    public List<GameObject> breakablePieces;
    public List<GameObject> exitPieces;

    public void TriggerBreak()
    {
        foreach (var piece in breakablePieces)
        {
            piece.SetActive(true);
            piece.transform.parent = null;
        }

        foreach (var piece in exitPieces)
        {
            piece.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
