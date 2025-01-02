using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingArea : Area
{
    public bool canCollecting = true;

    public Collection[] collections;
    public int collectedTargets = 0;  // 수집된 물건

    public string collectMotion;

    // 열매나 채소는 씬 상에 존재하고 3초 후부터 딸 수 있는 상태.
    public void Start()
    {
        //apples[0].GetComponent<Collection>().collectionName;


        //apples.SetActive(false);

        //for(int i = 0; i < collections.Length; i++)
        //{
        //    collections[i].gameObject.SetActive(false);
        //}

        foreach (Collection collection in collections)
        {
            collection.gameObject.SetActive(false);
        }

        //게임 시작 후 3초 후부터 수집 가능 
        StartCoroutine(EnableApples(3));
    }

    // 캐릭터가 물체가 있는지 없는지 확인 해야함.

    public override void Arrived()
    {
        //활성화되어 있는 사과들을 순차적으로 접근하는 코드 필요.
        Debug.Log("수집 지역 도착");
        StartCoroutine(CollectApple()); // 코루틴실행시 서브 스레드 형성된거 "처럼" 실행됨

       
    }

    public void Update()
    {
        canCollecting = false;

        for (int i = 0; i < collections.Length; i++)
        {
            if (collections[i].gameObject.activeSelf)
            {
                canCollecting = true;
                break;
            }
        }


    }


    // 해당 지역에 오브젝트가 남아있다면 계속해서 해당 오브젝트 밑으로 이동 후 수집. 없으면 Idle 
    public IEnumerator CollectApple() //서브 쓰레드 "처럼" 동작되기 때문.
    {
        for (int i = 0; i < collections.Length; i++)
        {
            if (collections[i].gameObject.activeSelf)
            {
                Debug.Log($"{i}번째에 해당하는 오브젝트 수집하기");
                Vector3 applePoint = collections[i].gameObject.transform.position;
                applePoint.y = 0;
                bool arrived = false;
                Character.Instance.MoveTo(applePoint, () =>
                {
                    Debug.Log("목적지에 도착함!");
                    arrived = true;

                });

                //  Debug.Log("목적지에 도착함!"); 이 로그가 출력되기 전까지 코드를 지연

                //WaitUntil([함수!])
                yield return new WaitUntil(() => {
                    return arrived; //리턴하는 값이 true 일 때까지 기다림.
                });

                if (string.IsNullOrEmpty(collectMotion) ==false)
                    Character.Instance.animator.Play(collectMotion);

                yield return null;
                float animTime = Character.Instance.animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animTime);

                //어느 시점! 누구를 2분있다가 활성화 시키기!!
                collections[i].gameObject.SetActive(false);
                
                StartCoroutine(RespawnCollection(collections[i]));

                //획득하는 처리. (캐릭터가 데이터를 가지고 있게끔)

                User.Instance.AddItem(collections[i].collectionName, 1);
            }
        }



        yield return new WaitForSeconds(2);  // 다음 수집까지 대기
        //수집후 Idle 상태로 전환
        Character.Instance.React(BehaviourType.Idle);
    }

    private IEnumerator EnableApples(float d)
    {
        yield return new WaitForSeconds(d);

        foreach (Collection collection in collections)
        {
            collection.gameObject.SetActive(true);
        }

    }

    private IEnumerator RespawnCollection(Collection collection)
    {
        yield return new WaitForSeconds(120); // 2분 대기
        collection.gameObject.SetActive(true);

        Debug.Log("다시 활성화");
    }
}
