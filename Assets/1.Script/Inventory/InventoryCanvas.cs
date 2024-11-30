using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    //������ ȹ�� �� InventoryPanel �ϳ��� ���� (�ߺ�����)

    public GameObject inventoryPrefab;

    public Transform inventoryParentTr;

    // �κ��丮 â�� Ȱ��ȭ ���� ����
    public bool inventoryOpen = false;

    public static InventoryCanvas Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
      
    }
    private void Start()
    {

        InventoryUpdate();

        // ���� �� �κ��丮 ��Ȱ��ȭ
        gameObject.SetActive(false);

    }
    // E Ű�� ������ �κ��丮â ������.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpen = !inventoryOpen;

            // �κ� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ (������Ű��)
            gameObject.SetActive(inventoryOpen); 
        }
    }

    // ��ŸƮ�� �ѹ��� �Ҹ��µ� �������� �߰������� �����ϸ� ������Ʈ�� �ȵǴ� ���� ��ġ��.


    List<GameObject> inventoryPanels = new List<GameObject>();
    public void InventoryUpdate()
    {
      

        for(int i = 0; i < inventoryPanels.Count; i++)
        {
            inventoryPanels[i].SetActive(false);
        }


        if (Character.Instance.itemList.Count <= 0)
            return;

        for (int i = 0; i < Character.Instance.itemList.Count; i++)
        {
            if (Character.Instance.itemList[i].count <= 0)
                continue; // �ݺ�����  }�� ������ ���� ���� ó�� -�ݺ����� �ٽ� ����

            GameObject inventoryPanel = null; //���� ��Ȱ��ȭ�� inventoryPanel ���

            //��Ȱ�� �����Ѱ� �ִ��� Ȯ��!!
            for (int j= 0; j < inventoryPanels.Count; j++)
            {
                if (inventoryPanels[j].activeSelf == false)
                {
                    inventoryPanel = inventoryPanels[j];
                    inventoryPanel.SetActive(true);
                    break;
                }
            }
            
            if (inventoryPanel == null)
            {
                inventoryPanel = Instantiate(inventoryPrefab, inventoryParentTr);
                inventoryPanels.Add(inventoryPanel);
            }
            
            InventoryPanel panel = inventoryPanel.GetComponent<InventoryPanel>();
            panel.SetItem(Character.Instance.itemList[i]);
        }
    }

    public void ItemCollected()
    {
        if (gameObject.activeSelf)
        InventoryUpdate();
    }



    // ���� �����- ���� �Ǹ��� �� �ִ� �����۵��� �����ǰ� �Ǹ� ��ư ������ �ϰ� �Ǹŵǵ��� ó��. ���� ������.
    
    // S Ű�� ������ ��� �ൿ�� ���߰� ������ �̵� 
    
    // ��(DestinationTr)�� �����ϴ� ���� Idle ���³� �ٸ� �ൿ �ִϸ��̼� ���·� ��ȯ �� �κ��丮 ����

    // (�г� ���� �� �Ǹ� �� ���� ��ư ���� Ȥ�� Ȱ��ȭ)

    // ���� ���Ͽ� ���� �Ǹ� �� ���� ������ ������ ���� 

    // �÷��̾ ���� �� �Ǹ���? �׷��� �Ǹ� ������ �̵��϶�� �ϴ°� ���� �̻�����. (������ �ʿ�)- �ڵ�ȭ �κ��� ���?

}
