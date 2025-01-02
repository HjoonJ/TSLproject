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
    public string itemName; //이걸 왜 써야하는지 떠올려야함.(key 값을 위해)
    

    public void OnClickedPanel()
    {
        InventorySellCanvas inventorySellCanvas = FindObjectOfType<InventorySellCanvas>(true);
        inventorySellCanvas.gameObject.SetActive(true);


        inventorySellCanvas.GetSellItemData(itemName);

    }
    public void SetItem(UserItem item)
    {
        //Sprite sprite = Resources.Load<Sprite>("Item/" + item.itemName);
        //itemImage.sprite = item.GetSprite();
        this.itemName = item.itemName; 
        ItemData data = ItemManager.Instance.GetItemData(item.itemName);
        itemImage.sprite = data.thum;
        itemCount.text = item.count.ToString();
    }

    

}
