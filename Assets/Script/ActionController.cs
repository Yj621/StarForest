using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private RaycastHit2D hitInfo;  // 2D 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private Text actionText;  // 행동을 보여 줄 텍스트

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        hitInfo = Physics2D.Raycast(transform.position, Vector2.right, range, layerMask);

        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
                //Debug.Log("CheckItem");
            }
        }
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        //actionText.gameObject.SetActive(true);
        //Debug.Log("ItemInfoAppear");
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        //actionText.gameObject.SetActive(false);
    }

    [SerializeField]
    private Inventory theInventory;

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
                Debug.Log("Item picked up");
            }
        }
    }
}
