using System.Collections.Generic;
using UnityEngine;

public class Deliverer : MonoBehaviour
{
    [SerializeField] private GameObject cratePrefab;
    private void OnEnable()
    {
        Computer.OnItemPurchased += PreparePackage;
    }
    private void OnDisable()
    {
        Computer.OnItemPurchased -= PreparePackage;
    }
    private void PreparePackage(List<GameObject> item)
    {
        Vector3 pos = new Vector3(-2,2,-13);    // à modifier plus tard avec une delivery zone (peut-être)
        GameObject package = Instantiate(cratePrefab, pos, Quaternion.identity);
        package.GetComponent<Package>().itemPrefab = item;
    }
}
