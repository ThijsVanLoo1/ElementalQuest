using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography.X509Certificates;

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
    public RectTransform cooldownFill;
    public Animator playerAnimator;

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    public static GameObject ore;
    private float oreMass;
    public string oreName;

    public Slider healthBar;
    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI maxHealth;

    AudioManager audioManager;

    //Finds Audio before first frame
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
    }

    private void OnEnable()
    {
        oreMass = ore.GetComponent<Ore>().oreMass;
        oreName = ore.GetComponent<Ore>().oreName;
        healthBar.maxValue = oreMass;
        healthBar.value = oreMass;

        currentHealth.text = oreMass.ToString();
        maxHealth.text = oreMass.ToString();
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
            audioManager.playSFXStoppable(audioManager.MineSound);
            StartCoroutine(InputCooldownCoroutine());
        }
        if(canInput && Input.GetButtonDown("Cancel"))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            Player.canWalk = true;
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
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(normalForce, pointerTransform.position, null))
            {
                oreMass -= 2;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(minForce, pointerTransform.position, null))
            {
                oreMass -= 1;
            }

            healthBar.value = oreMass;
            currentHealth.text = (oreMass >= 0) ? oreMass.ToString() : "0";
        }
    }

    void CheckComplete()
    {
        Player.canWalk = true;

        // Do inventory shit <-- Jeffrey
        InventoryOpacity inventory = FindFirstObjectByType<InventoryOpacity>();
        if (inventory != null)
        {
            inventory.AddToInventory(oreName);
        }
        Destroy(ore);
        gameObject.transform.parent.gameObject.SetActive(false);

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

        if (oreMass <= 0) CheckComplete();

        canInput = true;
    }
}
