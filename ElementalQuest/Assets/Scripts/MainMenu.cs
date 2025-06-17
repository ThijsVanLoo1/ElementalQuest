using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject fadeToBlack;
    public void PlayButton()
    {
        fadeToBlack.SetActive(true);
    }

    public void QuitButton()
    {
        Debug.Log("Game is quiting...");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
