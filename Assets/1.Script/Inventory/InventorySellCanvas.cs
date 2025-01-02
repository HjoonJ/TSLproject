using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySellCanvas : MonoBehaviour
{
    //InventoryPanel 클릭 시 해당 패널에서 보여주고 있는 아이템을 SellCanvas에서 보여지게 처리하기

    public Image sellItemImage;
    public TMP_Text sellItemCount;
    public TMP_InputField sellCountInputField;
    ItemData itemData;
    UserItem userItem;


    public void GetSellItemData(string itemName)
    {
        //게임데이터 불러오기
        itemData = ItemManager.Instance.GetItemData(itemName);
        //유저데이터 불러오기
        userItem = User.Instance.GetUserItem(itemName);

        sellItemImage.sprite = itemData.thum;

        sellItemCount.text = userItem.count.ToString();

        //입력되어 있는 수량의 Max로 우선 설정.
        sellCountInputField.text = userItem.count.ToString();
    }
    public void OnClickedSell()
    {
        int count = int.Parse(sellCountInputField.text);
        // max itemCount 보다 높은 숫자는 입력이 안되도록 InputField 처리
        if (count > userItem.count)
        {
            count = userItem.count;
        }

        int getGold = itemData.price * count;
        //count * price 골드로 반환 
        
        
        User.Instance.AddGold(getGold);


        //유저 데이터 내 아이템의 보유개수 차감 
        User.Instance.AddItem(itemData.key, - count );

        //다시 아이템데이터 불러와서 재설정 & UI 갱신
        GetSellItemData(itemData.key);

    }
    public void OnClickedSellClose()
    {
        
        InventoryCanvas.Instance.InventoryUpdate();
        gameObject.SetActive(false);
    }
}
