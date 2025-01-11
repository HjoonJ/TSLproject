using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    //공격력
    public float bulletPower;

    Target target;
    

    //공격력과 타켓을 전달하는 함수
    public void Shoot(float power, Target t)
    {
        bulletPower = power;
        target = t;
    }

    public void Update()
    {
        //타겟 쪽으로 쏘아야함. 
        if (target != null)
        {
            // 타겟 방향
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // 타겟 방향으로 이동
            transform.position += direction * moveSpeed * Time.deltaTime;
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Target"))
        {
            collision.GetComponent<Target>().TakeDamage(bulletPower);
            Destroy(gameObject);
        }
    }
}
