using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket2 : MonoBehaviour
{
    public GameObject bucketBall; // 水桶上的球（默认隐藏）

    void Start()
    {
        // 确保水桶球默认隐藏
        if (bucketBall != null)
        {
            bucketBall.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 右键
        {
            TryInsertBall();
        }
    }

    void TryInsertBall()
    {
        // 检查是否手里拿着球
        if (ItemPickup.instance != null &&
            ItemPickup.instance.isHoldingItem &&
            ItemPickup.instance.currentItemName == "WoodSphere")
        {
            // 判断是否正在看着水桶
            Camera cam = Camera.main;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 2f)) // 近距离检测
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // 销毁手里的球
                    Destroy(ItemPickup.instance.currentItem);
                    ItemPickup.instance.currentItem = null;
                    ItemPickup.instance.isHoldingItem = false;
                    ItemPickup.instance.currentItemName = "";

                    // 显示水桶上的球
                    if (bucketBall != null)
                    {
                        bucketBall.SetActive(true);
                    }

                    Debug.Log("成功把球放进水桶！");
                }
            }
        }
    }
}
