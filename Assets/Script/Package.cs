using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour, IInteractable
{
    public List<GameObject> itemPrefab;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        foreach (var item in itemPrefab)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
