using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        } else if(Input.GetButtonDown("Pause") && isPaused)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void options()
    {
        //Hier komen options als we die ooit hebben
    }

    public void quitGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(0);
    }
}
