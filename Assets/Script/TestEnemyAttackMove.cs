using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyAttackMove : GenericEnemyAttack
{
    private EnemyStats enemyStats;

    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent agent;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject farm;

    private bool canAttack;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        farm = GameObject.FindGameObjectWithTag("Farm");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyStats.currentSpeed;
        SearchEnemy();
        canAttack = true;

        

    }
    private void Update()
    {
        SearchEnemy();
        if (target != null)
        {
            Debug.Log("target != null");
            Vector3 dir = target.transform.position - transform.position;
            if (dir.sqrMagnitude <= (enemyStats.currentAttackRange * enemyStats.currentAttackRange) && canAttack)
            {
                Debug.Log("Attackrange is ok");
                Attack();
            }
            else { agent.SetDestination(target.transform.position); }
                        
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackCooldown());
        target.TryGetComponent<IDamageable>(out IDamageable damageable);
        damageable.TakeDamage(enemyStats.currentAttackDamage);
        Debug.Log("Attack");
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f / enemyStats.currentAttackSpeed);
        canAttack = true;
    }

    public override void SearchEnemy()
    {
        Vector3 dirPlayer= player.transform.position-transform.position;
        Vector3 dirFarm= farm.transform.position-transform.position;
        target = (dirPlayer.sqrMagnitude < dirFarm.sqrMagnitude) ? player : farm;
        agent.SetDestination(target.transform.position);
        
    }


    
}
