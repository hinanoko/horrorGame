using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollOpener : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Canvas scrollCanvas;
    [SerializeField] private TextMeshProUGUI scrollText;
    
    [Header("Settings")]
    [SerializeField] private float interactionRange = 5f;
    
    private bool isInspecting = false;
    private Camera mainCamera;
    private ItemPickup itemPickup;

    private void Start()
    {
        // Hide UI at start
        if (scrollCanvas != null)
        {
            scrollCanvas.gameObject.SetActive(false);
        }
        
        mainCamera = Camera.main;
        itemPickup = FindObjectOfType<ItemPickup>();

        // Lock cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isInspecting) // Right click
        {
            TryReadScroll();
        }
        else if (isInspecting && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseScroll();
        }
    }

    private void TryReadScroll()
    {
        if (mainCamera == null || scrollCanvas == null || itemPickup == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Check if player is holding the vase
                if (itemPickup.isHoldingItem && 
                    itemPickup.currentItem != null && 
                    itemPickup.currentItem.name.Contains("vase.002"))
                {
                    OpenScroll();
                }
                else
                {
                    Debug.Log("You need to hold the special vase to read this scroll.");
                }
            }
        }
    }

    private void OpenScroll()
    {
        isInspecting = true;
        scrollCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    private void CloseScroll()
    {
        isInspecting = false;
        scrollCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}