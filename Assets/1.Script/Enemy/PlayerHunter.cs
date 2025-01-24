using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunter : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {

        // attackPower를 가지고 있음. 

        // 범위를 지정해서 공격?
        //코드로 충돌 확인
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, characterLayerMask);
        if (cols.Length <= 0)
        {
            // 가장 가까운 캐릭터 찾고 이동시키는 코드.
            FindCharacter();

            return;
        }
        // 칼로 써는 애니메이션 추가


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Character")
            {
                // 휘두를때마다 Character의 TakeDamage 함수에다가 매개변수로 PlayerHunter의 attackPower를 전달해야함.
                cols[i].gameObject.GetComponent<Character>().TakeDamage(attackPower);
            }
        }


        Debug.Log("Player Hunter Attack!!");

    }


    public override void FindCharacter()
    {
        StopAllCoroutines();

        // character 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        character = GameManager.Instance.GetClosestCharacter(transform.position);


        MoveTo(character.transform.position, Arrived);

    }

    public override void LookAtCharacter()
    {
        if (character != null)
        {
            // 캐릭터 방향 계산
            Vector3 direction = (character.transform.position - transform.position).normalized;

            // Y축 회전만 고려하여 회전값 설정
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 즉시 캐릭터 방향으로 회전
            transform.rotation = lookRotation;
        }
    }
}
