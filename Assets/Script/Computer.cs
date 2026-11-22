using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Computer;

public class Computer : MonoBehaviour, IInteractable
{

    [Header("Canvas ordinateur")]
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject computerCanvas;

    [Header("Panels ordinateur")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject plantPanel;
    [SerializeField] private GameObject buyPanel;


    [Header("Ressources Joueur")]
    [SerializeField] private GameObject playerMoneyText;

    [Header("Panier Shop")]
    [SerializeField] private GameObject basketText;

    [Header("Buy Shop")]
    [SerializeField] private GameObject buyRecapText;


    [Header("Variable Shop")]
    [SerializeField] private List<BasketItem> basket;
    [SerializeField] private int totalPrice;


    private GameObject player;

    public static event Action<List<GameObject>> OnItemPurchased;
    public class BasketItem
    {
        public ShopItemData shopItemData;
        public int quantity;
        public BasketItem(ShopItemData shopItemData)
        {
            this.shopItemData = shopItemData;
            quantity = 1;
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }


            BasketItem other = (BasketItem)obj;


            if (shopItemData == null || other.shopItemData == null)
            {
                return shopItemData == other.shopItemData;
            }


            return shopItemData.Equals(other.shopItemData);
        }


        public override int GetHashCode()
        {
            return shopItemData != null ? shopItemData.GetHashCode() : 0;
        }
    }


    private void OnEnable()
    {
        PlayerWallet.OnMoneyChanged += UpdatePlayerMoneyText;
    }
    private void OnDisable()
    {
        PlayerWallet.OnMoneyChanged -= UpdatePlayerMoneyText;
    }

    void Start()
    {
        computerCanvas.SetActive(false);
        basket = new List<BasketItem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        { 
            CloseShop();
        }
}
    private void UpdatePlayerMoneyText()
    {
        playerMoneyText.GetComponent<TextMeshProUGUI>().text = PlayerWallet.instance.money.ToString();
        Debug.Log(PlayerWallet.instance.money.ToString());
    }

    public void Interact()
    {
        OpenShop();
    }

    public bool CanInteract()
    {
        return true;
    }
    private void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        computerCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        player.GetComponent<PlayerMove>().enabled = false;
        UpdatePlayerMoneyText();
        UpdateBasket();
    }
    private void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        basket.Clear();
        player.GetComponent<PlayerMove>().enabled = true;
        buyRecapText.GetComponent<TextMeshProUGUI>().text = "";
        computerCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void SelectItem(ShopItemData itemData)
    {
        BasketItem newItem = new BasketItem(itemData);
        if (basket.Contains(newItem))
        {
            basket[basket.IndexOf(newItem)].quantity++;
        }
        else
        { 
            basket.Add(newItem); 
        }
            
        UpdateBasket();
    }

    private void UpdateBasket()
    {
        if (basket.Count<1)
        {
            basketText.gameObject.SetActive(false);
            return;
        }
        if(basket.Count>0)
        {
            basketText.gameObject.SetActive(true);
        }
        int nbItems=0;
        foreach (BasketItem item in basket)
        {
            nbItems += item.quantity;
        }
        basketText.GetComponent<TextMeshProUGUI>().text = nbItems.ToString();
        Debug.Log(basket.Count.ToString());
    }

    public void BasketClicked()
    {
        if (buyPanel.gameObject.activeSelf) { 
            buyPanel.gameObject.SetActive(false);
        }
        totalPrice = 0;
        buyPanel.SetActive(true);

        string resul="";
        foreach (BasketItem item in basket)
        {
            resul += $"{item.shopItemData.itemName}<pos=50%>{item.shopItemData.price} x {item.quantity}<pos=80%>= {item.shopItemData.price * item.quantity}€\n"; // <pos=xx%> place le texte correctement
            totalPrice += item.shopItemData.price *item.quantity;
        }
        resul += "\n------------------------------\nTotal Price : \t" + totalPrice;
        buyRecapText.GetComponent<TextMeshProUGUI>().text=resul;
    }

    public void Buy()
    {
        List<GameObject> temp= new List<GameObject>();
        if (PlayerWallet.instance.money >= totalPrice)
        {
            PlayerWallet.instance.RemoveMoney(totalPrice);
            foreach (BasketItem item in basket)
            {
                for (int i = 0; i < item.quantity; i++)
                {
                    {
                        temp.Add(item.shopItemData.itemPrefab);
                        
                    }
                }
            }
            OnItemPurchased?.Invoke(temp);

        }
    }

    public void ChangePanelToUpgrade()
    {
        upgradePanel.SetActive(true);
        itemPanel.SetActive(false);
        plantPanel.SetActive(false);
        buyPanel.SetActive(false);
    }
    public void ChangePanelToItem()
    {
        upgradePanel.SetActive(false);
        itemPanel.SetActive(true);
        plantPanel.SetActive(false);
        buyPanel.SetActive(false);
    }
    public void ChangePanelToPlants()
    {
        upgradePanel.SetActive(false); 
        itemPanel.SetActive(false); 
        plantPanel.SetActive(true);
        buyPanel.SetActive(false);
    }

} 
