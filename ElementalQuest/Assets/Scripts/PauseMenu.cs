using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseFirstButton;
    
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        } 
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void Options()
    {
        //Hier komen options als we die ooit hebben
    }

    public void QuitGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(0);
    }
}
