using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CutsceneManager : MonoBehaviour
{
    private int lineNumber;

    public string[] lines;
    public AudioSource[] sources;

    public TextMeshProUGUI subtitle;

    public GameObject theFrench;
    public GameObject qteCanvas;
    public Animator frenchAnimator;

    private void Start()
    {
        lineNumber = 0;
        subtitle.text = lines[0].ToString();
        sources[0].Play();
    }

    public void NextLine()
    {
        lineNumber++;
        subtitle.text = lines[lineNumber].ToString();
        sources[lineNumber].Play();
    }

    public void StartQTE()
    {
        QuicktimeEvent.ore = theFrench;
        qteCanvas.SetActive(true);

        frenchAnimator.SetTrigger("Attack");
    }

    public void SendToMain()
    {
        SceneManager.LoadScene(0);
    }
}
