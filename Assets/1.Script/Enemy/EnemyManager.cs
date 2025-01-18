using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] Enemy[] enemyPrefabs; 

    // 8개의 좌표에서 등장.
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
                //위치설정 (8개의 위치 중 하나 랜덤으로 뽑아서 등장)
                int p = Random.Range(0, enemyPopUpPoints.Length);
                
                Transform randomPopUpPoint = enemyPopUpPoints[p];

                // 스피어 안 2 범위 만큼의 범위 안에서 랜덤하게 적 등장 시키기 위한 코드
                Vector3 randomPoint = randomPopUpPoint.position + Random.insideUnitSphere * 2;
                randomPoint.y = 0;
                
                Enemy e = Instantiate(enemyPrefabs[i]);

                e.transform.position = randomPoint;
                
                
                //리스트 안에 생성된 적 담기.
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
