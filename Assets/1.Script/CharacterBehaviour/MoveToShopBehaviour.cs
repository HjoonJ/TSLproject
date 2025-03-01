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

        character.MoveTo(shopArea.transform.position);
    }

    public override void UpdateBehaviour()
    {


        
        float distance = Vector3.Distance(character.transform.position, shopArea.transform.position);


        if (Vector3.Distance(character.transform.position, shopArea.transform.position) <= shopArea.areaDistance)

        {
            //캐릭터 멈춰세우기.
            character.StopMoving();
            arrived = true;
            shopArea.Arrived(character);

        }

        // 비활성화 되어 있는 InventoryCanvas 오브젝트 활성화 시키기.


    }

}
