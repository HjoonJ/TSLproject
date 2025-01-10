using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float moveSpeed;
    public float atkSpeed;
    [SerializeField] EnemyBullet enemyBulletPrefeb;

    //가장 가까운 타겟 쪽으로 이동시켜야함.
    Vector3 targetPos;

    bool arrived = false;

    public override void TakeDamage(int d)
    {
        curHp -= d;
        if (curHp <= 0)
        { 
            Destroy(gameObject);   
        }
    }

    private void Update()
    {

        if (arrived)
            return;

        targetPos = GameManager.Instance.GetClosestTarget(targetPos).transform.position;

        if (Vector2.Distance(transform.position, targetPos) <= 0.01)
        {
            arrived = true;
            StartCoroutine(CoAttack());

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);
            EnemyBullet b = Instantiate(enemyBulletPrefeb);
            b.transform.position = transform.position;

            //b.Shoot(damage);

            Debug.Log("Attack!!");

        }

            
        
    }

}
