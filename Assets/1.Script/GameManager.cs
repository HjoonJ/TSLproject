using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameMode gameMode;
    public Target[] targets;
    public Character[] characters;


    public float duration;

    public Transform[] enemyPopUpPoints;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        targets = FindObjectsOfType<Target>();
        //유니티6에서는 targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

        characters = FindObjectsOfType<Character>();

    }

    public void Start()
    {
        gameMode = GameMode.Normal;

        StartCoroutine(CoGameMode());
    }

    IEnumerator CoGameMode()
    {
        yield return new WaitForSeconds(duration);
        gameMode = GameMode.Battle;

        //타임라인 플레이시키기.

        //몬스터 생성시키기 (어디, 어떻게)
        EnemyManager.Instance.EnemySpawn();

        //몬스터가 생성될 때까지 코드 대기


        //캐릭터들을 Battle 모드로 바꾸기.
        Character.Instance.React(BehaviourType.Battle);
        
        //적의 숫자가 일정 수 이하 만큼 대기
        
        //현재 처치된 적 수가 절반인지 판단!!

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

    


    public enum GameMode
    {
        Normal,
        Battle
    }
}
