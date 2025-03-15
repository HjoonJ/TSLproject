using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public NavMeshAgent agent;

    //public static Character Instance;

    public CharacterType type;

    public LayerMask enemyLayerMask;

    public float maxHp;
    public float curHp; // current hp

    public int attackSpeed;
    [SerializeField] public float attackRange = 1f;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public int attackPower;

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
        //if (Instance == null)
        //    Instance = this;

        behaviours = GetComponentsInChildren<CharacterBehaviour>();
    }

    public void ChangeGameMode(GameMode gameMode)
    {
        if (gameMode == GameMode.Battle)
        {
            React(BehaviourType.Battle);
            animator.SetLayerWeight(animator.GetLayerIndex("Battle"),1);
        }
        else
        {
            React(BehaviourType.Idle);
            animator.SetLayerWeight(animator.GetLayerIndex("Battle"), 0);
        }
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
        if (agent.pathPending==false && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (agent.hasPath==false || agent.velocity.sqrMagnitude == 0)
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
    
    public virtual void Attack(Enemy enemy)
    {
        // attackPower�� ������ ����. 

        // ������ �����ؼ� ����?
        //�ڵ�� �浹 Ȯ��
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayerMask);
        if (cols.Length <= 0)
        {
            return;
        }
        // Į�� ��� �ִϸ��̼� �߰�

        int randomNumber = Random.Range(0, 3);
        animator.SetInteger("AttackRandom", randomNumber);
        animator.SetTrigger("Attack");


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Enemy")
            {
                // �ֵθ������� Character�� TakeDamage �Լ����ٰ� �Ű������� PlayerHunter�� attackPower�� �����ؾ���.
                
                
                
                Debug.Log($"������ ������ ������ {cols[i].name}");
                cols[i].gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
            }
        }

        
        //Debug.Log("ĳ���Ͱ� ���� Attack!!");

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
