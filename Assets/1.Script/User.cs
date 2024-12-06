using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public UserData userData;

    public void Start()
    {
        userData = SaveMgr.LoadData<UserData>("UserData");
        if(userData == null)
        {
            //�ű�����
            userData = new UserData();

            userData.nickName = "A";
            userData.gold = 100;
            userData.playTime = Time.time;
            SaveMgr.SaveData("UserData", userData);
        }


        

        //userData.array �迭�� 1~10 ������ ���.
        for (int i = 0; i < 10; i++)
        {
            userData.array[i] = i + 1;
        }

    }

    public string itemName;
    public int count;

    // AŰ�� ������ ���� UserItem ��ü�� �����Ͽ� 
    // itemName, count ��������� ������ ���� UserItem ��ü�� ������ ������ ���.

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("AŰ ����");
            UserItem userItem = new UserItem();
            userItem.itemName = itemName;
            userItem.count = count;
            
            //UserData ��ü�� userItems ����Ʈ�� UserItem ��ü ���.
            userData.userItems.Add(userItem);
            
            SaveMgr.SaveData("UserData", userData);
        }
    }
}


[System.Serializable]
public class UserData
{
    public string nickName;
    public int gold;

    public float playTime;
    public List<UserItem> userItems = new List<UserItem>();
    public int[] array = new int[10];
}


//UserItme Ŭ���� ����

[System.Serializable]
public class UserItem
{
    public string itemName;
    public int count;
}