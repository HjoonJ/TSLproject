using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleBehaviour : CharacterBehaviour
{

    public Enemy enemy;

    public BattleBehaviour()
    {
        type = BehaviourType.Battle;

    }

    private void Start()
    {
        //FindEnemy();
    }


    public override void EnterBehaviour()
    {

        character.animator.Play("Idle");

        character.StopMoving();
        // �ִϸ��̼� ����

        FindEnemy();
    }
    public override void UpdateBehaviour()
    {
        
        
    }

    public override void CompleteBehaviour()
    {
       



    }


    public void FindEnemy()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        enemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

     

        character.MoveTo(enemy.transform.position, Arrived);

    }

    public void Arrived()
    {
        Debug.Log("������ ����");
        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(character.attackSpeed);

            // Ÿ���� �Ҹ�� ���
            if (enemy == null)
            {
                Debug.Log("Ÿ���� �Ҹ�, �� Ÿ�� ã��");
                FindEnemy();
                continue;  // �ڷ�ƾ ���� (���ο� Ÿ�� Ž�� �� ���� CoAttack�� ����.)
            }


            Debug.Log("�� ����!!");
            // ĳ���Ͱ� ���� Attack() �Լ� ȣ���ϰ� enemy ����
            character.Attack(enemy);
        }

    }


    public void LookAtEnemy()
    {
        
    }



}
