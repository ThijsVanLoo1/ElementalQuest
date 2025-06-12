using UnityEngine;
using UnityEngine.UI;

public class InventoryOpacity : MonoBehaviour
{
    public Image panelImage; // Assign this in the Inspector

    // Set opacity (0 = transparent, 1 = opaque)
    public void SetOpacity(float alpha)
    {
        Color color = panelImage.color;
        color.a = Mathf.Clamp01(alpha); // Ensure alpha is between 0 and 1
        panelImage.color = color;
    }

    void Start()
    {
        // Example: Set panel to 50% opacity at start
        SetOpacity(0.5f);
    }
}