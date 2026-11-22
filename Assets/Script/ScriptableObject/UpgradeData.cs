using UnityEngine;

[CreateAssetMenu(fileName = "NouvelAmélioration", menuName = "Amélioration/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [Header("Statistiques")]
    public int healthModifier;
    public int damaModifier;
    public float speedModifier;
    public float jumpForceModifier;
}
