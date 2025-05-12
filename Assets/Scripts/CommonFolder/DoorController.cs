using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float interactDistance = 2f;
    private bool isOpen = false;

    void Update()
    {
        if (isOpen) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        if (Vector3.Distance(player.transform.position, transform.position) < interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E) && PlayerInventory.hasKey)
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;

        // 移除门
        Destroy(gameObject);

        // 隐藏手上的钥匙
        Camera cam = Camera.main;
        if (cam != null)
        {
            Transform holdPoint = cam.transform.Find("KeyHoldPoint");
            if (holdPoint != null && holdPoint.childCount > 0)
            {
                holdPoint.GetChild(0).gameObject.SetActive(false);
            }
        }

        Debug.Log("门已被移除！");
    }
}