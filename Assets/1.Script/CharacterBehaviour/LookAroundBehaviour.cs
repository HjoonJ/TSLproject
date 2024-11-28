using System.Collections;
using UnityEngine;


public class LookAroundBehaviour : CharacterBehaviour
{
    public LookAroundArea[] destinations;
    public LookAroundArea targetDestination; //가고자하는 랜덤 지역
    public LookAroundBehaviour()
    {
        type = BehaviourType.LookAround;

    }


    public override void EnterBehaviour()
    {
        int count = destinations.Length;
        int d = Random.Range(0, count);

        targetDestination = destinations[d];
        Character.Instance.MoveTo(targetDestination.transform.position);

        // 구경할 지역 찾기 (3군데)
        

    }

    public override void UpdateBehaviour()
    {
        //des 이 위치로 캐릭터 이동 시 호출

        if (targetDestination == null)
            return;

        
        float distance = Vector3.Distance(Character.Instance.transform.position, targetDestination.transform.position);

        Debug.Log("transform.gameObject.name : " + transform.gameObject.name);
        Debug.Log("targetDestination.gameObject.name : " + targetDestination.gameObject.name);
        Debug.Log("거리 : " + distance);
        if (Vector3.Distance(Character.Instance.transform.position, targetDestination.transform.position) <= targetDestination.areaDistance)
        
        {
            targetDestination.Arrived();
        }
    }
    
}