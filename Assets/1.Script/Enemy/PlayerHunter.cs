using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunter : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Update()
    {
       base.Update();
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    public override void Attack()
    {
        // 캐릭터가 이미 hp=0이 되었으면 새로 찾음. -> 나중에 새로 짜야함.
        if (target == null)
        {
            FindTarget();
            return;
        }


        // attackPower를 가지고 있음. 

        // 범위를 지정해서 공격?
        //코드로 충돌 확인
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, characterLayerMask);
        if (cols.Length <= 0)
        {
            // OverlapSphere가 아무런 충돌체도 반환하지 않으면 새 캐릭터 찾기
            FindTarget();

            return;
        }
        // 칼로 써는 애니메이션 추가


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Character")
            {
                // 휘두를때마다 Character의 TakeDamage 함수에다가 매개변수로 PlayerHunter의 attackPower를 전달해야함.
                Character c = cols[i].gameObject.GetComponent<Character>();
                if (c != null)
                {
                    c.TakeDamage(attackPower);
                }

                //cols[i].gameObject.GetComponent<Character>().TakeDamage(attackPower);
            }
        }


        Debug.Log("Player Hunter Attack!!");

    }


    public override void FindTarget()
    {
        StopAllCoroutines();

        // character 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        target = GameManager.Instance.GetClosestCharacter(transform.position).transform;

        if (target == null)
        {
            Debug.Log("유효한 타겟이 없음. 1초 후에 다시 시도");
            Invoke("FindTarget", 1f);  // 1초 후에 다시 캐릭터 찾기 시도
            return;
        }

        MoveTo(target.transform.position, Arrived);

    }

    public override void LookAtCharacter()
    {
        if (target != null)
        {
            // 캐릭터 방향 계산
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Y축 회전만 고려하여 회전값 설정
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 즉시 캐릭터 방향으로 회전
            transform.rotation = lookRotation;
        }
    }
}
