using UnityEngine;
using UnityEngine.UI;

public class ScrollReveal : MonoBehaviour
{
    [Header("Requirements")]
    public string requiredVaseId = "vase.002";
    public float interactionRange = 2f;

    [Header("UI References")]
    public Canvas scrollCanvas;
    public Text scrollText;
    public Image backgroundPanel;

    [TextArea(5, 10)]
    public string poemText = @"Where velvet dreams begin at rest.
Then inked thoughts rise from the desk.
Beyond the stones, I fade throughout.
Atop the spire, beneath the stars I wait.

Only by holding my scepter and following in my footsteps can escape from the castle";

    private bool isRevealed = false;
    private Camera mainCamera;
    private ItemPickup itemPickup;
    private PlayerController playerController;

    void Start()
    {
        mainCamera = Camera.main;
        itemPickup = FindObjectOfType<ItemPickup>();
        playerController = FindObjectOfType<PlayerController>();

        // Hide UI at start
        if (scrollCanvas != null)
        {
            scrollCanvas.gameObject.SetActive(false);
        }

        // Set the text
        if (scrollText != null)
        {
            scrollText.text = poemText + "\n\n(Press ESC to close)";
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            TryInteract();
        }

        if (isRevealed && Input.GetKeyDown(KeyCode.Escape))
        {
            HideScroll();
        }
    }

    void TryInteract()
    {
        if (isRevealed) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (itemPickup != null && itemPickup.isHoldingItem && 
                    itemPickup.currentItem != null && 
                    itemPickup.currentItem.name.Contains(requiredVaseId))
                {
                    ShowScroll();
                }
            }
        }
    }

    void ShowScroll()
    {
        isRevealed = true;
        if (playerController != null)
        {
            playerController.UnlockCursor();
            Time.timeScale = 0f;
        }

        if (scrollCanvas != null)
        {
            scrollCanvas.gameObject.SetActive(true);
        }
    }

    void HideScroll()
    {
        if (!isRevealed) return;
        
        isRevealed = false;
        if (scrollCanvas != null)
        {
            scrollCanvas.gameObject.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.LockCursor();
            Time.timeScale = 1f;
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        HideScroll();
    }
} 