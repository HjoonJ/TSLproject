using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToShopBehaviour : CharacterBehaviour
{
    public ShopArea shopArea;

    public bool arrived;



    public MoveToShopBehaviour()
    {
        type = BehaviourType.MoveToShop;

    }

    public override void EnterBehaviour()
    {
       
        Character.Instance.MoveTo(shopArea.transform.position);
    }

    public override void UpdateBehaviour()
    {


        
        float distance = Vector3.Distance(Character.Instance.transform.position, shopArea.transform.position);


        if (Vector3.Distance(Character.Instance.transform.position, shopArea.transform.position) <= shopArea.areaDistance)

        {
            //ĳ���� ���缼���.
            Character.Instance.StopMoving();
            arrived = true;
            shopArea.Arrived();

        }

        // ��Ȱ��ȭ �Ǿ� �ִ� InventoryCanvas ������Ʈ Ȱ��ȭ ��Ű��.


    }

}
