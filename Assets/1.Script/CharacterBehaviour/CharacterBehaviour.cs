using System.Collections;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public BehaviourType type;
    
    //행동이 완성되었는지 아닌지.
    public bool IsComplete;

    public Character character;

    public void Awake()
    {
        character = GetComponentInParent<Character>();
    }
    public virtual void EnterBehaviour()
    {
        IsComplete = false;
    }

    public virtual void UpdateBehaviour()
    {
        
    }


    //모든 행동이 끝나고 상점으로 가게끔
    public virtual void CompleteBehaviour()
    {
        IsComplete = true;
    }
}

public enum BehaviourType
{
    Idle,
    Jogging,
    Fishing,
    Collect,
    LookAround,
    MoveToShop,
    Battle,

    Count,
}