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

    public Animator animator; // ĳ���� �ִϸ��̼��� �����ϴ� ������Ʈ
    Vector3 destinationPoint;
    Action arrivedCallback;

    public void Start()
    {
        // target ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        Target target = GameManager.Instance.GetClosestTarget(transform.position);


        MoveTo(GameManager.Instance.targets[0].transform.position, Arrived);
        

    }

    private void Update()
    {
       

        // ĳ���Ͱ� Ư�� ��ġ�� �����ߴٰ� �Ǻ��ϴ� if �ڵ�
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("Walking", false);

                if (arrivedCallback != null)
                {
                    // arrivedCallback �� ����ִ� �Լ��� �����϶� (Invoke)
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
    //�÷��̾ �����ϴ� ��
    PlayerHunter
}
