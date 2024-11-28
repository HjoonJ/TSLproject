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

        float distance = Vector3.Distance(Character.Instance.transform.position, targetArea.transform.position);

        
        if (Vector3.Distance(Character.Instance.transform.position, targetArea.transform.position) <= targetArea.areaDistance)

        {
            //캐릭터 멈춰세우기.
            Character.Instance.StopMoving();
            arrived = true;
            targetArea.Arrived();

        }

    }
}