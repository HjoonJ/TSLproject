using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySellCanvas : MonoBehaviour
{
    //InventoryPanel Ŭ�� �� �ش� �гο��� �����ְ� �ִ� �������� SellCanvas���� �������� ó���ϱ�

    public Image sellItemImage;
    public TMP_Text sellItemCount;
    public TMP_InputField sellCountInputField;
    ItemData itemData;
    UserItem userItem;


    public void GetSellItemData(string itemName)
    {
        //���ӵ����� �ҷ�����
        itemData = ItemManager.Instance.GetItemData(itemName);
        //���������� �ҷ�����
        userItem = User.Instance.GetUserItem(itemName);

        sellItemImage.sprite = itemData.thum;

        sellItemCount.text = userItem.count.ToString();

        //�ԷµǾ� �ִ� ������ Max�� �켱 ����.
        sellCountInputField.text = userItem.count.ToString();
    }
    public void OnClickedSell()
    {
        int count = int.Parse(sellCountInputField.text);
        // max itemCount ���� ���� ���ڴ� �Է��� �ȵǵ��� InputField ó��
        if (count > userItem.count)
        {
            count = userItem.count;
        }

        int getGold = itemData.price * count;
        //count * price ���� ��ȯ 
        
        
        User.Instance.AddGold(getGold);


        //���� ������ �� �������� �������� ���� 
        User.Instance.AddItem(itemData.key, - count );

        //�ٽ� �����۵����� �ҷ��ͼ� �缳�� & UI ����
        GetSellItemData(itemData.key);

    }
    public void OnClickedSellClose()
    {
        
        InventoryCanvas.Instance.InventoryUpdate();
        gameObject.SetActive(false);
    }
}
