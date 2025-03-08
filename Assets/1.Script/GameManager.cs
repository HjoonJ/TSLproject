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

    //public Transform[] enemyPopUpPoints;
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

        //CharacterManager로부터 Character 1에 해당하는 컨포넌트 받기

        Character character1 = CharacterManager.Instance.GetCharacter(CharacterType.Character1);
    }

    IEnumerator CoGameMode()
    {
        while (true)
        {
            // 일반모드 상황
            for (int i = 0; i < CharacterManager.Instance.usingCharacters.Count; i++)
            {
                CharacterManager.Instance.usingCharacters[i].ChangeGameMode(gameMode);
            }


            yield return new WaitForSeconds(duration);

            // 배틀모드 상황
            gameMode = GameMode.Battle;

            //타임라인 플레이시키기.

            //몬스터 생성시키기 (어디, 어떻게)
            EnemyManager.Instance.StartBattle();

            //몬스터가 생성될 때까지 코드 대기


            //캐릭터들을 Battle 모드로 바꾸기

            for (int i = 0; i < CharacterManager.Instance.usingCharacters.Count; i++)
            {
                CharacterManager.Instance.usingCharacters[i].ChangeGameMode(gameMode);
            }

            //적의 숫자가 일정 수 이하 만큼 대기

            //현재 처치된 적 수가 절반인지 판단!!<<EnemyManager의 리스트를 확인>>
            while (true)
            {
                
                if (EnemyManager.Instance.enemies.Count <= EnemyManager.Instance.totalEnemies / 2)
                {

                    for (int i = 0; i < EnemyManager.Instance.enemies.Count; i++)
                    {
                        if (EnemyManager.Instance.enemies[i] != null)
                        {
                            Destroy(EnemyManager.Instance.enemies[i].gameObject);
                        }
                    }
                    EnemyManager.Instance.enemies.Clear();

                    gameMode = GameMode.Normal;
                    break;
                }
                // 코루틴 안에서 While 문을 쓸때는 밑에 문장을 꼭 써야함!!!!
                yield return null;
            }
        }
        
        
        


    }

    // 가장 가까운 적을 설정하는 방법 (여러 곳에서 많이 활용됨!!)
    public Target GetClosestTarget(Vector3 point)
    {
        float minDis = float.MaxValue;
        Target closestTarget = null;

        for (int i = 0; i < targets.Length; i++)
        {
            // 이미 파괴된 객체는 건너뜀
            if (targets[i] == null) 
                continue;

            float dis = Vector3.Distance(targets[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                closestTarget = targets[i];
            }
        }

        if (closestTarget == null)
        {
            //Debug.Log("no Target");
        }
        
        return closestTarget;
    }

    public Character GetClosestCharacter(Vector3 point)
    {
        float minDis = float.MaxValue;
        Character closestCharacter = null;
        
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
                continue;
            
            float dis = Vector3.Distance(characters[i].transform.position, point);
            if (dis < minDis)
            {
                minDis = dis;
                closestCharacter = characters[i];
            }
        }
        if (closestCharacter == null)
        {
            Debug.Log("no character");
        }
        
       
        return closestCharacter;
    }

    public void RemoveTarget(Target target)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == target)
            {
                // 파괴되는 해당 타겟을 null로 설정.
                targets[i] = null; 
                Debug.Log("타겟 제거됨");
                break;
            }
        }
    }

}


public enum GameMode
{
    Normal,
    Battle
}
