using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("Statistiques")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    [SerializeField] private float baseSpeed;
    public float currentSpeed;

    [SerializeField] private float baseAttackDamage;
    public float currentAttackDamage;

    [SerializeField] private float baseAttackSpeed;
    public float currentAttackSpeed;

    [SerializeField] private float baseAttackRange;
    public float currentAttackRange;


    private void Awake()
    {
        currentHp = maxHp;
        currentSpeed = baseSpeed;
        currentAttackDamage = baseAttackDamage;
        currentAttackSpeed = baseAttackSpeed;
        currentAttackRange = baseAttackRange;
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHp-=damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
}
