using UnityEngine;

public interface IUpgradable
{
    public void AddUpgrade(UpgradeData upgrade); 

    public void RemoveUpgrade(UpgradeData upgrade);
}
