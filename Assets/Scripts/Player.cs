using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int health = 100;
    private TextMeshProUGUI healthText;
    private GameManager gameManager;
    
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Transform bulletParent;
    public float shootInterval = 1f;
    public float attackRange = 10f;
    public float bulletSpeed = 20f;
    
    private float shootTimer = 0f;

    public int Health { get { return health; } }

    private void Start()
    {
        health = 100;
        UpdateHealthDisplay();
        gameManager = FindObjectOfType<GameManager>();
        shootTimer = shootInterval;
    }

    private void Update()
    {
        // Handle shooting
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            ShootAtEnemy();
            shootTimer = shootInterval;
        }
    }

    private void ShootAtEnemy()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Player bulletPrefab is not assigned.", this);
            return;
        }

        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy == null)
        {
            Debug.Log("No enemy in range to shoot.", this);
            return;
        }

        Vector3 shootPos = shootPoint != null ? shootPoint.position : transform.position;
        GameObject bullet = Instantiate(bulletPrefab, shootPos, Quaternion.identity, bulletParent != null ? bulletParent : null);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            Vector2 direction = (closestEnemy.transform.position - shootPos).normalized;
            bulletRb.linearVelocity = direction * bulletSpeed; // Bullet speed
        }
        else
        {
            Debug.LogWarning("Bullet prefab needs a Rigidbody2D component.", bullet);
        }
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closest = null;
        float closestDistance = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
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
