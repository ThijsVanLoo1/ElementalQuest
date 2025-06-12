using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject canvas;

    Vector2 movement;
    private bool oreInRange = false;


    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (oreInRange && Input.GetKeyDown(KeyCode.E)) //Change for controller input
        {
            startQuickTime();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            oreInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            oreInRange = false;
        }
    }

    private void startQuickTime()
    {
        canvas.SetActive(true);
    }
}