using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float interactDistance = 2f;
    private bool isPicked = false;

    void Update()
    {
        if (!isPicked)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;

            // 判断玩家距离
            if (Vector3.Distance(player.transform.position, transform.position) < interactDistance)
            {
                // 按E拾取钥匙
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isPicked = true;
                    PlayerInventory.hasKey = true;

                    // 让钥匙跟随第一人称视角
                    Camera cam = Camera.main;
                    if (cam != null)
                    {
                        Transform holdPoint = cam.transform.Find("KeyHoldPoint");
                        if (holdPoint != null)
                        {
                            transform.SetParent(holdPoint);
                            transform.localPosition = Vector3.zero;
                            transform.localRotation = Quaternion.identity;
                            // 可根据需要调整钥匙的局部缩放
                            transform.localScale = Vector3.one * 0.5f;
                        }
                    }
                }
            }
        }
    }
}