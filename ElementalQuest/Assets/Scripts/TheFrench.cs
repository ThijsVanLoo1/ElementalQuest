using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheFrench : MonoBehaviour
{
    public Slider healthbar;
    public AudioSource hit;
    
    // Attack player function
    public void AttackPlayer()
    {
        // Decrease health by one
        healthbar.value--;
        hit.Play();

        // If no health left, reload scene
        if(healthbar.value < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
