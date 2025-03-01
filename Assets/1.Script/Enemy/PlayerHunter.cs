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
        // ĳ���Ͱ� �̹� hp=0�� �Ǿ����� ���� ã��. -> ���߿� ���� ¥����.
        if (target == null)
        {
            FindTarget();
            return;
        }


        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, characterLayerMask);
        if (cols.Length <= 0)
        {
            // OverlapSphere�� �ƹ��� �浹ü�� ��ȯ���� ������ �� ĳ���� ã��
            FindTarget();

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


    public override void FindTarget()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        target = GameManager.Instance.GetClosestCharacter(transform.position).transform;

        if (target == null)
        {
            Debug.Log("��ȿ�� Ÿ���� ����. 1�� �Ŀ� �ٽ� �õ�");
            Invoke("FindTarget", 1f);  // 1�� �Ŀ� �ٽ� ĳ���� ã�� �õ�
            return;
        }

        MoveTo(target.transform.position, Arrived);

    }

    public override void LookAtCharacter()
    {
        if (target != null)
        {
            // ĳ���� ���� ���
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Y�� ȸ���� ����Ͽ� ȸ���� ����
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // ��� ĳ���� �������� ȸ��
            transform.rotation = lookRotation;
        }
    }
}
