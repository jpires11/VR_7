using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Plate : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    private bool hasBeenTouched = false;

    private static int touchedPlatesCount = 0;
    private static int totalPlates = 3;
    private static bool puzzleSolved = false;

    private LevelCompletion levelCompletion;

    public GameObject levelManager;
    public GameObject fishbone;

    // 🔊 Sound to play when puzzle is solved
    public AudioClip puzzleSolvedSound;
    public AudioSource audioSource;

    public float moveSpeed = 0.2f;
    public float maxMoveDistance = 0.5f;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float moveTimer;
    private float moveInterval = 3.0f;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        else
        {
            Debug.LogError("Renderer component not found.");
        }

        if (levelManager != null)
        {
            levelCompletion = levelManager.GetComponent<LevelCompletion>();
            if (levelCompletion == null)
            {
                Debug.LogWarning("LevelCompletion script not found.");
            }
        }

        if (fishbone != null)
        {
            fishbone.SetActive(true);
        }

        originalPosition = transform.position;
        SetNewRandomTarget();
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            SetNewRandomTarget();
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void SetNewRandomTarget()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-maxMoveDistance, maxMoveDistance),
            Random.Range(-maxMoveDistance, maxMoveDistance),
            Random.Range(-maxMoveDistance, maxMoveDistance)
        );

        targetPosition = originalPosition + randomOffset;
        moveTimer = moveInterval;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }

    private void ChangeColorToYellow()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.yellow;
            hasBeenTouched = true;
            touchedPlatesCount++;

            if (fishbone != null)
            {
                fishbone.SetActive(false);
            }

            CheckAllPlatesCompleted();
        }
    }

    private void CheckAllPlatesCompleted()
    {
        if (!puzzleSolved && touchedPlatesCount >= totalPlates)
        {
            puzzleSolved = true;

            // ✅ Play sound from assigned AudioSource
            if (audioSource != null && puzzleSolvedSound != null)
            {
                audioSource.PlayOneShot(puzzleSolvedSound);
            }

            if (levelCompletion != null)
            {
                levelCompletion.OnLevelCompleted();
            }
        }
    }

    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;

            if (hasBeenTouched)
            {
                hasBeenTouched = false;
                touchedPlatesCount--;

                if (fishbone != null)
                {
                    fishbone.SetActive(true);
                }
            }
        }
    }

    private void OnDestroy()
    {
        touchedPlatesCount = 0;
        puzzleSolved = false;
    }
}
