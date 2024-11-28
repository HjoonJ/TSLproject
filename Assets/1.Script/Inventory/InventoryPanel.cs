using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;

public class InventoryPanel : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemCount;

    public void SetItem(Item item)
    {
        //Sprite sprite = Resources.Load<Sprite>("Item/" + item.itemName);
        //itemImage.sprite = item.GetSprite();
        ItemData data = ItemManager.Instance.GetItemData(item.itemName);
        itemImage.sprite = data.thum;
        itemCount.text = item.count.ToString();
    }

    

}
