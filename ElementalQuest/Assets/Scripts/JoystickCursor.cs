using UnityEngine;
using UnityEngine.SceneManagement;
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
                break;
            }
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

  

