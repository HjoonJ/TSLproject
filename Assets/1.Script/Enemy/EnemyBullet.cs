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
        // 타겟이 유효한지 체크
        if (target == null)
        {
            // 타겟이 사라졌다면 총알을 삭제
            Destroy(gameObject);
            return;
        }

        // 타겟 방향
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // 타겟 방향으로 이동
        transform.position += direction * moveSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Target"))
        {
            Debug.Log("총알 부딪힘");
            collision.GetComponent<Target>().TakeDamage(bulletPower);
            Destroy(gameObject);
        }
    }
}
