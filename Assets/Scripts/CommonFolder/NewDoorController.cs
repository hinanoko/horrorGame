using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorController : MonoBehaviour
{
    public float interactionRange = 3f;
    public float doorOpenAngle = 90f;
    public float doorOpenSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + doorOpenAngle, 0);
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
        }
    }

    void TryOpenDoor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // 判断玩家是否持有钥匙
                if (ItemPickup.instance != null && ItemPickup.instance.currentItemName == "GreenKey")
                {
                    isOpen = true;
                    Debug.Log("门打开了！");
                }
                else
                {
                    Debug.Log("你需要钥匙才能打开这扇门。");
                }
            }
        }
    }
}
