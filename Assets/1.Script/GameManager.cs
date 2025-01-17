using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public Target[] targets;
    public Character[] characters;

    public Enemy[] enemies;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        targets = FindObjectsOfType<Target>();
        //유니티6에서는 targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

        characters = FindObjectsOfType<Character>();

        enemies = FindObjectsOfType<Enemy>();
    }

    public void Start()
    {
        
    }

    // 가장 가까운 적을 설정하는 방법 (여러 곳에서 많이 활용됨!!)
    public Target GetClosestTarget(Vector3 point)
    {
        float minDis = float.MaxValue;
        int targetIdx = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            float dis = Vector3.Distance(targets[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                targetIdx = i;
            }
        }
        Debug.Log(minDis);
        return targets[targetIdx];
    }

    public Character GetClosestCharacter(Vector3 point)
    {
        float minDis = float.MaxValue;
        int characterIdx = 0;
        for (int i = 0; i < characters.Length; i++)
        {
            float dis = Vector3.Distance(characters[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                characterIdx = i;
            }
        }
        Debug.Log(minDis);
        return characters[characterIdx];
    }

    public Enemy GetClosestEnemy(Vector3 point)
    {
        float minDis = float.MaxValue;
        int enemyIdx = 0;
        for (int i = 0; i < enemies.Length; i++)
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
