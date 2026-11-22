using UnityEngine;

public class PlantableField : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        GameObject plantePrefab = Inventory.instance.GetCurrentItem().prefab.GetComponent<PlanteItem>().PlantePlantePrefab;
        Instantiate(plantePrefab, transform.position, Quaternion.identity, transform);
        Inventory.instance.RemoveCurrentItem();

        GetComponent<Collider>().enabled = false;
    }

    public void ResetField()
    {
        GetComponent<Collider>().enabled = true;
    }

    public bool CanInteract()
    {
        Debug.Log(Inventory.instance.IsHolding() && Inventory.instance.GetCurrentItem().prefab.TryGetComponent<PlanteItem>(out PlanteItem f));
        return Inventory.instance.IsHolding() && Inventory.instance.GetCurrentItem().prefab.TryGetComponent<PlanteItem>(out PlanteItem plante);
    }
}
