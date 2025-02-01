using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    //���ݷ�
    public float bulletPower;

    Target target;
    

    //���ݷ°� Ÿ���� �����ϴ� �Լ�
    public void Shoot(float power, Target t)
    {
        bulletPower = power;
        target = t;
    }

    public void Update()
    {
        // Ÿ���� ��ȿ���� üũ
        if (target == null)
        {
            // Ÿ���� ������ٸ� �Ѿ��� ����
            Destroy(gameObject);
            return;
        }

        // Ÿ�� ����
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Ÿ�� �������� �̵�
        transform.position += direction * moveSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Target"))
        {
            Debug.Log("�Ѿ� �ε���");
            collision.GetComponent<Target>().TakeDamage(bulletPower);
            Destroy(gameObject);
        }
    }
}
