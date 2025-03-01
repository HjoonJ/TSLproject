using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    public float areaDistance;

    public Character character;
    //해당지역에 도착하면 호출되는 함수

    public virtual void TakeArea(Character c)
    {
        character = c;
    }

    public virtual void Arrived(Character c)
    {
        character = c;
    }
}
