using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Crafting : MonoBehaviour
{
    public Item currentItem;
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
    

    public void OnMouseDownItem(Item item)
    {
        Debug.Log("please work");
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
    public void OnMouseDownSlot(Slot slot)
    {
        Debug.Log("it almost works");
        if (currentItem != null)
        {
          
                slot.gameObject.SetActive(true);
                slot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                slot.item = currentItem;
                itemList[slot.index] = currentItem;

                currentItem = null;
                CheckForCompletedRecipe();
            
        }
        else
        {
            slot.item = null;
            itemList[slot.index] = null;
            slot.GetComponent<Image>().sprite = slot.defaultSprite;
            CheckForCompletedRecipe();
        }
    }
}
