using UnityEngine;
using TMPro;
using System.Collections;

public class QuicktimeEvent : MonoBehaviour
{
    public Transform pointA; // Reference to the starting point
    public Transform pointB; // Reference to the ending point
    public RectTransform minForce; // Red zone
    public RectTransform normalForce; // Yellow zone
    public RectTransform maxForce; // Green zone
    public float moveSpeed; // Speed of the pointer movement
    private bool canInput = true;
    private float inputCooldown = 0.7f;
    public TMP_Text mass;
    public RectTransform cooldownFill;
    public Animator playerAnimator;

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
        mass.text = "Atoommassa: " + oreMass.ToString();
    }

    void Update()
    {
        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime * Screen.width / 840);

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
        if (canInput && (Input.GetButtonDown("MineOre") || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            CheckSuccess();
            StartCoroutine(InputCooldownCoroutine());
        }
    }

    void CheckSuccess()
    {
        if (oreMass > 0)
        {
            // Check if the pointer is within the safe zone
            if (RectTransformUtility.RectangleContainsScreenPoint(maxForce, pointerTransform.position, null))
            {
                oreMass -= 3;
                mass.text = "Atoommassa: " + oreMass.ToString();
                if (oreMass <= 0) CheckComplete();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(normalForce, pointerTransform.position, null))
            {
                oreMass -= 2;
                mass.text = "Atoommassa: " + oreMass.ToString();
                if (oreMass <= 0) CheckComplete();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(minForce, pointerTransform.position, null))
            {
                oreMass -= 1;
                mass.text = "Atoommassa: " + oreMass.ToString();
                if (oreMass <= 0) CheckComplete();
            }
        }
    }

    void CheckComplete()
    {
        Destroy(ore);
        gameObject.transform.parent.parent.gameObject.SetActive(false);
        Player.canWalk = true;
    }

    IEnumerator InputCooldownCoroutine()
    {
        canInput = false;

        playerAnimator.SetTrigger("Click");

        float elapsed = 0f;

        cooldownFill.localScale = new Vector3(1, 0, 1);

        while (elapsed < inputCooldown)
        {
            elapsed += Time.deltaTime;
            float scale = 0f + (elapsed / inputCooldown);
            cooldownFill.localScale = new Vector3(1, scale, 1);
            yield return null;
        }
        cooldownFill.localScale = new Vector3(1, 1, 1);

        canInput = true;
    }
}
