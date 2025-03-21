using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundArea2 : LookAroundArea
{
    // 도착 후 특정 애니메이션(호흡하는 애니메이션//임시로 Idle) 전환 후 2초 뒤 lookAt 애니메이션 호출

    public override void Arrived(Character c)
    {
        base.Arrived(c);
        Debug.Log("LookAt 2초 후 실행");
        //Character.Instance.animator.Play("Idle");
        //Invoke("LookAt", 2);
        StartCoroutine(CoLookAt());
    }

    IEnumerator CoLookAt()
    {
        //2초 기다림
        yield return new WaitForSeconds(2);
        character.animator.Play("LookAt");

        //한프레임 기다림
        yield return null;
        float lookAtAnimTime = character.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(lookAtAnimTime);
        character.animator.Play("Idle");
        
        yield return new WaitForSeconds(1);

        character.React(BehaviourType.Idle);
        character = null;
    }

    public void Update()
    {

    }


}
