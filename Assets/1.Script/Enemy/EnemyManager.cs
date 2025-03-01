using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] Enemy[] enemyPrefabs; 

    // 8개의 좌표에서 등장.
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
            //위치설정 (8개의 위치 중 하나 랜덤으로 뽑아서 등장)
            int p = Random.Range(0, enemyPopUpPoints.Length);

            Transform randomPopUpPoint = enemyPopUpPoints[p];

            // 스피어 안 2 범위 만큼의 범위 안에서 랜덤하게 적 등장 시키기 위한 코드
            Vector3 randomPoint = randomPopUpPoint.position + Random.insideUnitSphere * 2;
            randomPoint.y = 0;

            Enemy e = Instantiate(ePrefab);
            e.transform.position = randomPoint;

            //리스트 안에 생성된 적 담기.
            enemies.Add(e);
        }

    }

    public Enemy GetClosestEnemy(Vector3 point)
    {
        float minDis = float.MaxValue;
        int enemyIdx = -1;
        for (int i = 0; i < enemies.Count; i++)
        {
            float dis = Vector3.Distance(enemies[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                enemyIdx = i;
            }
        }

        if (enemyIdx == -1) 
            return null;

        Debug.Log(minDis);
        return enemies[enemyIdx];


    }


}
