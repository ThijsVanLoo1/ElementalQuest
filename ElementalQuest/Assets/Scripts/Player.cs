using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject canvas;
    public GameObject crafting;
    public GameObject signCanvas;
    public Animator animator;
    public Transform spotLightTransform;

    Vector2 movement;
    private bool oreInRange = false;
    private bool craftingTableInRange = false;
    private bool elevatorInRange = false;
    private bool signInRange = false;

    private GameObject ore;
    private GameObject craftingTable;
    private GameObject elevator;
    private GameObject sign;
    public static bool canWalk = true;

    private string oreName;
    private string oreSymbol;
    private GameObject tooltip;

    AudioManager audioManager;
    private float Timer = 5f;
    private float Timer2 = 0f;

    //Finds Audio before first frame
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        Timer2 += Time.deltaTime;
        if (!canWalk) return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        //Mining QTE Start
        if (!canvas.activeSelf && oreInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startQuickTime();
        }
        if (!canvas.activeSelf && craftingTableInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startCrafting();
        }
        if (!canvas.activeSelf && signInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            readSign();
        }
        //hide canvas after 3s
        if (signCanvas.activeSelf && Timer2 > 3f)
        {
            signCanvas.SetActive(false);
        }
        if (!canvas.activeSelf && craftingTableInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startCrafting();
        }
        if (!canvas.activeSelf && elevatorInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            repairElevator();
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

        else if (other.CompareTag("CraftingTable"))
        {
            craftingTableInRange = true;
            craftingTable = other.gameObject;
            tooltip = craftingTable.GetComponentInChildren<Canvas>(true).gameObject;
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text ="Open crafting table";
            tooltip.SetActive(true);

        }

        else if (other.CompareTag("Elevator"))
        {
            Debug.Log("working elevator");
            elevatorInRange = true;
            elevator = other.gameObject;
            tooltip = elevator.GetComponentInChildren<Canvas>(true).gameObject;
            tooltip.SetActive(true);
        }

        else if (other.CompareTag("Sign"))
        {
            signInRange = true;
            sign = other.gameObject;
            tooltip = sign.GetComponentInChildren<Canvas>(true).gameObject;
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

        if (other.CompareTag("Sign"))
        {
            signInRange = false;
            tooltip.SetActive(false);
        }

        if (other.CompareTag("Elevator"))
        {
            signInRange = false;
            tooltip.SetActive(false);
        }
    }

    private void startQuickTime()
    {
        QuicktimeEvent.ore = ore;
        canvas.SetActive(true);
        canWalk = false;
    }
    private void startCrafting()
    {
        crafting.SetActive(true);
        canWalk = false;
    }

    private void readSign()
    {
        signCanvas.SetActive(true);
        Timer2 = 0f;
    }

    private void repairElevator()
    {
        //inventory check
        SceneManager.LoadScene("Ending");
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