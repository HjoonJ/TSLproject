using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleBehaviour : CharacterBehaviour
{

    public Enemy enemy;
    public bool checkCoAttack; // 공격상태인지 판별

    public BattleBehaviour()
    {
        type = BehaviourType.Battle;

    }


    public override void EnterBehaviour()
    {
        Debug.Log($"BattleBehaviour EnterBehaviour() {character.gameObject.name}");
        character.animator.Play("Idle");

        character.StopMoving();
        // 애니메이션 끄기

        FindEnemy();

    }

    public void FindEnemy()
    {
        StopAllCoroutines();

        // character 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        enemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

        Debug.Log($"BattleBehaviour FindEnemy() {character.gameObject.name} enemy name {enemy.name} ");
        //Debug.Log("가까운 적 찾음");

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
        //Debug.Log("적에게 도착");
        //StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        // 때릴때 바라보기 + 멀어졌다고 판별됐을 땐 다시 가장 가까운 얘 찾아서 다가가서 공격하기.


        while (true)
        {

            // 타겟이 소멸된 경우
            if (enemy == null)
            {
                Debug.Log("타겟이 소멸, 새 타겟 찾기");
                FindEnemy();
                yield break;  // 코루틴 종료 (새로운 타겟 탐색 후 새로 CoAttack이 시작.)
            }

            // 1초마다 새로운 가장 가까운 적을 확인
            yield return new WaitForSeconds(1f);

            Enemy closestEnemy = EnemyManager.Instance.GetClosestEnemy(transform.position);
            
            // 가장 가까운 적으로 교체
            if (closestEnemy != null && closestEnemy != enemy)
            {
                float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
                float newDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
                
                if (newDistance < currentDistance)
                {
                    Debug.Log("더 가까운 적 발견, 타겟 교체");
                    enemy = closestEnemy;

                }
            }


            //  현재 적과의 거리를 매 공격 주기마다 확인
            float distanceToEnemy = Vector3.Distance(character.attackPoint.position, enemy.transform.position);
            
            if (distanceToEnemy > character.attackRange)
            {
                Debug.Log("적이 공격 범위 밖으로 이동, 새로운 적 탐색");
                
                checkCoAttack = false; // 공격 상태 해제
                FindEnemy();
                
                yield break;
            }

            yield return new WaitForSeconds(character.attackSpeed);

            Debug.Log("적 공격!!");
            // 캐릭터가 가진 Attack() 함수 호출하고 enemy 전달
            character.Attack(enemy);
        }

    }


    public void LookAtEnemy()
    {
        if (enemy != null)
        {
            // 타겟 방향 계산
            Vector3 direction = (enemy.transform.position - transform.position).normalized;

            // Y축 회전만 고려하여 회전값 설정
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 즉시 타겟 방향으로 회전
            transform.rotation = lookRotation;
        }
    }



}
