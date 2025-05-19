using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Breakable breakable = other.GetComponent<Breakable>();
        if (breakable != null)
        {
            breakable.Break();
        }
    }
}
