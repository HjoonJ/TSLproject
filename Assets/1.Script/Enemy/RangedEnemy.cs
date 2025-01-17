using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [SerializeField] EnemyBullet enemyBulletPrefeb;

    public override void Attack()
    {
        EnemyBullet b = Instantiate(enemyBulletPrefeb);
        
        // Bullet ������ġ�� ���߿� �ѱ��� ��ȯ�ؾ���.
        b.transform.position = transform.position;

        //���ݷ� �� Ÿ���� ���޹���.
        b.Shoot(attackPower, target);

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
