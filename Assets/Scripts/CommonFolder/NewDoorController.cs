using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorController : MonoBehaviour
{
    public float interactionRange = 3f;
    public float doorOpenAngle = -90f;  // 改为向内开门
    public float doorOpenSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Vector3 originalPosition;
    private BoxCollider doorCollider;

    private void Start()
    {
        closedRotation = transform.rotation;
        // 计算开门后的旋转（绕Z轴旋转，使门向内开）
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + doorOpenAngle, transform.eulerAngles.z);
        originalPosition = transform.position;
        
        // 获取或添加BoxCollider
        doorCollider = GetComponent<BoxCollider>();
        if (doorCollider == null)
        {
            doorCollider = gameObject.AddComponent<BoxCollider>();
        }
        
        // 调整碰撞器大小和位置，确保不会阻挡入口
        doorCollider.size = new Vector3(0.1f, 2f, 1f);  // 减小厚度
        doorCollider.center = new Vector3(0f, 1f, 0f);  // 调整中心点位置
        doorCollider.isTrigger = true;  // 设置为触发器，这样不会物理阻挡玩家
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 右键
        {
            TryOpenDoor();
        }

        if (isOpen)
        {
            // 平滑旋转门
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * doorOpenSpeed);
            
            // 确保门的位置不会偏移
            transform.position = originalPosition;
            
            // 如果门已经完全打开，禁用碰撞器
            if (Quaternion.Angle(transform.rotation, openRotation) < 1f)
            {
                if (doorCollider != null)
                {
                    doorCollider.enabled = false;
                }
            }
        }
    }

    void TryOpenDoor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // 检查玩家是否有钥匙
                if (ItemPickup.instance != null && ItemPickup.instance.currentItemName == "GreenKey")
                {
                    isOpen = true;
                    Debug.Log("门开了！");
                }
                else
                {
                    Debug.Log("你需要钥匙才能打开这扇门。");
                }
            }
        }
    }
}
