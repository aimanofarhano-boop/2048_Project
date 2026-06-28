using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;

    public int Damage => damage;

    public void setDamage(int value)
    {
        damage = value;
    }

    public float moveSpeed = 1.5f;
    public float destroyBelowY = -10f;
    private int health;
    private Rigidbody2D rb;
    public TMP_Text healthText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb != null)
        {
            // Override any physics impulses (e.g. from bullet hits) each frame
            rb.linearVelocity = Vector2.down * moveSpeed;
        }
        else
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(int startingHealth)
    {
        health = startingHealth;
        updateHealthText();
    }

    //public int Damage { get { return damage; } }

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