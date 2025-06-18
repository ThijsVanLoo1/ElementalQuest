using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour
{
    public Item item;
    public int index;
    public Sprite defaultSprite; 

    private void Awake()
    {
        defaultSprite = GetComponent<Image>().sprite;
    }
}
