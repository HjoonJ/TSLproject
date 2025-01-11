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

}
