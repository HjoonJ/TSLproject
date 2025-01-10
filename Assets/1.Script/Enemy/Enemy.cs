using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public EnemyType type;

    public float maxHp;
    public float curHp;
    
    public NavMeshAgent agent;

    public Animator animator; // 캐릭터 애니메이션을 제어하는 컨포넌트
    Vector3 destinationPoint;
    Action arrivedCallback;

    public void Start()
    {
        // target 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        Target target = GameManager.Instance.GetClosestTarget(transform.position);


        MoveTo(GameManager.Instance.targets[0].transform.position, Arrived);
        

    }

    private void Update()
    {
       

        // 캐릭터가 특정 위치에 도착했다고 판별하는 if 코드
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("Walking", false);

                if (arrivedCallback != null)
                {
                    // arrivedCallback 에 담겨있는 함수를 실행하라 (Invoke)
                    arrivedCallback.Invoke();
                    arrivedCallback = null;
                }


            }
        }

    }

    public void Arrived()
    {

    }

    public void MoveTo(Vector3 des, Action aCallback = null)
    {
        arrivedCallback = aCallback;
        agent.isStopped = false;
        animator.SetBool("Walking", true);
        destinationPoint = des;
        agent.SetDestination(destinationPoint);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public virtual void Spawn(Vector3 startPos) // start position
    {
        transform.position = startPos;
    }
    public virtual void TakeDamage(int d)
    {
        curHp -= d;
        if (curHp <= 0)
        {
            Destroy(gameObject);   
        }
    }
}

public enum EnemyType
{
    MeleeEnemy,
    RangedEnemy,
    //플레이어만 공격하는 적
    PlayerHunter
}
