using UnityEngine;

public class PlanteItem : Item
{
    [SerializeField] private GameObject plantePlantePrefab;

    public GameObject PlantePlantePrefab => plantePlantePrefab;
}
