using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    //아이템 획득 시 InventoryPanel 하나씩 생성 (중복방지)

    public GameObject inventoryPrefab;

    public Transform inventoryParentTr;

    // 인벤토리 창의 활성화 상태 관리
    public bool inventoryOpen = false;

    private static InventoryCanvas instance;
    
    // 비활성화 되어 있는 컨포넌트를 쓰고 싶을 때 사용하는 속성 관련 내용.
    public static InventoryCanvas Instance //속성(Property)
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<InventoryCanvas>(FindObjectsInactive.Include);

            return instance;
        }
    }


    private void Start()
    {

        InventoryUpdate();

        // 시작 시 인벤토리 비활성화
        gameObject.SetActive(false);

    }
    // I 키를 누르면 인벤토리창 열리기.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpen = !inventoryOpen;

            // 인벤 활성화 또는 비활성화 (반전시키기)
            gameObject.SetActive(inventoryOpen); 
        }
    }

    // 스타트는 한번만 불리는데 아이템을 추가적으로 습득하면 업데이트가 안되는 현상 고치기.


    List<GameObject> inventoryPanels = new List<GameObject>();
    public void InventoryUpdate()
    {
      

        for(int i = 0; i < inventoryPanels.Count; i++)
        {
            inventoryPanels[i].SetActive(false);
        }


        if (User.Instance.userData.userItems.Count <= 0)
            return;

        for (int i = 0; i < User.Instance.userData.userItems.Count; i++)
        {
            if (User.Instance.userData.userItems[i].count <= 0)
                continue; // 반복문의  }를 만났을 때와 같은 처리 -반복문을 다시 시작

            GameObject inventoryPanel = null; //현재 비활성화된 inventoryPanel 담기

            //재활용 가능한게 있는지 확인!!
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
            panel.SetItem(User.Instance.userData.userItems[i]);
        }
    }

    public void ItemCollected()
    {
        if (gameObject.activeSelf)
        InventoryUpdate();
    }



    // 상점 만들기- 현재 판매할 수 있는 아이템들이 나열되고 판매 버튼 누르면 일괄 판매되도록 처리. 돈도 얻어야함.
    
    // S 키를 누르면 모든 행동을 멈추고 샵으로 이동 
    
    // 샵(DestinationTr)에 도착하는 순간 Idle 상태나 다른 행동 애니메이션 상태로 전환 후 인벤토리 오픈

    // (패널 오픈 시 판매 및 구매 버튼 생성 혹은 활성화)

    // 실제 요일에 따라 판매 및 구매 아이템 가격이 변동 

    // 플레이어가 구매 및 판매자? 그렇게 되면 샵으로 이동하라고 하는게 조금 이상해짐. (조언이 필요)- 자동화 로봇이 산다?

}
