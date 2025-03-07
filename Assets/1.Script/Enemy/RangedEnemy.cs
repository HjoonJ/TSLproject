using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [SerializeField] EnemyBullet enemyBulletPrefeb;

    public override void Attack()
    {
        // 타겟이 이미 소멸되었으면 새로 찾음
        if (target == null)
        {
            FindTarget();
            return;
        }

        EnemyBullet b = Instantiate(enemyBulletPrefeb);
        
        // Bullet 시작위치를 나중에 총구로 변환해야함.
        b.transform.position = transform.position;

        //공격력 및 타켓을 전달받음.
        b.Shoot(attackPower, target);

        //Debug.Log("Ranged Enemy Attack!!");

    }

    public override void FindTarget()
    {
        StopAllCoroutines();

        Target t = GameManager.Instance.GetClosestTarget(transform.position);

        // target 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        if ( t != null)
        {
            target = t.transform;
        }

        if (target == null)
        {
            agent.isStopped = true;
            Debug.Log("유효한 타겟이 없음. 1초 후에 다시 시도");
            Invoke("FindTarget", 1f);  // 1초 후에 다시 타겟 찾기 시도
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
            // 타겟 방향 계산
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Y축 회전만 고려하여 회전값 설정
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 즉시 타겟 방향으로 회전
            transform.rotation = lookRotation;
        }
    }

}
