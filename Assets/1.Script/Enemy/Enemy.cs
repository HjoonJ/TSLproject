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

    //���ݷ�
    public float attackPower;

    public NavMeshAgent agent;

    public float moveSpeed;
    public float atkSpeed;

    //public Animator animator; // ĳ���� �ִϸ��̼��� �����ϴ� ������Ʈ
    
    Vector3 destinationPoint;
    Action arrivedCallback;

    public Target target;

    public Character character;

    public void Start()
    {
        curHp = maxHp;

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
       

        // ĳ���Ͱ� Ư�� ��ġ�� �����ߴٰ� �Ǻ��ϴ� if �ڵ�
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                //�̵� �ִϸ��̼� ����.
                //animator.SetBool("Walking", false);

                // ���� �� Ÿ�� Ȥ�� ĳ���͸� �ٶ�
                LookAtTarget();
                LookAtCharacter();

                if (arrivedCallback != null)
                {
                    // arrivedCallback �� ����ִ� �Լ��� �����϶� (Invoke)
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
        Debug.Log("�� ����");
        StartCoroutine(CoAttack());
    }
    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);

            Debug.Log("�� ����!!");
            Attack();
        }

    }

    public void MoveTo(Vector3 des, Action aCallback = null)
    {
        arrivedCallback = aCallback;
        agent.isStopped = false;

        //�̵� �ִϸ��̼� ����.
        //animator.SetBool("Walking", true);

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
            EnemyManager.Instance.enemies.Remove(this);
            Destroy(gameObject);

        }
    }
}

public enum EnemyType
{
    MeleeEnemy,//�ٰŸ�
    RangedEnemy, //���Ÿ�
    
    //�÷��̾ �����ϴ� ��
    PlayerHunter
}
