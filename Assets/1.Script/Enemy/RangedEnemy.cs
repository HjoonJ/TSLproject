using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [SerializeField] EnemyBullet enemyBulletPrefeb;

    public override void Attack()
    {
        // Ÿ���� �̹� �Ҹ�Ǿ����� ���� ã��
        if (target == null)
        {
            FindTarget();
            return;
        }

        EnemyBullet b = Instantiate(enemyBulletPrefeb);
        
        // Bullet ������ġ�� ���߿� �ѱ��� ��ȯ�ؾ���.
        b.transform.position = transform.position;

        //���ݷ� �� Ÿ���� ���޹���.
        b.Shoot(attackPower, target);

        //Debug.Log("Ranged Enemy Attack!!");

    }

    public override void FindTarget()
    {
        StopAllCoroutines();

        Target t = GameManager.Instance.GetClosestTarget(transform.position);

        // target ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        if ( t != null)
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
