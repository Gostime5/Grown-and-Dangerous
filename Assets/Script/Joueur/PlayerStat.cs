using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageable, IUpgradable
{
    [Header("Statistiques de vie")]
    [SerializeField] private float baseHp;
    [SerializeField] private float healthModifier;
    [SerializeField] private float currentHealth;

    [Header("Statistiques de vitesse")]
    public float baseSpeed;
    public float sprintMultiplier;
    public float speedModifier;
    public float baseJumpForce;
    public float jumpForceModifier;

    [Header("Statistiques de dégât")]
    public float baseDamage;
    public float damageModifier;

    public static PlayerStat instance;

    public void Start()
    {
        currentHealth = baseHp + healthModifier;
        instance = this;
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddUpgrade(UpgradeData upgrade)
    {
        healthModifier += upgrade.healthModifier;
        speedModifier += upgrade.speedModifier;
        damageModifier += upgrade.damaModifier;
        jumpForceModifier += upgrade.jumpForceModifier;
    }


    public void RemoveUpgrade(UpgradeData upgrade)
    {
        healthModifier -= upgrade.healthModifier;
        speedModifier -= upgrade.speedModifier;
        damageModifier -= upgrade.damaModifier;
        jumpForceModifier -= upgrade.jumpForceModifier;
    }
}
