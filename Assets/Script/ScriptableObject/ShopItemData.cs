using UnityEngine;

[CreateAssetMenu(fileName = "NouvelItemShop", menuName = "Shop/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public int price;
    public int quantity;
    public Sprite sprite;
    public GameObject itemPrefab;

}
