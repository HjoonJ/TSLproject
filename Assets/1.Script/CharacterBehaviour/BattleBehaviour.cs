using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleBehaviour : CharacterBehaviour
{

    public Enemy enemy;
    public bool checkCoAttack; // ���ݻ������� �Ǻ�

    public BattleBehaviour()
    {
        type = BehaviourType.Battle;

    }


    public override void EnterBehaviour()
    {
        Debug.Log($"BattleBehaviour EnterBehaviour() {character.gameObject.name}");
        character.animator.Play("Idle");

        character.StopMoving();
        // �ִϸ��̼� ����

        FindEnemy();

    }

    public void FindEnemy()
    {
        StopAllCoroutines();

        // character ������Ʈ�� �����ϰ� �ִ� ���ӿ�����Ʈ �� ���� ����� ������ �̵�.
        enemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

        Debug.Log($"BattleBehaviour FindEnemy() {character.gameObject.name} enemy name {enemy.name} ");
        //Debug.Log("����� �� ã��");

        StartCoroutine(CoCheckFrontEnemy());
    }
    
    IEnumerator CoCheckFrontEnemy()
    {
       
            while (true && checkCoAttack== false)
            {
                yield return new WaitForSeconds(0.5f);

                Collider[] cols = Physics.OverlapSphere(character.attackPoint.position, character.attackRange, character.enemyLayerMask);

                if (cols.Length <= 0)
                {
                    continue;
                }


                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].gameObject.tag == "Enemy")
                    {

                        enemy = cols[i].gameObject.GetComponent<Enemy>();
                        checkCoAttack = true;
                        StartCoroutine(CoAttack());


                        break;
                    }
                }

            }
        
    }
    public override void UpdateBehaviour()
    {
        if (enemy != null && checkCoAttack == false)
        {

            LookAtEnemy();

            character.MoveTo(enemy.transform.position, Arrived);

        }
        
    }

    public override void CompleteBehaviour()
    {
       



    }



    public void Arrived()
    {
        //Debug.Log("������ ����");
        //StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        // ������ �ٶ󺸱� + �־����ٰ� �Ǻ����� �� �ٽ� ���� ����� �� ã�Ƽ� �ٰ����� �����ϱ�.


        while (true)
        {

            // Ÿ���� �Ҹ�� ���
            if (enemy == null)
            {
                Debug.Log("Ÿ���� �Ҹ�, �� Ÿ�� ã��");
                FindEnemy();
                yield break;  // �ڷ�ƾ ���� (���ο� Ÿ�� Ž�� �� ���� CoAttack�� ����.)
            }

            // 1�ʸ��� ���ο� ���� ����� ���� Ȯ��
            yield return new WaitForSeconds(1f);

            Enemy closestEnemy = EnemyManager.Instance.GetClosestEnemy(transform.position);
            
            // ���� ����� ������ ��ü
            if (closestEnemy != null && closestEnemy != enemy)
            {
                float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
                float newDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
                
                if (newDistance < currentDistance)
                {
                    Debug.Log("�� ����� �� �߰�, Ÿ�� ��ü");
                    enemy = closestEnemy;

                }
            }


            //  ���� ������ �Ÿ��� �� ���� �ֱ⸶�� Ȯ��
            float distanceToEnemy = Vector3.Distance(character.attackPoint.position, enemy.transform.position);
            
            if (distanceToEnemy > character.attackRange)
            {
                Debug.Log("���� ���� ���� ������ �̵�, ���ο� �� Ž��");
                
                checkCoAttack = false; // ���� ���� ����
                FindEnemy();
                
                yield break;
            }

            yield return new WaitForSeconds(character.attackSpeed);

            Debug.Log("�� ����!!");
            // ĳ���Ͱ� ���� Attack() �Լ� ȣ���ϰ� enemy ����
            character.Attack(enemy);
        }

    }


    public void LookAtEnemy()
    {
        if (enemy != null)
        {
            // Ÿ�� ���� ���
            Vector3 direction = (enemy.transform.position - transform.position).normalized;

            // Y�� ȸ���� ����Ͽ� ȸ���� ����
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // ��� Ÿ�� �������� ȸ��
            transform.rotation = lookRotation;
        }
    }



}
