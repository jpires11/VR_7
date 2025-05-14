using UnityEngine;
using System.Collections;

public class MemorySequence : MonoBehaviour
{
    [Header("Cylinder Object")]
    public GameObject cylinder;

    [Header("Materials")]
    public Material material1;
    public Material material2;
    public Material material3;
    public Material startMaterial; // "B" indicator

    [Header("Timing Settings")]
    public float colorChangeDuration = 1f;
    public float pauseBetweenChanges = 0.5f;

    [Header("Sequence Configuration")]
    public int[] sequence = { 0, 1, 2, 1 }; // Can be set from Inspector

    private Renderer cylinderRenderer;

    void Start()
    {
        if (cylinder != null)
        {
            cylinderRenderer = cylinder.GetComponent<Renderer>();
            StartCoroutine(PlaySequenceLoop());
        }
        else
        {
            Debug.LogError("Cylinder GameObject not assigned.");
        }
    }

    IEnumerator PlaySequenceLoop()
    {
        while (true)
        {
            // Start marker ("B")
            cylinderRenderer.material = startMaterial;
            yield return new WaitForSeconds(colorChangeDuration);
            yield return new WaitForSeconds(pauseBetweenChanges);

            // Show the sequence
            foreach (int index in sequence)
            {
                switch (index)
                {
                    case 0:
                        cylinderRenderer.material = material1;
                        break;
                    case 1:
                        cylinderRenderer.material = material2;
                        break;
                    case 2:
                        cylinderRenderer.material = material3;
                        break;
                    default:
                        Debug.LogWarning("Invalid index in sequence array: " + index);
                        continue;
                }

                yield return new WaitForSeconds(colorChangeDuration);
                yield return new WaitForSeconds(pauseBetweenChanges);
            }
        }
    }
}
