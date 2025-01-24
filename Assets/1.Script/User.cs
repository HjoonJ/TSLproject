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
            //신규유저
            userData = new UserData();

            userData.nickName = "A";
            userData.gold = 100;
            userData.playTime = Time.time;
            SaveMgr.SaveData("UserData", userData);
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

    public UserItem AddItem(string itemName, int count)
    {
        UserItem userItem = GetUserItem(itemName);
        userItem.count += count;
        // 아이템 리스트가 업데이트 시 인벤토리 갱신

        //인벤토리 캔버스가 비활성화 되어 있어서 에러가 발생.
        InventoryCanvas.Instance.InventoryUpdate();

        SaveMgr.SaveData("UserData", userData);
        
        return userItem;

    }

    //itemName에 해당하는 UserItem 객체 반환

    public UserItem GetUserItem(string itemName) 
    {

        for (int i = 0; i < userData.userItems.Count; i++)
        { 
            if (itemName == userData.userItems[i].itemName)
            {
               
               return userData.userItems[i]; 
            }
  
        }

        //itemName에 해당하는 UserItem을 생성하고 
        //유저 아이템 리스트에 담고
        //해당 객체를 반환!
        
        userData.userItems.Add(new UserItem(itemName, 0));
        // 추가된 아이템을 마지막 리스트에 추가하는 코드
        return userData.userItems[userData.userItems.Count - 1];
   
    }

    public void AddGold(int g)
    {
        userData.gold += g;
        SaveMgr.SaveData("UserData", userData);
    }


}

// 유저 데이터 (유저가 아이템 활용함에 따라 가변할 수 있는 내용들)
[System.Serializable]
public class UserData
{
    public string nickName;
    public int gold;

    public float playTime;
    public List<UserItem> userItems = new List<UserItem>();
    
}


//UserItme 클래스 선언

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