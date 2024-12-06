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
            //신규유저
            userData = new UserData();

            userData.nickName = "A";
            userData.gold = 100;
            userData.playTime = Time.time;
            SaveMgr.SaveData("UserData", userData);
        }


        

        //userData.array 배열에 1~10 정수를 담기.
        for (int i = 0; i < 10; i++)
        {
            userData.array[i] = i + 1;
        }

    }

    public string itemName;
    public int count;

    // A키를 누를때 마다 UserItem 객체를 생성하여 
    // itemName, count 멤버변수에 설정된 값을 UserItem 객체의 적절한 변수에 담기.

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A키 누름");
            UserItem userItem = new UserItem();
            userItem.itemName = itemName;
            userItem.count = count;
            
            //UserData 객체의 userItems 리스트에 UserItem 객체 담기.
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


//UserItme 클래스 선언

[System.Serializable]
public class UserItem
{
    public string itemName;
    public int count;
}