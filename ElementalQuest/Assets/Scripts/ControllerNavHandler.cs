using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerNavHandler : MonoBehaviour
{
    public GameObject mainFirstButton, optionsFirstButton, creditsFirstButton;

    void Start()
    {
        MainOpened();
    }

    public void MainOpened()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    public void OptionsOpened()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CreditsOpened()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }
}
