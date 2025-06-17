using System.Collections;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private int lineNumber;

    public string[] lines;

    public TextMeshProUGUI subtitle;

    private void Start()
    {
        lineNumber = 0;
        subtitle.text = lines[0].ToString();
    }

    public void NextLine()
    {
        lineNumber++;
        subtitle.text = lines[lineNumber].ToString();
    }
}
