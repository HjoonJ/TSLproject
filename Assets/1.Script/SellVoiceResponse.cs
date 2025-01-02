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

        //첫 번째 단어로 아이템 이름 확인
        string itemName = response[0].ToLower();

        // 다른 인식 단어 넣고 싶을때 사용
        //string shapeName = response[1];

        //아이템 이름 확인
        if (itemName == "apple" || itemName == "mushroom" || itemName == "banana")
        {
            //해당 아이템 이름으로 데이터를 불러오고 Sell 처리
            inventorySellCanvas.GetSellItemData(itemName);

            //위 함수를 순차적으로 눈에 보이게 처리. 


            //판매 함수 호출
            inventorySellCanvas.OnClickedSell();

            
        }
        else
        {
            Debug.Log("No matching item found");
        }
    }
}
