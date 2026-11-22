using TMPro;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        Inventory.instance.takeItem(itemData);
        Destroy(gameObject);
    }
     
}
