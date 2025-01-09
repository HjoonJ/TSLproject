using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellVoiceResponse : MonoBehaviour
{
    InventorySellCanvas inventorySellCanvas = FindObjectOfType<InventorySellCanvas>(true);
    public void SellVoiceCommand(string[] response)
    {
        if (response == null || response.Length == 0)
        {
            Debug.Log("No valid response detected.");
            return;
        }

        //ù ��° �ܾ�� ������ �̸� Ȯ��
        string itemName = response[0].ToLower();

        // �ٸ� �ν� �ܾ� �ְ� ������ ���.
        //string shapeName = response[1];

        UserItem userItem = User.Instance.GetUserItem(itemName);
        if (userItem == null || userItem.count <= 0)
        {
            Debug.Log("No matching item found");
        }

        else
        {
            
            //�ش� ������ �̸����� �����͸� �ҷ����� Sell ó��
            inventorySellCanvas.GetSellItemData(itemName);

            //�� �Լ��� ���������� ���� ���̰� ó��. 
            //1. InventoryCanvas ���� �ѱ�.
            //2. InventorySellCanvas �Ѱ�(�νĽ�Ų itemName�� �ش��ϴ� ������ �����Ͱ� ������) 
            //3. �ȸ���
            //4. ������

            //�Ǹ� �Լ� ȣ��
            inventorySellCanvas.OnClickedSell();

        }

        
    }
}
