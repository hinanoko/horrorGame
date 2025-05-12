using UnityEngine;

public class DoorController : MonoBehaviour
{
<<<<<<< HEAD
    public float interactionRange = 3f;
=======
    public ItemPickup playerPickup;           // æ‹–æ‹½çŽ©å®¶çš„ ItemPickup è„šæœ¬
    public float interactionRange = 3f;       // äº¤äº’è·ç¦»
    private Camera playerCamera;
>>>>>>> 426642d5cd8cf6ba8ebe6945160da9bb82f25bb0
    private bool isOpen = false;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // °´ÏÂE¼üÊ±³¢ÊÔ¿ªÃÅ
        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            TryOpenDoor();
        }
    }

    void TryOpenDoor()
    {
<<<<<<< HEAD
        // ´ÓÆÁÄ»ÖÐÐÄ·¢ÉäÒ»ÌõÉäÏß
=======
        // å‘å°„å°„çº¿ï¼Œåˆ¤æ–­çŽ©å®¶æ˜¯å¦å¯¹ç€å½“å‰è¿™æ‰‡é—¨
>>>>>>> 426642d5cd8cf6ba8ebe6945160da9bb82f25bb0
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.gameObject == this.gameObject) // æ˜¯è¿™æ‰‡é—¨
            {
                if (PlayerInventory.hasKey)
                {
<<<<<<< HEAD
                    OpenDoor();
                }
                else
                {
                    Debug.Log("ÄãÃ»ÓÐÔ¿³×£¬ÎÞ·¨¿ªÃÅ£¡");
=======
                    string itemName = playerPickup.currentItem.name;

                    if (itemName.Contains("GreenKey"))
                    {
                        OpenDoor();
                    }
                    else
                    {
                        Debug.Log("éœ€è¦ç»¿è‰²é’¥åŒ™æ‰èƒ½æ‰“å¼€è¿™æ‰‡é—¨ï¼");
                    }
                }
                else
                {
                    Debug.Log("ä½ æ‰‹ä¸Šä»€ä¹ˆéƒ½æ²¡æœ‰ï¼");
>>>>>>> 426642d5cd8cf6ba8ebe6945160da9bb82f25bb0
                }
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
<<<<<<< HEAD
        // Ðý×ªÃÅ90¶È£¨ÄãÒ²¿ÉÒÔÓÃ¶¯»­£©
        transform.Rotate(Vector3.up, 90f);
        Debug.Log("ÃÅÒÑ´ò¿ª£¡");
        // ¿ÉÑ¡£º¿ªÃÅºóÈÃÔ¿³×ÏûÊ§
        Camera cam = Camera.main;
        if (cam != null)
        {
            Transform holdPoint = cam.transform.Find("KeyHoldPoint");
            if (holdPoint != null && holdPoint.childCount > 0)
            {
                holdPoint.GetChild(0).gameObject.SetActive(false);
            }
        }
=======
        transform.Rotate(Vector3.up, 90f);  // ç®€å•æ—‹è½¬90åº¦
        Debug.Log("é—¨å·²æ‰“å¼€ï¼");
>>>>>>> 426642d5cd8cf6ba8ebe6945160da9bb82f25bb0
    }
}