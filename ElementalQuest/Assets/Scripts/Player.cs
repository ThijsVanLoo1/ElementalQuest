using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject canvas;
    public GameObject crafting;
    public GameObject binasCanvas;
    public GameObject signCanvas;
    public Animator animator;
    public Transform spotLightTransform;
    public GameObject fuel;

    Vector2 movement;
    private bool oreInRange = false;
    private bool craftingTableInRange = false;
    private bool elevatorInRange = false;

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
    private bool craftingSFXPlaying = false;


    //Finds Audio before first frame
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if(craftingSFXPlaying)
        {
            Timer2 += Time.deltaTime;
        }

        if (canWalk)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        //Mining QTE Start
        if (!canvas.activeSelf && oreInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startQuickTime();
        }
        //Open Crafting Table
        if (!canvas.activeSelf && craftingTableInRange && (Input.GetButtonDown("MineOre") || Input.GetKey(KeyCode.Mouse0))) //Change for controller input
        {
            startCrafting();
        }
        //Toggle Binas from CT
        if (crafting.activeSelf && Input.GetButtonDown("OpenInventory")) //Change for controller input
        {
            ToggleBinas();
        }
        //Repair Elevator
        if (!canvas.activeSelf && elevatorInRange && Input.GetButtonDown("MineOre")) //Change for controller input
        {
            repairElevator();
        }
        //Go to next Level if Elevator is repaired
        if (craftingSFXPlaying && Timer2 > 3f)
        {
            SceneManager.LoadScene("Ending");

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
                audioManager.playSFXStoppable(audioManager.Walking);
                Timer = 0;
            }
        }

        if (movement.x == 0 && movement.y == 0)
        {
            Timer = 5f;
            audioManager.StopSFXSpecific(audioManager.Walking);
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
            elevatorInRange = true;
            elevator = other.gameObject;
            tooltip = elevator.GetComponentInChildren<Canvas>(true).gameObject;
            tooltip.SetActive(true);
        }

        else if (other.CompareTag("Sign"))
        {
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
        if (other.CompareTag("CraftingTable"))
        {
            craftingTableInRange = false;
            tooltip.SetActive(false);
        }

        if (other.CompareTag("Sign"))
        {
            tooltip.SetActive(false);
        }

        if (other.CompareTag("Elevator"))
        {
            elevatorInRange = false;
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

    void ToggleBinas()
    {
        binasCanvas.SetActive(!binasCanvas.activeSelf);
    }

    private void repairElevator()
    {
        if (fuel.activeSelf)
        {
            audioManager.playSFXStoppable(audioManager.crafting);
            craftingSFXPlaying = true;
        }
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