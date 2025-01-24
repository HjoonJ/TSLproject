using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleBehaviour : CharacterBehaviour
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform attackPoint;
    [SerializeField] int attackPower;

    public BattleBehaviour()
    {
        type = BehaviourType.Battle;

    }

    private void Start()
    {
        
    }


    public override void EnterBehaviour()
    {
        Character.Instance.StopMoving();
    }
    public override void UpdateBehaviour()
    {


    }

    public override void CompleteBehaviour()
    {
       



    }


    public void Attack()
    {

        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, Character.Instance.enemyLayerMask);
        if (cols.Length <= 0)
        {
            // ���� ����� �� ã�� �̵���Ű�� �ڵ�.
            FindEnemy();

            return;
        }
        // Į�� ��� �ִϸ��̼� �߰�


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Enemy")
            {
                // �ֵθ������� Character�� TakeDamage �Լ����ٰ� �Ű������� PlayerHunter�� attackPower�� �����ؾ���.
                cols[i].gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
            }
        }


        Debug.Log("Attack!!");

    }


    public void FindEnemy()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        Enemy enemy = EnemyManager.Instance.GetClosestEnemy(transform.position);


        //MoveTo(enemy.transform.position, Arrived);

    }

    public void LookAtEnemy()
    {
        //if (character != null)
        //{
        //    // ĳ���� ���� ���
        //    Vector3 direction = (character.transform.position - transform.position).normalized;

        //    // Y�� ȸ���� ����Ͽ� ȸ���� ����
        //    Quaternion lookRotation = Quaternion.LookRotation(direction);

        //    // ��� ĳ���� �������� ȸ��
        //    transform.rotation = lookRotation;
        //}
    }



}
