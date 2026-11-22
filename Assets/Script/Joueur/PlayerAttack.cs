using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private ItemData currentWeapon;

    [Header("Statistiques")]
    [SerializeField] private float currentDamage;
    [SerializeField] private float currentRange;
    [SerializeField] private float currentAttackeSpeed;

    [SerializeField] private bool canAttack;

    [Header("UI")]
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GameObject normalCrosshair;
    [SerializeField] private float hitMarkerDuration = 0.2f;

    void Start()
    {
        canAttack = true;
        currentWeapon = null;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentWeapon!=null && canAttack)
        {
            if(currentWeapon.isMeleeWeapon)
            {
                MeleeAttack();
            }
            else
            {
                RangeAttaque();
            }
            
        }
    }

    private void MeleeAttack()
    {
        //mettre animation ici
        //mettre son ici
        //mettre effet ici

        Vector3 origine = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        RaycastHit hit;
        
        if (Physics.Raycast(origine, direction, out hit, currentRange))
        {

            GameObject target = hit.collider.gameObject;
            if(target.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                StartCoroutine(HitMarker());
                damageable.TakeDamage(currentDamage);
            }
        }
        StartCoroutine(AttackCooldown());
    }

    private void RangeAttaque()
    {
        //mettre animation ici
        //mettre son ici
        //mettre effet ici
        
    }

    private IEnumerator HitMarker()
    {
        normalCrosshair.SetActive(false);
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(hitMarkerDuration);
        normalCrosshair.SetActive(true);
        hitMarker.SetActive(false);

    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1/currentAttackeSpeed);
        canAttack = true;
    }

    public void EquipWeapon(ItemData item)
    {
        currentWeapon = item;
        if(currentWeapon != null)
        {
            currentDamage = item.damage;
            currentRange = item.range;
            currentAttackeSpeed = item.attackSpeed;
        }
        
    }
}
