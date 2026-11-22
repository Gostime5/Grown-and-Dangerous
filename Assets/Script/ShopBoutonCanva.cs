using TMPro;
using UnityEngine;

public class ShopBoutonCanva : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ShopItemData shopItemData;

    private void OnEnable()
    {
        text.text=shopItemData.price.ToString();
    }
}
