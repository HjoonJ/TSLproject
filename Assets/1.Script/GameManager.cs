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
        //����Ƽ6������ targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

        characters = FindObjectsOfType<Character>();

    }

    public void Start()
    {
        gameMode = GameMode.Normal;

        
        StartCoroutine(CoGameMode());

        //CharacterManager�κ��� Character 1�� �ش��ϴ� ������Ʈ �ޱ�

        Character character1 = CharacterManager.Instance.GetCharacter(CharacterType.Character1);
    }

    IEnumerator CoGameMode()
    {
        while (true)
        {
            // �Ϲݸ�� ��Ȳ
            for (int i = 0; i < CharacterManager.Instance.usingCharacters.Count; i++)
            {
                CharacterManager.Instance.usingCharacters[i].ChangeGameMode(gameMode);
            }


            yield return new WaitForSeconds(duration);

            // ��Ʋ��� ��Ȳ
            gameMode = GameMode.Battle;

            //Ÿ�Ӷ��� �÷��̽�Ű��.

            //���� ������Ű�� (���, ���)
            EnemyManager.Instance.StartBattle();

            //���Ͱ� ������ ������ �ڵ� ���


            //ĳ���͵��� Battle ���� �ٲٱ�

            for (int i = 0; i < CharacterManager.Instance.usingCharacters.Count; i++)
            {
                CharacterManager.Instance.usingCharacters[i].ChangeGameMode(gameMode);
            }

            //���� ���ڰ� ���� �� ���� ��ŭ ���

            //���� óġ�� �� ���� �������� �Ǵ�!!<<EnemyManager�� ����Ʈ�� Ȯ��>>
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
                // �ڷ�ƾ �ȿ��� While ���� ������ �ؿ� ������ �� �����!!!!
                yield return null;
            }
        }
        
        
        


    }

    // ���� ����� ���� �����ϴ� ��� (���� ������ ���� Ȱ���!!)
    public Target GetClosestTarget(Vector3 point)
    {
        float minDis = float.MaxValue;
        Target closestTarget = null;

        for (int i = 0; i < targets.Length; i++)
        {
            // �̹� �ı��� ��ü�� �ǳʶ�
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
                // �ı��Ǵ� �ش� Ÿ���� null�� ����.
                targets[i] = null; 
                Debug.Log("Ÿ�� ���ŵ�");
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
