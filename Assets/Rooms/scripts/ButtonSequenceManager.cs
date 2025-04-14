using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonTrigger : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public Button continueButton;

    private int currentStep = 0;

    private void Start()
    {
        continueButton.interactable = false;
    }

    public void ButtonPressed(GameObject pressedButton)
    {
        if (currentStep == 0 && pressedButton == button1)
        {
            Debug.Log("Step 1: Button 1 correct");
            currentStep = 1;
        }
        else if (currentStep == 1 && pressedButton == button2)
        {
            Debug.Log("Step 2: Button 2 correct");
            currentStep = 2;
        }
        else if (currentStep == 2 && pressedButton == button3)
        {
            Debug.Log("Step 3: Button 3 correct. Sequence complete!");
            continueButton.interactable = true;
        }
        else
        {
            Debug.Log("Wrong button or order. Resetting sequence.");
            currentStep = 0;
            continueButton.interactable = false;
        }
    }
}
