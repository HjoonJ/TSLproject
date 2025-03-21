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

    //행동: 낚시, 채집, 구경하기
    public CharacterBehaviour[] behaviours;
    public CharacterBehaviour curBehaviour; //현재행동

    public Animator animator; // 캐릭터 애니메이션을 제어하는 컨포넌트

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

        // 랜덤한 위치(rPoint)로 캐릭터 이동
        // destinationPoint = RandomPoint();
        // MoveTo(destinationPoint); 

        curHp = maxHp;
    }

    private void Update()
    {
        curBehaviour.UpdateBehaviour();

        // 캐릭터가 특정 위치에 도착했다고 판별하는 if 코드
        if (agent.pathPending==false && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (agent.hasPath==false || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("Walking", false);




                if (arrivedCallback != null)
                {
                    // arrivedCallback 에 담겨있는 함수를 실행하라 (Invoke)
                    arrivedCallback.Invoke();
                    arrivedCallback = null;
                }

            }
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    모든 행동을 멈추고 Shop의 DestinationTr로 이동 및 도착 시 인벤토리  자동 오픈
        //}
    }



    //action 에 따른 캐릭터의 현재 행동을 어떻게 변경할까
    // 넣고싶은 행동 기획하기
    public void React(BehaviourType type)
    {

        
        for (int i = 0; i < behaviours.Length; i++)
        {
            if (behaviours[i].type == type)
            {
                curBehaviour = behaviours[i];
                curBehaviour.EnterBehaviour();
                //현재 행동을 판단해서 어떤 AI를 켤지 판단
                ResponseMatcherFunction.Instance.UpdateResponse();
                break;
            }
        }

    }

    //랜덤하게 위치를 반환하는 함수
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
            // 일정 시간이 지나고 나면 다시 Hp 절반이 차오르고 공격.

            curHp = maxHp / 2;
        }
    }
    
    public virtual void Attack(Enemy enemy)
    {
        // attackPower를 가지고 있음. 

        // 범위를 지정해서 공격?
        //코드로 충돌 확인
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayerMask);
        if (cols.Length <= 0)
        {
            return;
        }
        // 칼로 써는 애니메이션 추가

        int randomNumber = Random.Range(0, 3);
        animator.SetInteger("AttackRandom", randomNumber);
        animator.SetTrigger("Attack");


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Enemy")
            {
                // 휘두를때마다 Character의 TakeDamage 함수에다가 매개변수로 PlayerHunter의 attackPower를 전달해야함.
                
                
                
                Debug.Log($"적에게 데미지 들어가야함 {cols[i].name}");
                cols[i].gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
            }
        }

        
        //Debug.Log("캐릭터가 적을 Attack!!");

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
    //Item 클래스 설정하기 => 객체를 생성하기 위한 설계도
    //클래스를 마음대로 설정할 수 있어야 고수다!!
    [System.Serializable]
public class Item
{
    public string itemName;
    public int count;
    
    //(디폴트 생성자) 참고
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
