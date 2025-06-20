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

    private int lastCompletedRecipe = -1;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;

    public GameObject water;
    public GameObject ethene;
    public GameObject ethanol;
    public GameObject hydrochloricAcid;
    private void Start()
    {
        defaultCursorSprite = customCursor.sprite;
        //WHEN ORE IS MINED -> SET ITEM OBJECT TRUE (OR FROM INVENTORY)
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
            if (recipes[i] == currentItems)
            {


                Image recipeImage = recipeResults[i].GetComponent<Image>();


                Image resultSlotImage = resultSlot.GetComponent<Image>();


                resultSlot.gameObject.SetActive(true);
                resultSlotImage.sprite = recipeImage.sprite;
                resultSlot.item = recipeResults[i];
                lastCompletedRecipe = i;
            }
        }

    }

    void AddCompletedElement(int i)
    {
        // ADD TO INVENTORY IN EVERY LOOP
        if (i == 0)
        {
            water.gameObject.SetActive(true);
            lastCompletedRecipe = -1;
        }
        else if (i == 1) {
            ethene.gameObject.SetActive(true);
            lastCompletedRecipe = -1;
        }
        else if (i == 2)
        {
            ethanol.gameObject.SetActive(true);
            lastCompletedRecipe = -1;
        }
        else if (i == 3)
        {
            hydrochloricAcid.gameObject.SetActive(true);
            lastCompletedRecipe = -1;
        }

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
    public void OnMouseDownSlot(Slot slot)
    {
        if (currentItem != null)
        {

            slot.gameObject.SetActive(true);
            slot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
            slot.item = currentItem;
            itemList[slot.index] = currentItem;

            customCursor.sprite = defaultCursorSprite;
            currentItem = null;
            CheckForCompletedRecipe();

        }
        else
        {
            if (slot != resultSlot)
            {
                slot.item = null;
                itemList[slot.index] = null;
                slot.GetComponent<Image>().sprite = slot.defaultSprite;

                CheckForCompletedRecipe();
            }
            else
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    craftingSlots[i].item = null;
                    craftingSlots[i].GetComponent<Image>().sprite = craftingSlots[i].defaultSprite;
                    resultSlot.item = null;
                    resultSlot.gameObject.SetActive(false);
                    itemList[i] = null;
                    AddCompletedElement(lastCompletedRecipe);

                }


            }
        }
    }
}
