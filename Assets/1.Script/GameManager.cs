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
        //����Ƽ6������ targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

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

        //Ÿ�Ӷ��� �÷��̽�Ű��.

        //���� ������Ű�� (���, ���)
        EnemyManager.Instance.EnemySpawn();

        //���Ͱ� ������ ������ �ڵ� ���


        //ĳ���͵��� Battle ���� �ٲٱ�.
        Character.Instance.React(BehaviourType.Battle);
        
        //���� ���ڰ� ���� �� ���� ��ŭ ���
        
        //���� óġ�� �� ���� �������� �Ǵ�!!

    }

    // ���� ����� ���� �����ϴ� ��� (���� ������ ���� Ȱ���!!)
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
