using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject canvas;
    private bool isCanvasActive = false;


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

}
