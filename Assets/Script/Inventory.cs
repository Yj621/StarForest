using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject canvas;
    private bool isCanvasActive = false;
    private Slot[] slots;  // 슬롯들 배열


    private void Start()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // i 키를 누를 때 Canvas의 활성화 상태를 변경
            isCanvasActive = !isCanvasActive;
            canvas.SetActive(isCanvasActive);
        }
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

}
