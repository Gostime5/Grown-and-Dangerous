using UnityEngine;

public class FarmHealth : MonoBehaviour, IDamageable
{
    [Header("Statistiques")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void Die()
    {
        Debug.Log("Farm has been destroyed");
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
}
