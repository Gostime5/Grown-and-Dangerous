using UnityEngine;

[CreateAssetMenu(fileName = "NouvelEnnemi", menuName = "Ennemi/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Statistiques")]
    public float maxHp;
    public float currentHp;

    public float baseSpeed;
    public float currentSpeed;

    public float baseAttackDamage;
    public float currentAttackDamage;
}
