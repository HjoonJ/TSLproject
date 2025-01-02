using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectBehaviour : CharacterBehaviour
{
    public CollectingArea[] collectingAreas;  // 수집 지역
    
    public CollectingArea targetArea;

    public bool arrived;

    //public CollectingArea1 area1;


    public CollectBehaviour()
    {
        type = BehaviourType.Collect;

    }

    private void Start()
    {
        collectingAreas = FindObjectsOfType<CollectingArea>();
    }

    // 나무에 열린 열매를 채집하거나 땅에 있는 식물 채집
    public override void EnterBehaviour()
    {
        List<CollectingArea> list = new List<CollectingArea>();

        for (int i = 0; i < collectingAreas.Length; i++)
        {
            if (collectingAreas[i].canCollecting == true)
            {
                list.Add (collectingAreas[i]);
            }
        }
        
        if (list.Count <= 0)
        {
            Character.Instance.React(BehaviourType.Idle);
            return;
        }

        int count = list.Count;
        int d = Random.Range(0, count);

        targetArea = collectingAreas[d];
        Character.Instance.MoveTo(targetArea.transform.position);
    }
    public override void UpdateBehaviour()
    {
        

        if (targetArea == null)
            return;

        if (arrived == true)
            return;


        // 도착 후 수집
        float distance = Vector3.Distance(Character.Instance.transform.position, targetArea.transform.position);

        
        if (Vector3.Distance(Character.Instance.transform.position, targetArea.transform.position) <= targetArea.areaDistance)

        {
            //캐릭터 멈춰세우기.
            Character.Instance.StopMoving();
            arrived = true;
            targetArea.Arrived();

        }
        
        //// 수집 대상이 없을 경우 행동 완료
        //if (targetArea.canCollecting == false)
        //{
        //    Debug.Log("모든 수집 완료");
            
        //    CompleteBehaviour(); // CollectBehaviour 종료
        //}
    }

    public override void CompleteBehaviour()
    {
        //부모 클래스의 함수 호출하기.
        base.CompleteBehaviour();

        //shop 으로 이동.
        for (int i = 0; i < User.Instance.userData.userItems.Count; i++)
        {
            //itemName에 해당하는 아이템이 몇개인지
            if (User.Instance.userData.userItems[i].count >= 5)
            {
                // 현재 행동을 마치고 다음 행동으로 Shop Area로 이동 (어느타이밍이 모든 행동을 마치는 건지 알아야함)

                Debug.Log("5개 아이템수집");

                //MoveToShopArea(); - 행동 마무리까지 포함.
                Character.Instance.React(BehaviourType.MoveToShop);

            }

        }

        

    }
}