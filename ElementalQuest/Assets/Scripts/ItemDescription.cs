using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDescription : MonoBehaviour
{
    public GameObject oreDescription;
    public TMP_Text oreDescriptionText;

    public void Show(string message, Vector2 position)
    {
        oreDescription.SetActive(true);
        oreDescriptionText.text = message;
        oreDescription.transform.position = position;
    }

    public void Hide()
    {
        oreDescription.SetActive(false);
    }

}
