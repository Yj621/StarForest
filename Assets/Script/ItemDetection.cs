using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public float detectionRadius = 10f; // 아이템을 감지할 반경
    public LayerMask itemLayer; // 아이템 레이어

    private void Update()
    {
        DetectItemsInRadius();
    }

    private void DetectItemsInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, itemLayer);

        if (colliders.Length > 0)
        {
            // 아이템을 감지한 경우 메시지 출력
            Debug.Log("아이템 감지");
        }
    }
}
