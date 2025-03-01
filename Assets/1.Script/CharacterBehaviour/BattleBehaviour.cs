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
        // 애니메이션 끄기

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

        // character 컨포넌트를 포함하고 있는 게임오브젝트 중 가장 가까운 것으로 이동.
        enemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

     

        character.MoveTo(enemy.transform.position, Arrived);

    }

    public void Arrived()
    {
        Debug.Log("적에게 도착");
        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(character.attackSpeed);

            // 타겟이 소멸된 경우
            if (enemy == null)
            {
                Debug.Log("타겟이 소멸, 새 타겟 찾기");
                FindEnemy();
                continue;  // 코루틴 종료 (새로운 타겟 탐색 후 새로 CoAttack이 시작.)
            }


            Debug.Log("적 공격!!");
            // 캐릭터가 가진 Attack() 함수 호출하고 enemy 전달
            character.Attack(enemy);
        }

    }


    public void LookAtEnemy()
    {
        
    }



}
