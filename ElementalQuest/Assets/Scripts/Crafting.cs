using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Crafting : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;
    public Slot[] craftingSlots;
    private Sprite defaultCursorSprite;
    public Transform controllerCursor;


    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;

    private void Start()
    {
           defaultCursorSprite = customCursor.sprite;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetButtonDown("Submit"))
        {
            if (currentItem != null)
            {
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float distance = Vector2.Distance(controllerCursor.position, slot.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSlot = slot;
                    }
                }

                if (nearestSlot != null)
                {
                    nearestSlot.gameObject.SetActive(true);
                    nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                    nearestSlot.item = currentItem;
                    itemList[nearestSlot.index] = currentItem;

                    currentItem = null;
                    CheckForCompletedRecipe();
                }
            }
        }




    }
    void CheckForCompletedRecipe()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentItems = "";
        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentItems += item.itemName;
            }
            else
            {
                currentItems += "null";
            }
        }
        for (int i = 0; i < recipes.Length; i++)
        {
            Debug.Log(currentItems);
            if (recipes[i] == currentItems)
            {
         

                Image recipeImage = recipeResults[i].GetComponent<Image>();
           

                Image resultSlotImage = resultSlot.GetComponent<Image>();
               

                resultSlot.gameObject.SetActive(true);
                resultSlotImage.sprite = recipeImage.sprite;
                resultSlot.item = recipeResults[i];
            }
        }

    }
    public void ClearSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCompletedRecipe();
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
        else
        {
            customCursor.sprite = defaultCursorSprite;
            currentItem = null;
        }
    }
}
