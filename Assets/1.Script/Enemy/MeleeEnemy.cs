using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float attackRange = 1f;
    public override void Attack()
    {
       // Į�� ��� �ִϸ��̼� �߰�

        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?


        // �ֵθ������� Target�� TakeDamage �Լ����ٰ� �Ű������� MelleEnemy�� attackPower�� �����ؾ���.

        Debug.Log("Attack!!");

    }
}
