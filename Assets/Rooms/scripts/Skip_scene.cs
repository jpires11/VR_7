using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneNumber;

    public void OnButtonPressed()
    {
        Debug.Log("Button Pressed! Loading Scene: " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }
}
