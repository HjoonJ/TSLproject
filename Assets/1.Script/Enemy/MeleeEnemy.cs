using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {

        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayerMask);
        if (cols.Length <= 0)
        {
            // ���� ����� Ÿ�� ã�� �̵���Ű�� �ڵ� ¥����.
            FindTarget();

            return;
        }    
        // Į�� ��� �ִϸ��̼� �߰�


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Target")
            {
                // �ֵθ������� Target�� TakeDamage �Լ����ٰ� �Ű������� MelleEnemy�� attackPower�� �����ؾ���.
                cols[i].gameObject.GetComponent<Target>().TakeDamage(attackPower);
            }
        }
        

        Debug.Log("Attack!!");

    }
    public override void FindTarget()
    {
        StopAllCoroutines();

        // target ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        target = GameManager.Instance.GetClosestTarget(transform.position);


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
