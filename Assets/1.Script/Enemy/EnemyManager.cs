using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] Enemy[] enemyPrefabs; 

    // 8���� ��ǥ���� ����.
    public Transform[] enemyPopUpPoints;

    public List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;


    }

    public void EnemySpawn()
    {

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //��ġ���� (8���� ��ġ �� �ϳ� �������� �̾Ƽ� ����)
                int p = Random.Range(0, enemyPopUpPoints.Length);
                
                Transform randomPopUpPoint = enemyPopUpPoints[p];

                // ���Ǿ� �� 2 ���� ��ŭ�� ���� �ȿ��� �����ϰ� �� ���� ��Ű�� ���� �ڵ�
                Vector3 randomPoint = randomPopUpPoint.position + Random.insideUnitSphere * 2;
                randomPoint.y = 0;
                
                Enemy e = Instantiate(enemyPrefabs[i]);

                e.transform.position = randomPoint;
                
                
                //����Ʈ �ȿ� ������ �� ���.
                enemies.Add(e);
            }
            
        }
        
    }

    public Enemy GetClosestEnemy(Vector3 point)
    {
        float minDis = float.MaxValue;
        int enemyIdx = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            float dis = Vector3.Distance(enemies[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                enemyIdx = i;
            }
        }
        Debug.Log(minDis);
        return enemies[enemyIdx];
    }


}
