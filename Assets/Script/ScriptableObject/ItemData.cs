using UnityEngine;

[CreateAssetMenu(fileName = "NouvelItem", menuName = "Inventaire/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite inventoryImage;
    public GameObject prefab;
    public GameObject equipPrefab;
    public Vector3 equipRotation; //rotation de l'arme quand elle est equipée, pour la mettre dans la bonne position

    [Header("Stats de l'item")]
    public float damage;
    public float range;
    public float attackSpeed;
    public bool isMeleeWeapon;

}
