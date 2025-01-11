using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float attackRange = 1f;
    public override void Attack()
    {
       // 칼로 써는 애니메이션 추가

        // attackPower를 가지고 있음. 

        // 범위를 지정해서 공격?


        // 휘두를때마다 Target의 TakeDamage 함수에다가 매개변수로 MelleEnemy의 attackPower를 전달해야함.

        Debug.Log("Attack!!");

    }
}
