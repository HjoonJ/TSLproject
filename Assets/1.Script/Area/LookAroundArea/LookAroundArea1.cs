using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundArea1 : LookAroundArea
{
    public Transform[] points;
    public int curInx = 0;
    public int loopCount = 0;
    public int maxLoopCount = 2;
    
    public override void Arrived(Character c)
    {
        base.Arrived(c);
        
        Debug.Log("������ �� �ִ� ��� ����");

        curInx = 0;

        // ��ȸ Ƚ�� �ʱ�ȭ
        loopCount = 0; 

        c.MoveTo(points[curInx].position);
        

    }
    public void Update()
    {
        if (character == null)
            return;
        
        if (loopCount >= maxLoopCount)
        {
            return;
        }


        // 0,1,2 ��ġ�� 2�� �ݺ��ؼ� �̵�
        if (Vector2.Distance(points[curInx].position, character.transform.position) <= 0.1f)
        {
            curInx++;

            if (curInx >= points.Length)
            {
                curInx = 0;
                loopCount++;
            }

            if (loopCount < maxLoopCount)
            {
                character.MoveTo(points[curInx].position);
            }
            else
            {
                // 1�� �� Idle ���·� ����

                StartCoroutine(BackToIdle(1));

            }

        }
    }

    // �� �������� 1�� �ִٰ� ĳ���� ���¸� Idle ���·� �ǵ�����.
    public IEnumerator BackToIdle(float d)
    {
        yield return new WaitForSeconds(d);

        // Idle ���·� ����
        character.animator.Play("Idle");
        character.React(BehaviourType.Idle);
        character = null;
    }

}
