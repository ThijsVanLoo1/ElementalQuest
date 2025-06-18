using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button firstButton;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;

            // Selecteer eerste knop
            EventSystem.current.SetSelectedGameObject(null); // reset eerst
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
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
