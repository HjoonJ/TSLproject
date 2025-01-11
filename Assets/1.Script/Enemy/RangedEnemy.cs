using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [SerializeField] EnemyBullet enemyBulletPrefeb;

    public override void Attack()
    {
        EnemyBullet b = Instantiate(enemyBulletPrefeb);
        // Bullet 시작위치를 나중에 총구로 변환해야함.
        b.transform.position = transform.position;

        //공격력 및 타켓을 전달받음.
        b.Shoot(attackPower, target);

        Debug.Log("Attack!!");

    }

}
