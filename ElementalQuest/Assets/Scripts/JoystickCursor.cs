using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickMouse : MonoBehaviour
{
    public RectTransform cursorTransform;
    public float cursorSpeed = 1000f;

    private Vector2 moveInput;
    private Vector2 cursorPos;

    void Start()
    {
        cursorPos = cursorTransform.anchoredPosition;
    }

    void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKey("joystick button " + i))
            {
                Debug.Log("Pressed: joystick button " + i);
            }
        }

        for (int i = 1; i <= 10; i++)
        {
            float axis = Input.GetAxis("Joystick Axis " + i);
            if (Mathf.Abs(axis) > 0.1f)
            {
                Debug.Log($"Axis {i}: {axis}");
            }
        }
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        cursorPos += moveInput * cursorSpeed * Time.deltaTime;

        // Clamp position to stay within screen bounds (assuming full screen canvas)
        float clampedX = Mathf.Clamp(cursorPos.x, 0, Screen.width);
        float clampedY = Mathf.Clamp(cursorPos.y, 0, Screen.height);
        cursorTransform.position = new Vector2(clampedX, clampedY);

        // Simulate click (e.g., using "Submit" or "Fire1")
        if (Input.GetButtonDown("Submit"))
        {
            // Send click to UI
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = cursorTransform.position;

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);

            foreach (var result in results)
            {
                ExecuteEvents.Execute(result.gameObject, pointer, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}

  

