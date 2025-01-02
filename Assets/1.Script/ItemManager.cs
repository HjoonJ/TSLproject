using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public ItemData[] itemDatas;

    public ItemData GetItemData(string key)
    {
        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (itemDatas[i].key == key)
            {
                return itemDatas[i];
            }
        }
        return null;
    }
}


//게임 데이터 (게임 내 절대불변의 내용들)
[System.Serializable]
public class ItemData
{
    public string key;
    public Sprite thum;

    public string name;
    public string description;
    public int price;

}
