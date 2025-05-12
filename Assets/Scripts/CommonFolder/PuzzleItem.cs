using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public bool isActivated = false;
    public Renderer itemRenderer;
    public Color activatedColor = Color.yellow;
    public float glowIntensity = 2f;
    public float interactDistance = 2f;

    void Start()
    {
        if (itemRenderer == null)
            itemRenderer = GetComponent<Renderer>();
        SetGlow(false);
    }

    void Update()
    {
        if (!isActivated && PlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            Activate();
        }
    }

    void Activate()
    {
        isActivated = true;
        SetGlow(true);
        PuzzleManager.Instance.CheckAllItems();
    }

    void SetGlow(bool on)
    {
        if (itemRenderer != null)
        {
            var mat = itemRenderer.material;
            if (on)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", activatedColor * glowIntensity);
            }
            else
            {
                mat.DisableKeyword("_EMISSION");
            }
        }
    }

    bool PlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        return Vector3.Distance(player.transform.position, transform.position) < interactDistance;
    }
}