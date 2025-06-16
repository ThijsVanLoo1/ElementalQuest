using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject canvas;
    public Animator animator;

    Vector2 movement;
    private bool oreInRange = false;
    private GameObject ore;
    public static bool canWalk = true;

    private string oreName;
    private string oreSymbol;
    private GameObject tooltip;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (!canvas.activeSelf && oreInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startQuickTime();
        }
    }

    private void FixedUpdate()
    {
        if(canWalk) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            oreInRange = true;
            ore = other.gameObject;
            oreName = ore.GetComponent<Ore>().oreName;
            oreSymbol = ore.GetComponent<Ore>().oreSymbol;
            tooltip = ore.GetComponentInChildren<Canvas>(true).gameObject;
            Debug.Log(tooltip);
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = oreName + " (" + oreSymbol + ")";
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            oreInRange = false;
            tooltip.SetActive(false);
        }
    }

    private void startQuickTime()
    {
        QuicktimeEvent.ore = ore;
        canvas.SetActive(true);
        canWalk = false;
    }

    public void setIsWalkingTrue()
    {
        animator.SetBool("IsWalking", true);
    }

    public void setIsWalkingFalse()
    {
        animator.SetBool("IsWalking", false);
    }
}