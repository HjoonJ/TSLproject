using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : CharacterBehaviour
{
    public float timer = 0f;
    public BehaviourType[] randomBehaviourTypes;

    public IdleBehaviour()
    {
        type = BehaviourType.Idle;

    }
    public override void EnterBehaviour() //Idle 상태로 진입 시 한번 호출됨
    {
        //Idle 상태일 때
        //2-4초 간격으로 다음 행동을 결정
        timer = Random.Range(2f, 4f);

        Character.Instance.animator.Play("Idle");

    }

    public override void UpdateBehaviour() //Idle 상태로 진입 후 반복 호출됨
    {
        
        if (timer <= 0)
        {
            NextBehaviour();
            return;
        }
        timer -= Time.deltaTime;
    }

    

    public void NextBehaviour()
    {
        int count = randomBehaviourTypes.Length;
        int randomIdx = Random.Range(0, count);
        BehaviourType nextBehaviourType = randomBehaviourTypes[randomIdx];

        // 캐릭터의 React 함수를 호출하여 다음 행동을 실행


        //테스트용으로 LookAround, Collect 등으로 설정
        //Character.Instance.React(BehaviourType.LookAround);
        
        //Character.Instance.React(BehaviourType.Collect);

        Character.Instance.React(nextBehaviourType);
    }


}
