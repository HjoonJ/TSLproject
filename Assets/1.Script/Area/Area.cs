using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    public float areaDistance;

    public Character character;
    //�ش������� �����ϸ� ȣ��Ǵ� �Լ�

    public virtual void TakeArea(Character c)
    {
        character = c;
    }

    public virtual void Arrived(Character c)
    {
        character = c;
    }
}
