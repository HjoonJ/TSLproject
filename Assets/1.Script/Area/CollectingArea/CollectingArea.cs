using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingArea : Area
{
    public bool canCollecting = true;

    public Collection[] collections;
    public int collectedTargets = 0;  // ������ ����

    public string collectMotion;

    // ���ų� ä�Ҵ� �� �� �����ϰ� 3�� �ĺ��� �� �� �ִ� ����.
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

        //���� ���� �� 3�� �ĺ��� ���� ���� 
        StartCoroutine(EnableApples(3));
    }

    // ĳ���Ͱ� ��ü�� �ִ��� ������ Ȯ�� �ؾ���.

    public override void Arrived()
    {
        //Ȱ��ȭ�Ǿ� �ִ� ������� ���������� �����ϴ� �ڵ� �ʿ�.
        Debug.Log("���� ���� ����");
        StartCoroutine(CollectApple()); // �ڷ�ƾ����� ���� ������ �����Ȱ� "ó��" �����

       
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


    // �ش� ������ ������Ʈ�� �����ִٸ� ����ؼ� �ش� ������Ʈ ������ �̵� �� ����. ������ Idle 
    public IEnumerator CollectApple() //���� ������ "ó��" ���۵Ǳ� ����.
    {
        for (int i = 0; i < collections.Length; i++)
        {
            if (collections[i].gameObject.activeSelf)
            {
                Debug.Log($"{i}��°�� �ش��ϴ� ������Ʈ �����ϱ�");
                Vector3 applePoint = collections[i].gameObject.transform.position;
                applePoint.y = 0;
                bool arrived = false;
                Character.Instance.MoveTo(applePoint, () =>
                {
                    Debug.Log("�������� ������!");
                    arrived = true;

                });

                //  Debug.Log("�������� ������!"); �� �αװ� ��µǱ� ������ �ڵ带 ����

                //WaitUntil([�Լ�!])
                yield return new WaitUntil(() => {
                    return arrived; //�����ϴ� ���� true �� ������ ��ٸ�.
                });

                if (string.IsNullOrEmpty(collectMotion) ==false)
                    Character.Instance.animator.Play(collectMotion);

                yield return null;
                float animTime = Character.Instance.animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animTime);

                //��� ����! ������ 2���ִٰ� Ȱ��ȭ ��Ű��!!
                collections[i].gameObject.SetActive(false);
                
                StartCoroutine(RespawnCollection(collections[i]));

                //ȹ���ϴ� ó��. (ĳ���Ͱ� �����͸� ������ �ְԲ�)

                User.Instance.AddItem(collections[i].collectionName, 1);
            }
        }



        yield return new WaitForSeconds(2);  // ���� �������� ���
        //������ Idle ���·� ��ȯ
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
        yield return new WaitForSeconds(120); // 2�� ���
        collection.gameObject.SetActive(true);

        Debug.Log("�ٽ� Ȱ��ȭ");
    }
}
