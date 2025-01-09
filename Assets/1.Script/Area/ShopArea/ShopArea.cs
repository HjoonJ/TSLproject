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

    public override void Arrived()
    {
        StartCoroutine(shopCanvasOpen());
    }

    IEnumerator shopCanvasOpen()
    {
        yield return new WaitForSeconds(2); // 2초 있다가 실행.
        shopCanvas.SetActive(true);
        endVoice = false;
        itemName = null;

        //목소리가 들릴때 까지 실행안되도록 가두어놓기.
        while (true)
        {
            if (endVoice == true)
                break;

            yield return null;
        }
        if (itemName == null)
        {
            Debug.Log("안담김");
        }
        else
        {
            inventorySellCanvas.GetSellItemData(itemName);
            inventorySellCanvas.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);
            
            inventorySellCanvas.OnClickedSell();

            
        }
        


        //대기! - 캐릭터가 "나 뭐팔아?"
        //Voice 인식 활성화 - 사용자 : 00 2개 팔아.

        //SellVoiceCommand() 실행.


    }



    public void SellVoiceCommand(string[] response)
    {
        endVoice = true;
        if (response == null || response.Length == 0)
        {
            Debug.Log("No valid response detected.");
            return;
        }

        //첫 번째 단어로 아이템 이름 확인
        string iName = response[0].ToLower();

        // 다른 인식 단어 넣고 싶을때 사용.
        //string shapeName = response[1];

        UserItem userItem = User.Instance.GetUserItem(iName);
        if (userItem == null || userItem.count <= 0)
        {
            Debug.Log("No matching item found");
        }

        else
        {

            //해당 아이템 이름으로 데이터를 불러오고 Sell 처리
            inventorySellCanvas.GetSellItemData(itemName);

           

            //판매 함수 호출
            inventorySellCanvas.OnClickedSell();

        }


    }
}
