using UnityEngine;

public class QuicktimeEvent : MonoBehaviour
{
    public Transform pointA; // Reference to the starting point
    public Transform pointB; // Reference to the ending point
    public RectTransform minForce; // Red zone
    public RectTransform normalForce; // Yellow zone
    public RectTransform maxForce; // Green zone
    public float moveSpeed = 100f; // Speed of the pointer movement

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Change direction if the pointer reaches one of the points
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
        }

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space)) //KeyCode.JoystickButton5 for controller --> R2 button?
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    { 
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(maxForce, pointerTransform.position, null))
        {
            Debug.Log("Sterke slag!");

        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(normalForce, pointerTransform.position, null))
        {
            Debug.Log("Medium slag!!");
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(minForce, pointerTransform.position, null))
        {
            Debug.Log("Zwakke slag!!");
        }
    }
}
