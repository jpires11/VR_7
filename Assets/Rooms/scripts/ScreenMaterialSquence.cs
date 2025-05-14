using UnityEngine;
using System.Collections;

public class ScreenMaterialSequence : MonoBehaviour
{
    [Header("Screen Materials (play in order)")]
    public Material[] screenMaterials; // Materials for element 0 (the screen)

    [Header("Timing")]
    public float durationPerStep = 1f;

    private Renderer screenRenderer;

    void Start()
    {
        screenRenderer = GetComponent<Renderer>();
        if (screenRenderer == null)
        {
            Debug.LogError("No Renderer found on the object.");
            return;
        }

        if (screenMaterials.Length == 0)
        {
            Debug.LogWarning("No materials assigned.");
            return;
        }

        StartCoroutine(PlaySequenceLoop());
    }

    IEnumerator PlaySequenceLoop()
    {
        while (true)
        {
            for (int i = 0; i < screenMaterials.Length; i++)
            {
                Material[] currentMaterials = screenRenderer.materials;

                if (currentMaterials.Length > 0)
                {
                    currentMaterials[0] = screenMaterials[i]; // Change only the screen (element 0)
                    screenRenderer.materials = currentMaterials;
                }

                yield return new WaitForSeconds(durationPerStep);
            }
        }
    }
}
