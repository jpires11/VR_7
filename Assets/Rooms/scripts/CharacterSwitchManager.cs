using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Collections;
using System.Collections.Generic;

public class CharacterSwitchManager : MonoBehaviour
{
    [Header("XR Origin & Characters")]
    public XROrigin xrOrigin;
    public GameObject humanCharacter;
    public GameObject catCharacter;

    [Header("Start Options")]
    public bool startAsHuman = true;

    [Header("Hold Input Settings")]
    public InputActionProperty switchAction;
    public float holdDuration = 2.0f;

    [Header("Fade Settings")]
    public FadeScreen fadeScreen;
    public float fadeDelay = 0.1f;

    [Header("Scaling")]
    public float humanScale = 1.0f;
    public float catScale = 0.4f;

    [Header("Movement Speeds")]
    public float humanSpeed = 1.5f;
    public float catSpeed = 3.0f;

    [Header("Audio")]
    public AudioSource catMeowAudio;

    // Internal state
    private bool isHuman;
    private bool isSwitching = false;
    private float holdTimer = 0f;

    private Vector3 humanSavedPosition;
    private Quaternion humanSavedRotation;
    private Vector3 catSavedPosition;
    private Quaternion catSavedRotation;

    void Start()
    {
        // Save starting positions
        humanSavedPosition = humanCharacter.transform.position;
        humanSavedRotation = humanCharacter.transform.rotation;

        catSavedPosition = catCharacter.transform.position;
        catSavedRotation = catCharacter.transform.rotation;

        // Set initial character
        isHuman = startAsHuman;
        Vector3 startPosition = isHuman ? humanSavedPosition : catSavedPosition;
        Quaternion startRotation = isHuman ? humanSavedRotation : catSavedRotation;
        float startScale = isHuman ? humanScale : catScale;
        float startSpeed = isHuman ? humanSpeed : catSpeed;

        // Apply initial settings
        xrOrigin.transform.position = startPosition;
        xrOrigin.transform.rotation = startRotation;
        xrOrigin.transform.localScale = Vector3.one * startScale;

        var moveProvider = xrOrigin.GetComponentInChildren<DynamicMoveProvider>();
        if (moveProvider != null)
            moveProvider.moveSpeed = startSpeed;

        humanCharacter.GetComponent<HumanController>().enabled = isHuman;
        catCharacter.GetComponent<CatController>().enabled = !isHuman;
    }

    void Update()
    {
        if (isSwitching)
            return;

        if (switchAction.action.ReadValue<float>() > 0.5f)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdDuration)
            {
                StartCoroutine(SwitchWithFade());
                holdTimer = 0f;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    IEnumerator SwitchWithFade()
    {
        isSwitching = true;

        // Disable CharacterController during the switch
        CharacterController cc = xrOrigin.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration + fadeDelay);
        }

        bool switchingToCat = isHuman;
        PerformCharacterSwitch();

        if (fadeScreen != null)
        {
            fadeScreen.FadeIn();
            yield return new WaitForSeconds(fadeScreen.fadeDuration + fadeDelay);
        }

        // Play meow if switching to cat
        if (switchingToCat && catMeowAudio != null)
        {
            catMeowAudio.Play();
        }

        // Re-enable CharacterController after the switch
        if (cc != null) cc.enabled = true;

        isSwitching = false;
    }


    void PerformCharacterSwitch()
    {
        CharacterController cc = xrOrigin.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        if (isHuman)
        {
            // Save human position
            humanSavedPosition = xrOrigin.transform.position;
            humanSavedRotation = xrOrigin.transform.rotation;

            // Move to cat position
            xrOrigin.transform.position = catSavedPosition;
            xrOrigin.transform.rotation = catSavedRotation;
            xrOrigin.transform.localScale = Vector3.one * catScale;
        }
        else
        {
            // Save cat position
            catSavedPosition = xrOrigin.transform.position;
            catSavedRotation = xrOrigin.transform.rotation;

            // Move to human position
            xrOrigin.transform.position = humanSavedPosition;
            xrOrigin.transform.rotation = humanSavedRotation;
            xrOrigin.transform.localScale = Vector3.one * humanScale;
        }

        // Toggle state
        isHuman = !isHuman;

        // Reactivate CharacterController
        if (cc != null) cc.enabled = true;

        // Enable/disable scripts based on new state
        humanCharacter.GetComponent<HumanController>().enabled = isHuman;
        catCharacter.GetComponent<CatController>().enabled = !isHuman;
        xrOrigin.GetComponent<CatJumpControllerVignette>().enabled = !isHuman;

        var moveProvider = xrOrigin.GetComponentInChildren<DynamicMoveProvider>();
        if (moveProvider != null)
            moveProvider.moveSpeed = isHuman ? humanSpeed : catSpeed;
    }
}
