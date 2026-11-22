using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int money { private set; get; }
    [SerializeField] private int moneyIndicatorForInspectorDEBUG;
    public static event Action OnMoneyChanged;
    public static PlayerWallet instance { get; private set; }
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        moneyIndicatorForInspectorDEBUG=money;
        if (Input.GetKeyUp(KeyCode.R))
        {
            AddMoney(100);
        }
    }
    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        if(money < 0)
        {
            money = 0;
        }
        OnMoneyChanged?.Invoke();
    }

}
