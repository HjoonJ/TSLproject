using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Character : MonoBehaviour
{
    public NavMeshAgent agent;

    public static Character Instance;

    public LayerMask enemyLayerMask;

    public float maxHp;
    public float curHp; // current hp

    //public float maxShield = 100;
    //public float curShield; // current shield

    //�ൿ: ����, ä��, �����ϱ�
    public CharacterBehaviour[] behaviours;
    public CharacterBehaviour curBehaviour; //�����ൿ

    public Animator animator; // ĳ���� �ִϸ��̼��� �����ϴ� ������Ʈ

    Vector3 destinationPoint;

    Action arrivedCallback;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        behaviours = GetComponentsInChildren<CharacterBehaviour>();
    }

    private void Start()
    {
        React(BehaviourType.Idle);

        // ������ ��ġ(rPoint)�� ĳ���� �̵�
        // destinationPoint = RandomPoint();
        // MoveTo(destinationPoint); 

        curHp = maxHp;
    }

    private void Update()
    {
        curBehaviour.UpdateBehaviour();

        // ĳ���Ͱ� Ư�� ��ġ�� �����ߴٰ� �Ǻ��ϴ� if �ڵ�
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("Walking", false);

                if (arrivedCallback != null)
                {
                    // arrivedCallback �� ����ִ� �Լ��� �����϶� (Invoke)
                    arrivedCallback.Invoke();
                    arrivedCallback = null;
                }

            }
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ��� �ൿ�� ���߰� Shop�� DestinationTr�� �̵� �� ���� �� �κ��丮  �ڵ� ����
        //}
    }



    //action �� ���� ĳ������ ���� �ൿ�� ��� �����ұ�
    // �ְ���� �ൿ ��ȹ�ϱ�
    public void React(BehaviourType type)
    {
        for (int i = 0; i < behaviours.Length; i++)
        {
            if (behaviours[i].type == type)
            {
                curBehaviour = behaviours[i];
                curBehaviour.EnterBehaviour();
                //���� �ൿ�� �Ǵ��ؼ� � AI�� ���� �Ǵ�
                ResponseMatcherFunction.Instance.UpdateResponse();
                break;
            }
        }

    }

    //�����ϰ� ��ġ�� ��ȯ�ϴ� �Լ�
    public Vector3 RandomPoint()
    {
        float x = UnityEngine.Random.Range(-5f, 5f);
        float z = UnityEngine.Random.Range(-5f, 5f);

        Vector3 randomPoint = new Vector3(x, 0, z);

        return randomPoint;

    }

    public void MoveTo(Vector3 des, Action aCallback = null)
    {
        arrivedCallback = aCallback;
        agent.isStopped = false;
        animator.SetBool("Walking", true);
        destinationPoint = des;
        agent.SetDestination(destinationPoint);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void TakeDamage(float damage)
    {

        curHp -= damage;
        if (curHp <= 0)
        {
            // ���� �ð��� ������ ���� �ٽ� Hp ������ �������� ����.

            curHp = maxHp / 2;
        }
    }
    
    public void Attack()
    {


    }

}
    //Item Ŭ���� �����ϱ� => ��ü�� �����ϱ� ���� ���赵
    //Ŭ������ ������� ������ �� �־�� �����!!
    [System.Serializable]
public class Item
{
    public string itemName;
    public int count;
    
    //(����Ʈ ������) ����
    //public Item()
    //{

    //}

    public Item(string itemName, int count)
    {
        this.itemName = itemName;
        this.count = count;

    }
    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Item/" + itemName);
    }
}
