using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    private int health = 50;

    public int Damage { get { return damage; } }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
