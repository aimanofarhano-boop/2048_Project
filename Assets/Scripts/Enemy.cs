using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private TextMeshProUGUI hpText;

    public int MaxHealth => maxHealth;
    public int Health { get; private set; }
    public event System.Action<Enemy> Defeated;

    private void Awake()
    {
        Health = maxHealth;
        ResolveHpText();
        UpdateHealthText();
    }

    private void OnValidate()
    {
        ResolveHpText();
        UpdateHealthText();
    }

    private void ResolveHpText()
    {
        if (hpText != null)
            return;

        hpText = GetComponentInChildren<TextMeshProUGUI>();

        if (hpText == null)
        {
            Transform valueTransform = transform.Find("enemyhp/value");
            if (valueTransform != null)
            {
                hpText = valueTransform.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    private void UpdateHealthText()
    {
        if (hpText != null)
        {
            hpText.text = maxHealth.ToString();
        }
    }

    public void Initialize(int health)
    {
        maxHealth = Mathf.Max(0, health);
        Health = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(Health - Mathf.Max(0, damage), 0);

        if (Health == 0)
        {
            Defeated?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
