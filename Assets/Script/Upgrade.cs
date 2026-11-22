using UnityEngine;

public class Upgrade : MonoBehaviour, IUsable
{

    [SerializeField] private UpgradeData upgradeData;
    public bool CanUse()
    {
        return true;
    }

    public void Use()
    {
        PlayerStat.instance.AddUpgrade(upgradeData);
        Destroy(gameObject);
    }
}
