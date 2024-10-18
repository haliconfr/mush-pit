using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player, idlewalk1, entry;
    public GameObject enemy;
    public LayerMask ground, whatIsPlayer, whatIsEntry;
    private CharacterController me;
    public Animation anim;
    public bool playerInRange;
    Vector3 stopped = new Vector3(0, 0, 0);

    //patrolling
    Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    
    //attacking
    public float timeBetweenAttacks;

    //states
    public float sightRange, attackRange, entryRange;
    public EnemyState CurrentState = EnemyState.Idling;

    void Awake()
    {
        me = GetComponent<CharacterController>();
        entry = GameObject.FindWithTag("entry").transform;
        agent.SetDestination(entry.position);
        
    }
    public enum EnemyState
    {
        Idling,Walking,Chasing,Breaking,Attacking
    }
    
    void AttackPlayer()
    {
        CurrentState = EnemyState.Attacking;
        anim.CrossFade("attack", 0.1f);
        agent.SetDestination(transform.position);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
        CurrentState = EnemyState.Chasing;
    }

    private void SetIdling()
    {
        agent.SetDestination(idlewalk1.position);
        StartCoroutine(idlepattern());
    }

    private void SetWalking()
    {
        CurrentState = EnemyState.Walking;
        agent.SetDestination(idlewalk1.position);
        StartCoroutine(idlepattern());
    }
    private void Update()
    {
        Debug.Log(CurrentState);
        if (!anim.IsPlaying("attack"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, attackRange, whatIsPlayer))
            {
                Debug.Log("attacking");
                AttackPlayer();
                return;
            }
        }
        else
            return;


        switch (CurrentState)
        {
            case EnemyState.Chasing:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, sightRange, whatIsPlayer))
                {
                    ChasePlayer();
                }
                break;
            case EnemyState.Walking:
            case EnemyState.Attacking:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, sightRange, whatIsPlayer))
                {
                    ChasePlayer();
                    return;
                }
                break;
        }

        if (CurrentState != EnemyState.Breaking)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, entryRange, whatIsEntry))
            {
                BreakIn();
                return;
            }
        }

        if (transform.position == idlewalk1.position)
        {
            agent.velocity = stopped;
            SetIdling();
        }

        switch (CurrentState)
        {
            case EnemyState.Idling:
                anim["base idle"].speed = 0.3f;
                anim.CrossFade("base idle", 0.2f);
                break;
            case EnemyState.Walking:
                if (transform.position != idlewalk1.position)
                {
                    if (!anim.IsPlaying("walk"))
                    {
                        anim["walk"].speed = 0.9f;
                        anim.CrossFade("walk", 0.1f);
                    }
                }
                break;
            case EnemyState.Chasing:
                if (!anim.IsPlaying("chase"))
                {
                    anim["chase"].speed = 1.5f;
                    anim.CrossFade("chase", 0.1f);
                    agent.speed = 2.5f;
                }
                break;
            case EnemyState.Attacking:
                if (!anim.IsPlaying("attack"))
                {
                    anim.CrossFade("attack", 0.1f);
                }
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, attackRange, whatIsPlayer))
                {
                    Debug.Log("attacking");
                    AttackPlayer();
                    return;
                }
                else
                {
                    CurrentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Breaking:
                break;
        }


           
        

    }
    IEnumerator idlepattern()
    {
        yield return new WaitForSeconds(Random.Range(10, 14));
        idlewalk1.position = new Vector3(transform.position.x + Random.Range(-14, 14), transform.position.y, transform.position.z + Random.Range(-14, 14));
        agent.SetDestination(idlewalk1.position);
   }

    
    void BreakIn()
    {
        
        if (!anim.IsPlaying("break through"))
        {
            if (entry.GetComponent<activateSeal>().seal == true)
            {
                CurrentState = EnemyState.Breaking;
                Debug.Log("break");
                agent.SetDestination(transform.position);
                transform.LookAt(entry);
                anim.Play("break through");
            }
        }
    }
    private void LateUpdate()
    {
        if (CurrentState == EnemyState.Breaking)
        {
            if (!anim.IsPlaying("break through"))
            {
                transform.position = enemy.transform.position;
                entryRange = 0;
                SetWalking();
            }
        }
    }
}
