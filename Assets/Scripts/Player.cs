using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int health = 100;
    private TextMeshProUGUI healthText;

    public int Health { get { return health; } }

    private void Start()
    {
        health = 100;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        UpdateHealthDisplay();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(100, health + amount);
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    public void SetHealthText(TextMeshProUGUI text)
    {
        healthText = text;
        UpdateHealthDisplay();
    }

    private void Die()
    {
        // Handle player death
        Destroy(gameObject);
    }
}
