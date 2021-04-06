using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 5f;
    float distanceToPlayer = Mathf.Infinity;
    bool isProvoked = false;
    NavMeshAgent navMeshAgent;

    public float turnSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (isProvoked)
        {
            AttackTarget();
        }
        else if (distanceToPlayer < chaseRange)
        {
            isProvoked = true;
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    public void AgroAttack()
    {
        isProvoked = true;
    }
    private void AttackTarget()
    {
        FacePlayer();
        if (distanceToPlayer >= navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }
        if(distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            ShootTarget();
        }

    }
    private void ShootTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }
    private void ChasePlayer()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(player.position);
    } 
    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
