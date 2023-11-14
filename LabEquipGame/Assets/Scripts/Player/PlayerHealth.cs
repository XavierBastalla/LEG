using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private PlayerMotor playerMotor;

    public AudioSource damageSound;

    public Slider healthSlider; // Reference to your UI health slider
    [SerializeField]
    public TextMeshProUGUI healthText; // Reference to your UI text element

    void Start()
    {
        currentHealth = maxHealth;
        playerMotor = GetComponent<PlayerMotor>();

        // Initialize UI health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Initialize UI health text
        if (healthText != null)
        {
            UpdateHealthText();
        } else
        {
            Debug.Log("health text not found");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (damageSound != null)
        {
            damageSound.Play();
        }

        // Update UI health slider
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Update UI health text
        if (healthText != null)
        {
            UpdateHealthText();
        }

        if (currentHealth > 0)
        {
            playerMotor.Knockback();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void Die()
    {
        // Implement death actions (e.g., play death animation, show game over screen, etc.)
        // For now, let's just print a message and reload the scene
        Debug.Log("Player died!");
        // Reload the scene or trigger game over logic
        // Example: SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}