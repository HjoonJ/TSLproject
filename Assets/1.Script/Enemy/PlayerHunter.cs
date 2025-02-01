using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunter : Enemy
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;

    public override void Attack()
    {
        // ĳ���Ͱ� �̹� hp=0�� �Ǿ����� ���� ã��. -> ���߿� ���� ¥����.
        if (character == null)
        {
            FindCharacter();
            return;
        }


        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, characterLayerMask);
        if (cols.Length <= 0)
        {
            // OverlapSphere�� �ƹ��� �浹ü�� ��ȯ���� ������ �� ĳ���� ã��
            FindCharacter();

            return;
        }
        // Į�� ��� �ִϸ��̼� �߰�


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Character")
            {
                // �ֵθ������� Character�� TakeDamage �Լ����ٰ� �Ű������� PlayerHunter�� attackPower�� �����ؾ���.
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


    public override void FindCharacter()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        character = GameManager.Instance.GetClosestCharacter(transform.position);

        if (character == null)
        {
            Debug.Log("��ȿ�� Ÿ���� ����. 1�� �Ŀ� �ٽ� �õ�");
            Invoke("FindCharacter", 1f);  // 1�� �Ŀ� �ٽ� ĳ���� ã�� �õ�
            return;
        }

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
