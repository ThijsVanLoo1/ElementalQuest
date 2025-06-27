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
        // Display the first line of text and play sound if present
        lineNumber = 0;
        subtitle.text = lines[0].ToString();
        sources[0].Play();
    }

    public void NextLine()
    {
        // Display the next line in the array of lines and sound if present
        lineNumber++;
        subtitle.text = lines[lineNumber].ToString();
        sources[lineNumber].Play();
    }

    // BOSS ONLY - Start QTE
    public void StartQTE()
    {
        // Trick the QTE script into thinking The French is an ore so QTE script can be reused for this
        QuicktimeEvent.ore = theFrench;
        qteCanvas.SetActive(true);

        // Run Frenchman attacking sequence
        frenchAnimator.SetTrigger("Attack");
    }

    // BOSS ONLY - You defeated the French! GG
    public void SendToMain()
    {
        SceneManager.LoadScene(0);
    }
}
