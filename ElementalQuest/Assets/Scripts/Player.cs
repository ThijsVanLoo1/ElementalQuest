using UnityEngine;
using TMPro;
using System.Threading;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject canvas;
    public Animator animator;
    public Transform spotLightTransform;

    Vector2 movement;
    private bool oreInRange = false;
    private GameObject ore;
    public static bool canWalk = true;

    private string oreName;
    private string oreSymbol;
    private GameObject tooltip;

    AudioManager audioManager;
    private float Timer = 5f;

    //Finds Audio before first frame
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        //Mining QTE Start
        if (!canvas.activeSelf && oreInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startQuickTime();
        }

        //Lighting
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.x, -movement.y) * Mathf.Rad2Deg;
            angle += 180f;
            spotLightTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {


        //Makes Player walk
        if (canWalk)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        if (movement.x != 0 || movement.y != 0)
        {
            if (Timer > 4.3f)
            {
                audioManager.playSFX(audioManager.Walking);
                Timer = 0;
            }
        }

        if (movement.x == 0 && movement.y == 0)
        {
            Timer = 5f;
            audioManager.StopSFX();
        }

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