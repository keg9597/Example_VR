using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum ENEMYSTATE
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD
    }
    [Header("에너미상태")]
    public ENEMYSTATE enemyState;

    private NavMeshAgent agent;
    private Animator anim;

    [Header("타겟 플레이어")]
    public Transform target;

    [Header("히트 이펙트 & 아이템")]
    public GameObject hitEffect;
    public GameObject item;

    [Header("Zombie")]
    [Range(0, 5)]
    public float zombieSpeed = 2f;
    public float attackRange = 1.5f;
    public float stateTime;
    public float idleStateTime = 2f;
    public float attackStateTime = 1.5f;
    public float damageStateTime = 1.5f;
    public int hp = 5;

    [Header("LoockOn")]
    public bool isLockedOn = false;
    public float lockTime;
    public float lockCoolTime = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        enemyState = ENEMYSTATE.IDLE;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        switch (enemyState)
        {
            case ENEMYSTATE.NONE:
                GetComponent<CapsuleCollider>().enabled = false;
                break;
            case ENEMYSTATE.IDLE:              
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.speed = 0;
                stateTime += Time.deltaTime;
                if(stateTime > idleStateTime)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }   
                break;
            case ENEMYSTATE.MOVE:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.SetDestination(target.position);
                agent.speed = zombieSpeed;
                float dist = Vector3.Distance(target.position, transform.position);
                if(dist < attackRange)
                {
                    enemyState = ENEMYSTATE.ATTACK;
                }
                else
                {
                    agent.speed = zombieSpeed;
                }
                break;
            case ENEMYSTATE.ATTACK:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.speed = 0;
                stateTime += Time.deltaTime;
                if(stateTime > attackStateTime)
                {
                    stateTime = 0;
                    Debug.Log("공격");
                }
                break;
            case ENEMYSTATE.DAMAGE:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                stateTime += Time.deltaTime;
                agent.speed = 0;
                if(stateTime > damageStateTime)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }
                if( hp <= 0)
                {
                    enemyState = ENEMYSTATE.DEAD;
                }
                break;
            case ENEMYSTATE.DEAD:
                anim.SetTrigger("DEAD");
                enemyState = ENEMYSTATE.NONE;
                Destroy(gameObject, 2.2f);
                break;
            default:
                break;
        }

        if(isLockedOn && enemyState != ENEMYSTATE.DEAD)
        {
            lockTime += Time.deltaTime;
            if(lockTime > lockCoolTime)
            {
                lockTime = 0;
                Instantiate(hitEffect, transform.position, transform.rotation);
                DamageByPlayer();
                stateTime = 0;
                enemyState = ENEMYSTATE.DAMAGE;
            }
        }
    }

    public void AimEnter()
    {
        isLockedOn = true;
    }

    public void AimExit()
    {
        isLockedOn = false;
        lockTime = 0;
    }

    void DamageByPlayer()
    {
        --hp;
        if(hp <= 0)
        {
            enemyState = ENEMYSTATE.DEAD;
        }
    }
}
