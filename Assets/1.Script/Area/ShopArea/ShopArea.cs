using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopArea : Area
{
    public GameObject shopCanvas;
    string itemName;
    bool endVoice;

    InventorySellCanvas inventorySellCanvas;

    public void Awake()
    {
        inventorySellCanvas = FindObjectOfType<InventorySellCanvas>(true);
    }

    public override void Arrived(Character c)
    {
        base.Arrived(c);
        StartCoroutine(shopCanvasOpen());
    }

    IEnumerator shopCanvasOpen()
    {
        yield return new WaitForSeconds(2); // 2�� �ִٰ� ����.
        shopCanvas.SetActive(true);
        endVoice = false;
        itemName = null;

        //��Ҹ��� �鸱�� ���� ����ȵǵ��� ���ξ����.
        while (true)
        {
            if (endVoice == true)
                break;

            yield return null;
        }
        if (itemName == null)
        {
            Debug.Log("�ȴ��");
        }
        else
        {
            inventorySellCanvas.GetSellItemData(itemName);
            inventorySellCanvas.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);
            
            inventorySellCanvas.OnClickedSell();

            
        }
        


        //���! - ĳ���Ͱ� "�� ���Ⱦ�?"
        //Voice �ν� Ȱ��ȭ - ����� : 00 2�� �Ⱦ�.

        //SellVoiceCommand() ����.


    }



    public void SellVoiceCommand(string[] response)
    {
        endVoice = true;
        if (response == null || response.Length == 0)
        {
            Debug.Log("No valid response detected.");
            return;
        }

        //ù ��° �ܾ�� ������ �̸� Ȯ��
        string iName = response[0].ToLower();

        // �ٸ� �ν� �ܾ� �ְ� ������ ���.
        //string shapeName = response[1];

        UserItem userItem = User.Instance.GetUserItem(iName);
        if (userItem == null || userItem.count <= 0)
        {
            Debug.Log("No matching item found");
        }

        else
        {

            //�ش� ������ �̸����� �����͸� �ҷ����� Sell ó��
            inventorySellCanvas.GetSellItemData(itemName);

           

            //�Ǹ� �Լ� ȣ��
            inventorySellCanvas.OnClickedSell();

        }


    }
}
