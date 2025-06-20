using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class JoystickMouse : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    public Crafting crafting;
    public LayerMask itemMask;
    public RectTransform controllerCursor;
    private Item hoveredItem;
    private Slot hoveredSlot;
    public ItemDescription itemDescription;


    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        hoveredItem = null;
        hoveredSlot = null;
        PointerEventData pointerData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        pointerData.position = controllerCursor.position;

        var results = new List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(pointerData, results);

        bool showDescription = false;
        foreach (var result in results)
        {
            var slot = result.gameObject.GetComponent<Slot>();
            var item = result.gameObject.GetComponent<Item>();

            if (slot != null)
            {
                hoveredSlot = slot;
                break;
            }
            else if (item != null)
            {
                hoveredItem = item;

                if (item != null && !string.IsNullOrEmpty(item.description))
                {
                    Vector2 offset = new Vector2(80, 40);
                    itemDescription.Show(item.description, (Vector2)controllerCursor.position + offset);
                    showDescription = true;
                }
                break;
            }
        }

        if (!showDescription)
        {
            itemDescription.Hide();
        }



        if (Input.GetButtonDown("Submit"))
        {

            if (hoveredItem != null)
            {
                crafting.OnMouseDownItem(hoveredItem);


            }

            if (hoveredSlot != null)
            {
                crafting.OnMouseDownSlot(hoveredSlot);
            }
        }


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}

  

