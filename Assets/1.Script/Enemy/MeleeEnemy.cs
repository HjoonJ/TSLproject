using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {
        // Ÿ���� �̹� �Ҹ�Ǿ����� ���� ã��
        if (target == null)
        {
            FindTarget();
            return;
        }

        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayerMask);
        if (cols.Length <= 0)
        {
            // OverlapSphere�� �ƹ��� �浹ü�� ��ȯ���� ������ �� Ÿ�� ã��.
            FindTarget();

            return;
        }    
        // Į�� ��� �ִϸ��̼� �߰�


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Target")
            {
                // �ֵθ������� Target�� TakeDamage �Լ����ٰ� �Ű������� MelleEnemy�� attackPower�� �����ؾ���.
                Target t = cols[i].gameObject.GetComponent<Target>();
                if (t != null)
                {
                    t.TakeDamage(attackPower);
                }

                //cols[i].gameObject.GetComponent<Target>().TakeDamage(attackPower);
            }
        }
        

        //Debug.Log("Melee Enemy Attack!!");

    }
    public override void FindTarget()
    {
        StopAllCoroutines();

        // target ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        Target t = GameManager.Instance.GetClosestTarget(transform.position);

        // target ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        if (t != null)
        {
            target = t.transform;
        }


        if (target == null)
        {
            agent.isStopped = true;
            //Debug.Log("��ȿ�� Ÿ���� ����. 1�� �Ŀ� �ٽ� �õ�");
            Invoke("FindTarget", 1f);  // 1�� �Ŀ� �ٽ� Ÿ�� ã�� �õ�
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        MoveTo(target.transform.position, Arrived);

    }

    public override void LookAtTarget()
    {
        if (target != null)
        {
            // Ÿ�� ���� ���
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Y�� ȸ���� ����Ͽ� ȸ���� ����
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // ��� Ÿ�� �������� ȸ��
            transform.rotation = lookRotation;
        }
    }
}
