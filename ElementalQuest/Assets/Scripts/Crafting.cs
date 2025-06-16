using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Crafting : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;
    public Slot[] craftingSlots;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);

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


        if (currentItem != null)
        {
            customCursor.transform.position = Input.mousePosition;
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
                Debug.Log(recipes[i]);
                if (resultSlot == null)
                {
                    Debug.LogError("resultSlot is null. Did you forget to assign it in the Inspector?");
                    return;
                }

                if (recipeResults[i] == null)
                {
                    Debug.LogError("recipeResults[" + i + "] is null. Did you assign it in the Inspector?");
                    return;
                }

                Image recipeImage = recipeResults[i].GetComponent<Image>();
                if (recipeImage == null)
                {
                    Debug.LogError("recipeResults[" + i + "] is missing an Image component.");
                    return;
                }

                Image resultSlotImage = resultSlot.GetComponent<Image>();
                if (resultSlotImage == null)
                {
                    Debug.LogError("resultSlot is missing an Image component.");
                    return;
                }

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
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
        else
        {
            customCursor.gameObject.SetActive(false);
            currentItem = null;
        }
    }
}
