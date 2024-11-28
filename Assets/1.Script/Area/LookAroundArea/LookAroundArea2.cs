using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundArea2 : LookAroundArea
{
    // ���� �� Ư�� �ִϸ��̼�(ȣ���ϴ� �ִϸ��̼�//�ӽ÷� Idle) ��ȯ �� 2�� �� lookAt �ִϸ��̼� ȣ��

    public override void Arrived()
    {
        Debug.Log("LookAt 2�� �� ����");
        //Character.Instance.animator.Play("Idle");
        //Invoke("LookAt", 2);
        StartCoroutine(CoLookAt());
    }

    IEnumerator CoLookAt()
    {
        //2�� ��ٸ�
        yield return new WaitForSeconds(2);
        Character.Instance.animator.Play("LookAt");

        //�������� ��ٸ�
        yield return null;
        float lookAtAnimTime = Character.Instance.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(lookAtAnimTime);
        Character.Instance.animator.Play("Idle");
        
        yield return new WaitForSeconds(1);
        Character.Instance.React(BehaviourType.Idle);
    }

    public void Update()
    {

    }


}
