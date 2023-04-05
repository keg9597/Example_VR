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
        WALK,
        ATTACK,
        DAMAGE,
        DAED
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
    public float zombieSpeed = 2f;
    //[Range(0,5)]
    //public float zombie

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        enemyState = ENEMYSTATE.IDLE;
    }

    void Update()
    {
        switch (enemyState)
        {
            case ENEMYSTATE.NONE:
                break;
            case ENEMYSTATE.IDLE:
                break;
            case ENEMYSTATE.WALK:
                break;
            case ENEMYSTATE.ATTACK:
                break;
            case ENEMYSTATE.DAMAGE:
                break;
            case ENEMYSTATE.DAED:
                break;
            default:
                break;
        }
    }
}
