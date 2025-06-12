using UnityEngine;

public class QuicktimeEvent : MonoBehaviour
{
    public Transform pointA; // Reference to the starting point
    public Transform pointB; // Reference to the ending point
    public RectTransform minForce; // Red zone
    public RectTransform normalForce; // Yellow zone
    public RectTransform maxForce; // Green zone
    public float moveSpeed; // Speed of the pointer movement

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    public static GameObject ore;
    private float oreMass;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
    }

    private void OnEnable()
    {
        oreMass = ore.GetComponent<Ore>().oreMass;
        Debug.Log(oreMass);
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
        if (Input.GetButtonDown("MineOre") || Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        if (oreMass > 0)
        {
            // Check if the pointer is within the safe zone
            if (RectTransformUtility.RectangleContainsScreenPoint(maxForce, pointerTransform.position, null))
            {
                Debug.Log("Sterke slag!");
                oreMass -= 3;
                Debug.Log(oreMass);
                if (oreMass <= 0) CheckComplete();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(normalForce, pointerTransform.position, null))
            {
                Debug.Log("Medium slag!!");
                oreMass -= 2;
                Debug.Log(oreMass);
                if (oreMass <= 0) CheckComplete();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(minForce, pointerTransform.position, null))
            {
                oreMass -= 1;
                Debug.Log("Zwakke slag!!");
                Debug.Log(oreMass);
                if (oreMass <= 0) CheckComplete();
            }
        }
    }

    void CheckComplete()
    {
            Destroy(ore);
            gameObject.transform.parent.gameObject.SetActive(false);
    }
}
