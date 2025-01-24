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
    public override void EnterBehaviour() //Idle ���·� ���� �� �ѹ� ȣ���
    {
        //Idle ������ ��
        //2-4�� �������� ���� �ൿ�� ����
        timer = Random.Range(2f, 4f);

        Character.Instance.animator.Play("Idle");

    }

    public override void UpdateBehaviour() //Idle ���·� ���� �� �ݺ� ȣ���
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

        // ĳ������ React �Լ��� ȣ���Ͽ� ���� �ൿ�� ����


        //�׽�Ʈ������ LookAround, Collect ������ ����
        //Character.Instance.React(BehaviourType.LookAround);
        
        Character.Instance.React(BehaviourType.Collect);

        //Character.Instance.React(nextBehaviourType);
    }


}
