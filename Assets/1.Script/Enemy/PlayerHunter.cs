using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunter : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {

        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, characterLayerMask);
        if (cols.Length <= 0)
        {
            // ���� ����� ĳ���� ã�� �̵���Ű�� �ڵ�.
            FindCharacter();

            return;
        }
        // Į�� ��� �ִϸ��̼� �߰�


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Character")
            {
                // �ֵθ������� Character�� TakeDamage �Լ����ٰ� �Ű������� PlayerHunter�� attackPower�� �����ؾ���.
                cols[i].gameObject.GetComponent<Character>().TakeDamage(attackPower);
            }
        }


        Debug.Log("Player Hunter Attack!!");

    }


    public override void FindCharacter()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        character = GameManager.Instance.GetClosestCharacter(transform.position);


        MoveTo(character.transform.position, Arrived);

    }

    public override void LookAtCharacter()
    {
        if (character != null)
        {
            // ĳ���� ���� ���
            Vector3 direction = (character.transform.position - transform.position).normalized;

            // Y�� ȸ���� ����Ͽ� ȸ���� ����
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // ��� ĳ���� �������� ȸ��
            transform.rotation = lookRotation;
        }
    }
}
