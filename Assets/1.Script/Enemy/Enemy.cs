using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public EnemyType type;

    public LayerMask targetLayerMask;
    public LayerMask characterLayerMask;
    public float maxHp;
    public float curHp;

    //공격력
    public float attackPower;

    public NavMeshAgent agent;

    public float moveSpeed;
    public float atkSpeed;

    public Animator animator; // 캐릭터 애니메이션을 제어하는 컨포넌트
    Vector3 destinationPoint;
    Action arrivedCallback;

    public Target target;

    public Character character;

    public void Start()
    {
        FindTarget();
        FindCharacter();
    }

    public virtual void FindTarget()
    {
        
    }

    public virtual void FindCharacter()
    {

    }

    private void Update()
    {
       

        // 캐릭터가 특정 위치에 도착했다고 판별하는 if 코드
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("Walking", false);

                // 도착 후 타겟 혹은 캐릭터를 바라봄
                LookAtTarget();
                LookAtCharacter();

                if (arrivedCallback != null)
                {
                    // arrivedCallback 에 담겨있는 함수를 실행하라 (Invoke)
                    arrivedCallback.Invoke();
                    arrivedCallback = null;
                }


            }
        }

    }

    public virtual void LookAtTarget()
    {
        
    }

    public virtual void LookAtCharacter()
    {
        
    }

    public virtual void Attack()
    {
       
    }

    public void Arrived()
    {
        StartCoroutine(CoAttack());
    }
    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);

            Attack();
        }

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
    MeleeEnemy,//근거리
    RangedEnemy, //원거리
    
    //플레이어만 공격하는 적
    PlayerHunter
}
