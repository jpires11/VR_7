using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherStart : MonoBehaviour
{
    public int sceneNumber = 3;

    public void OnButtonPressed_1()
    {
        Debug.Log("Button Pressed! Loading Scene Index: " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }
}
