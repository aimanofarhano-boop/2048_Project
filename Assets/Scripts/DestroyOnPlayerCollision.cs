using UnityEngine;

public class DestroyOnPlayerCollision : MonoBehaviour
{
    public GameObject playerObject;
    public string playerTag = "Player";

    private bool IsPlayerObject(GameObject obj)
    {
        if (obj == null)
            return false;

        if (playerObject != null && obj == playerObject)
            return true;

        if (!string.IsNullOrEmpty(playerTag) && obj.CompareTag(playerTag))
            return true;

        if (obj.name.StartsWith("PlayerObject"))
            return true;

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayerObject(collision.gameObject))
        {
            DamagePlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayerObject(other.gameObject))
        {
            DamagePlayer(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void DamagePlayer(GameObject playerGO)
    {
        Player player = playerGO.GetComponent<Player>();
        if (player != null)
        {
            Enemy enemy = GetComponent<Enemy>();
            int damageAmount = enemy != null ? enemy.Damage : 10;
            player.TakeDamage(damageAmount);
        }
    }
}
