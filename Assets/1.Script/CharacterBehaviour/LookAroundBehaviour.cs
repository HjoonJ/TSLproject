using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAroundBehaviour : CharacterBehaviour
{
    public LookAroundArea[] destinations;
    public LookAroundArea targetDestination; //가고자하는 랜덤 지역
    public LookAroundBehaviour()
    {
        type = BehaviourType.LookAround;

    }

    private void Start()
    {
        destinations = FindObjectsOfType<LookAroundArea>();
    }


    public override void EnterBehaviour()
    {
        //이동 가능한 지역이 담기는 리스트.
        List<LookAroundArea> list = new List<LookAroundArea>();

        for (int i = 0; i < destinations.Length; i++)
        {
            if (destinations[i].character == null)
            {
                list.Add(destinations[i]);
            }
        }

        if (list.Count <= 0)
        {
            character.React(BehaviourType.Idle);
            return;
        }




        int count = list.Count;
        int d = Random.Range(0, count);

   
        targetDestination = destinations[d];
        targetDestination.TakeArea(character);

        character.MoveTo(targetDestination.transform.position);

        // 구경할 지역 찾기 (3군데)
        

    }

    public override void UpdateBehaviour()
    {
        //des 이 위치로 캐릭터 이동 시 호출

        if (targetDestination == null)
            return;

        
        float distance = Vector3.Distance(character.transform.position, targetDestination.transform.position);

        Debug.Log("transform.gameObject.name : " + transform.gameObject.name);
        Debug.Log("targetDestination.gameObject.name : " + targetDestination.gameObject.name);
        Debug.Log("거리 : " + distance);
        if (Vector3.Distance(character.transform.position, targetDestination.transform.position) <= targetDestination.areaDistance)
        
        {
            targetDestination.Arrived(character);
        }
    }
    
}