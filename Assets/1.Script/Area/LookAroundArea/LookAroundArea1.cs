using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundArea1 : LookAroundArea
{
    public Transform[] points;
    public int curInx = 0;
    public int loopCount = 0;
    public int maxLoopCount = 2;
    public override void Arrived()
    {
        Debug.Log("구경할 수 있는 장소 도착");

        curInx = 0;

        // 순회 횟수 초기화
        loopCount = 0; 

        Character.Instance.MoveTo(points[curInx].position);
        

    }
    public void Update()
    {
        if (loopCount >= maxLoopCount)
        {
            return;
        }


        // 0,1,2 위치를 2번 반복해서 이동
        if (Vector2.Distance(points[curInx].position, Character.Instance.transform.position) <= 0.1f)
        {
            curInx++;

            if (curInx >= points.Length)
            {
                curInx = 0;
                loopCount++;
            }

            if (loopCount < maxLoopCount)
            {
                Character.Instance.MoveTo(points[curInx].position);
            }
            else
            {
                // 1초 후 Idle 상태로 변경
                StartCoroutine(BackToIdle(1)); 
            }

        }
    }

    // 다 돌았으면 1초 있다가 캐릭터 상태를 Idle 상태로 되돌리기.
    public IEnumerator BackToIdle(float d)
    {
        yield return new WaitForSeconds(d);

        // Idle 상태로 변경
        Character.Instance.animator.Play("Idle");
        Character.Instance.React(BehaviourType.Idle);
    }

}
