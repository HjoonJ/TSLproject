using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopArea : Area
{
    public GameObject shopCanvas;
    public override void Arrived()
    {
        StartCoroutine(shopCanvasOpen());
    }

    IEnumerator shopCanvasOpen()
    {
        yield return new WaitForSeconds(2); // 2초 있다가 실행.
        shopCanvas.SetActive(true);

        
    }



    public void SellItemVoice(String[] info) // ex.사과, 버섯, 바나나 이러한 각각의 아이템 네임만 인식 
    {
        // info 배열로 전달되는 아이템 팔기.
        foreach (string e in info) 
        { 
            //User 스크립트안 GetUserItem 를 써야함. 
        }


    }

}
