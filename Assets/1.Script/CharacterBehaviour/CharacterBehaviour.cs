using System.Collections;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public BehaviourType type;
    public virtual void EnterBehaviour()
    {

    }

    public virtual void UpdateBehaviour()
    {

    }
  
}

public enum BehaviourType
{
    Idle,
    Jogging,
    Fishing,
    Collect,
    LookAround,

    Count,
}