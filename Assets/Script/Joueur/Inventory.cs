using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Inventory : MonoBehaviour
{
    [Header("Inventaire")]
    [SerializeField] private ItemData[] items;
    [SerializeField] private int currentItemIndex = 0;


    [Header("UI Hotbar")]
    [SerializeField] private Image slot1;
    [SerializeField] private Image slot2;
    [SerializeField] private Image slot3;

    [SerializeField] private GameObject panelSlot1;         // ces panels seront à retirer quand j'aurais les graphics de la hotbar
    [SerializeField] private GameObject panelSlot2;
    [SerializeField] private GameObject panelSlot3;

    [Header("Main Joueur")]
    [SerializeField] private GameObject playerHand;

    private PlayerAttack playerAttack;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        items = new ItemData[3];
        panelSlot1.SetActive(true);
        panelSlot2.SetActive(false);
        panelSlot3.SetActive(false);
        
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItemIndex = 0;
            panelSlot1.SetActive(true);             // remplacer ces lignes par un changement de couleur du slot quand j'aurais les graphics de la hotbar
            panelSlot2.SetActive(false);
            panelSlot3.SetActive(false);
            HoldingItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItemIndex = 1;
            panelSlot1.SetActive(false);            // remplacer ces lignes par un changement de couleur du slot quand j'aurais les graphics de la hotbar
            panelSlot2.SetActive(true);
            panelSlot3.SetActive(false);
            HoldingItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItemIndex = 2;
            panelSlot1.SetActive(false);            // remplacer ces lignes par un changement de couleur du slot quand j'aurais les graphics de la hotbar
            panelSlot2.SetActive(false);
            panelSlot3.SetActive(true);
            HoldingItem();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Drop Item");
            DropCurrentItem();
        }
    }

    public void takeItem(ItemData itemData)
    {
        DropCurrentItem();
        items[currentItemIndex] = itemData;
        UpdateInventory();
        HoldingItem();
    }
    private void DropCurrentItem()
    {
        if (items[currentItemIndex] != null)
        {
            Quaternion rotation = Quaternion.Euler(items[currentItemIndex].equipRotation)*Camera.main.transform.rotation;
            rotation.x = 0;
            Instantiate(items[currentItemIndex].prefab, transform.position+transform.forward, rotation);
            items[currentItemIndex] = null;
        }
        UpdateInventory();
        HoldingItem();
    }
    private void UpdateInventory()
    {
        slot1.sprite = items[0] != null ? items[0].inventoryImage : null;   // explication de la ligne : si items[0] n'est pas null, alors on met l'image de l'item dans le slot1, sinon on met null (donc pas d'image)
        slot2.sprite = items[1] != null ? items[1].inventoryImage : null;
        slot3.sprite = items[2] != null ? items[2].inventoryImage : null;

    }
    private void HoldingItem()
    {
        foreach (Transform child in playerHand.transform)
        {
            Destroy(child.gameObject);
        }
        if (items[currentItemIndex] == null) { return; }
        
        GameObject item = Instantiate(items[currentItemIndex].equipPrefab, playerHand.transform.position, transform.rotation, playerHand.transform);
        item.transform.Rotate(items[currentItemIndex].equipRotation);
        playerAttack.EquipWeapon(items[currentItemIndex]);
    }

    public bool IsHolding()
    {
        return items[currentItemIndex] != null;
    }
    public ItemData GetCurrentItem()
    {
        return items[currentItemIndex];
    }

    public void RemoveCurrentItem()
    {
        items[currentItemIndex] = null;
        UpdateInventory();
        HoldingItem();
        
    }

}
