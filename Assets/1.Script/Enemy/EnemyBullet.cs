using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    int damage;

    public void Shoot(int d)
    {
        //transform.rotation = Quaternion.AngleAxis(rotationZ, Vector3.back);
        damage = d;
    }

    void Update()
    {
        //Ÿ�� ������ ��ƾ���. 
        
        
        //transform.position += transform.up * moveSpeed * Time.deltaTime;
    }


    // 3D ? ������ �ȵ�. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            collision.GetComponent<Target>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
