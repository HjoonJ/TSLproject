using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {
        // 타겟이 이미 소멸되었으면 새로 찾음
        if (target == null)
        {
            FindTarget();
            return;
        }

        // attackPower를 가지고 있음. 

        // 범위를 지정해서 공격?
        //코드로 충돌 확인
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayerMask);
        if (cols.Length <= 0)
        {
            // OverlapSphere가 아무런 충돌체도 반환하지 않으면 새 타겟 찾기.
            FindTarget();

            return;
        }    
        // 칼로 써는 애니메이션 추가


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Target")
            {
                // 휘두를때마다 Target의 TakeDamage 함수에다가 매개변수로 MelleEnemy의 attackPower를 전달해야함.
                Target t = cols[i].gameObject.GetComponent<Target>();
                if (t != null)
                {
                    t.TakeDamage(attackPower);
                }

                //cols[i].gameObject.GetComponent<Target>().TakeDamage(attackPower);
            }
        }
        

        Debug.Log("Melee Enemy Attack!!");

    }
    public override void FindTarget()
    {
        StopAllCoroutines();

        // target 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        target = GameManager.Instance.GetClosestTarget(transform.position);

        if (target == null)
        {
            Debug.Log("유효한 타겟이 없음. 1초 후에 다시 시도");
            Invoke("FindTarget", 1f);  // 1초 후에 다시 타겟 찾기 시도
            return;
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
