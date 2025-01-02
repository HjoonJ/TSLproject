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
        yield return new WaitForSeconds(2); // 2�� �ִٰ� ����.
        shopCanvas.SetActive(true);

        
    }



    public void SellItemVoice(String[] info) // ex.���, ����, �ٳ��� �̷��� ������ ������ ���Ӹ� �ν� 
    {
        // info �迭�� ���޵Ǵ� ������ �ȱ�.
        foreach (string e in info) 
        { 
            //User ��ũ��Ʈ�� GetUserItem �� �����. 
        }


    }

}
