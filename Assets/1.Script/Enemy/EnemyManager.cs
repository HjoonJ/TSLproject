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
    public int totalEnemies;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;


    }

    public void StartBattle()
    {
        Enemy me = enemyPrefabs[0];
        Enemy re = enemyPrefabs[1];
        Enemy ph = enemyPrefabs[2];

        int randomNumber1 = Random.Range(5, 7);
        int randomNumber2 = Random.Range(6, 7);
        int randomNumber3 = Random.Range(4, 5);

        SpawnEnemy(me, randomNumber1);
        SpawnEnemy(re, randomNumber2);
        SpawnEnemy(ph, randomNumber3);

        totalEnemies = enemies.Count;
    }

    public void SpawnEnemy(Enemy ePrefab, int rNumber)
    {

        for (int i = 0; i < rNumber; i++)
        {
            //��ġ���� (8���� ��ġ �� �ϳ� �������� �̾Ƽ� ����)
            int p = Random.Range(0, enemyPopUpPoints.Length);

            Transform randomPopUpPoint = enemyPopUpPoints[p];

            // ���Ǿ� �� 2 ���� ��ŭ�� ���� �ȿ��� �����ϰ� �� ���� ��Ű�� ���� �ڵ�
            Vector3 randomPoint = randomPopUpPoint.position + Random.insideUnitSphere * 2;
            randomPoint.y = 0;

            Enemy e = Instantiate(ePrefab);
            e.transform.position = randomPoint;

            //����Ʈ �ȿ� ������ �� ���.
            enemies.Add(e);
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
