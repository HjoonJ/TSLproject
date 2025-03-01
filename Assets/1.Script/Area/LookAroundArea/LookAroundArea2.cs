using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundArea2 : LookAroundArea
{
    // ���� �� Ư�� �ִϸ��̼�(ȣ���ϴ� �ִϸ��̼�//�ӽ÷� Idle) ��ȯ �� 2�� �� lookAt �ִϸ��̼� ȣ��

    public override void Arrived(Character c)
    {
        base.Arrived(c);
        Debug.Log("LookAt 2�� �� ����");
        //Character.Instance.animator.Play("Idle");
        //Invoke("LookAt", 2);
        StartCoroutine(CoLookAt());
    }

    IEnumerator CoLookAt()
    {
        //2�� ��ٸ�
        yield return new WaitForSeconds(2);
        character.animator.Play("LookAt");

        //�������� ��ٸ�
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
