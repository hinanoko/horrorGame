using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float interactionRange = 3f;
    private bool isOpen = false;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // 按下E键时尝试开门
        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            TryOpenDoor();
        }
    }

    void TryOpenDoor()
    {
        // 从屏幕中心发射一条射线
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (PlayerInventory.hasKey)
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("你没有钥匙，无法开门！");
                }
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        // 旋转门90度（你也可以用动画）
        transform.Rotate(Vector3.up, 90f);
        Debug.Log("门已打开！");
        // 可选：开门后让钥匙消失
        Camera cam = Camera.main;
        if (cam != null)
        {
            Transform holdPoint = cam.transform.Find("KeyHoldPoint");
            if (holdPoint != null && holdPoint.childCount > 0)
            {
                holdPoint.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}