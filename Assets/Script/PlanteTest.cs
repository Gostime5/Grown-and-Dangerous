using UnityEngine;
using UnityEngine.UIElements;

public class PlanteTest : MonoBehaviour, IPlante
{
    [Header("Plante enemmi script")]
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private float plantPrefab;


    public void OnEnable()
    {
        TimeManager.OnNightStarted += MutateToEnemy;
    }
    
    public void OnDisable()
    {
        TimeManager.OnNightStarted -= MutateToEnemy;
    }

    public void Plant()
    {
        
    }

    public void MutateToEnemy()
    {
        Debug.Log("PlanteTest : mutateToEnemy");
        Vector3 pos = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
        transform.GetComponentInParent<PlantableField>().ResetField();
        Destroy(gameObject);
    }
}
