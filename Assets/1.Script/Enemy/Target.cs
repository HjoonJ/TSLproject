using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Ÿ�� �޴� ���� Hp �� ���� �� ���� Hp ���� �� ��Ÿ�� ����Ʈ 

    public int hp;

    public float maxHp;
    public float curHp; // current hp

    //public float maxShield = 100;
    //public float curShield; // current shield


    public void TakeDamage(int damage)
    {
        // ���߿� �ǵ� �߰��� �� ������ �߰��ϱ� - �ǹ��� �� �μ����� �� �� 2�� ħ�� �� ���.
        //curShield -= damage;

        //if (curShield <= 0)
        //{
        //    shieldImage.SetActive(false);

        //    curHp -= -curShield;
        //    curShield = 0;
        //    if (curHp <= 0)
        //    {
        //        StopAllCoroutines();

        //    }
        //}

        curHp -= damage;
        if (curHp <= 0)
        {

            Destroy(gameObject);
        }
    }

    public void StartGame()
    {

        curHp = maxHp;
        //curShield = maxShield;
    }
}
