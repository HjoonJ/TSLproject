using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class User : MonoBehaviour
{
    public UserData userData;

    public static User Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

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

    public UserItem AddItem(string itemName, int count)
    {
        UserItem userItem = GetUserItem(itemName);
        userItem.count += count;
        // ������ ����Ʈ�� ������Ʈ �� �κ��丮 ����

        //�κ��丮 ĵ������ ��Ȱ��ȭ �Ǿ� �־ ������ �߻�.
        InventoryCanvas.Instance.InventoryUpdate();

        SaveMgr.SaveData("UserData", userData);
        
        return userItem;

    }

    //itemName�� �ش��ϴ� UserItem ��ü ��ȯ

    public UserItem GetUserItem(string itemName) 
    {

        for (int i = 0; i < userData.userItems.Count; i++)
        { 
            if (itemName == userData.userItems[i].itemName)
            {
               
               return userData.userItems[i]; 
            }
  
        }

        //itemName�� �ش��ϴ� UserItem�� �����ϰ� 
        //���� ������ ����Ʈ�� ���
        //�ش� ��ü�� ��ȯ!
        
        userData.userItems.Add(new UserItem(itemName, 0));
        // �߰��� �������� ������ ����Ʈ�� �߰��ϴ� �ڵ�
        return userData.userItems[userData.userItems.Count - 1];
   
    }

    public void AddGold(int g)
    {
        userData.gold += g;
        SaveMgr.SaveData("UserData", userData);
    }


}

// ���� ������ (������ ������ Ȱ���Կ� ���� ������ �� �ִ� �����)
[System.Serializable]
public class UserData
{
    public string nickName;
    public int gold;

    public float playTime;
    public List<UserItem> userItems = new List<UserItem>();
    
}


//UserItme Ŭ���� ����

[System.Serializable]
public class UserItem
{
    public string itemName;
    public int count;

    public UserItem()
    {

    }
    public UserItem(string itemName, int count)
    {
        this.itemName = itemName;
        this.count = count;

    }


}