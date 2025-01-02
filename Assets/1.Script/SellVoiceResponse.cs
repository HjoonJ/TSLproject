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

        // �ٸ� �ν� �ܾ� �ְ� ������ ���
        //string shapeName = response[1];

        //������ �̸� Ȯ��
        if (itemName == "apple" || itemName == "mushroom" || itemName == "banana")
        {
            //�ش� ������ �̸����� �����͸� �ҷ����� Sell ó��
            inventorySellCanvas.GetSellItemData(itemName);

            //�� �Լ��� ���������� ���� ���̰� ó��. 


            //�Ǹ� �Լ� ȣ��
            inventorySellCanvas.OnClickedSell();

            
        }
        else
        {
            Debug.Log("No matching item found");
        }
    }
}
