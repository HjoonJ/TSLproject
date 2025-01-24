using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // 타격 받는 순간 Hp 바 등장 및 일정 Hp 이하 시 불타는 이펙트 

    //public int hp;

    public float maxHp;
    public float curHp; // current hp

    //public float maxShield = 100;
    //public float curShield; // current shield
    public void Start()
    {
        TargetSetUp();
    }

    public void TakeDamage(float damage)
    {
        // 나중에 실드 추가할 일 있으면 추가하기 - 건물이 다 부서지고 난 뒤 2차 침공 때 사용.
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

    public void TargetSetUp()
    {

        curHp = maxHp;
        //curShield = maxShield;
    }
}
