using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int damage = 5;
    private int health;

    public TMP_Text healthText;

    public void Initialize(int startingHealth)
    {
        health = startingHealth;
        updateHealthText();
    }

    public int Damage { get { return damage; } }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        updateHealthText();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void updateHealthText()
    {
        if(healthText != null)
        {
            healthText.text = health.ToString();
        }
    }
}