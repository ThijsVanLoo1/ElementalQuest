using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheFrench : MonoBehaviour
{
    public Slider healthbar;
    public AudioSource hit;
    
    public void AttackPlayer()
    {
        healthbar.value--;
        hit.Play();

        if(healthbar.value < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
