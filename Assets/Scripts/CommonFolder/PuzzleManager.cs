using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public PuzzleItem[] items; // 拖入四个道具
    public GameObject key;     // 拖入钥匙物体

    void Awake()
    {
        Instance = this;
        if (key != null)
        {
            key.SetActive(false); // 开始时钥匙隐藏
        }
    }

    public void CheckAllItems()
    {
        foreach (var item in items)
        {
            if (!item.isActivated)
                return;
        }
        // 全部激活，钥匙出现在玩家面前
        if (key != null)
        {
            // 找到玩家
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // 让钥匙出现在玩家前方1米处
                Vector3 spawnPos = player.transform.position + player.transform.forward * 1.0f + Vector3.up * 1.5f;
                key.transform.position = spawnPos;
                key.SetActive(true);
                
                // 添加旋转动画
                StartCoroutine(AnimateKey());
                
                // 显示提示
                Debug.Log("A mysterious key has appeared!");
            }
            else
            {
                // 没找到玩家就直接显示钥匙
                key.SetActive(true);
            }
        }
    }
    
    System.Collections.IEnumerator AnimateKey()
    {
        float duration = 2.0f;
        float elapsed = 0f;
        Vector3 startRotation = key.transform.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(0, 360, 0);
        
        // 添加发光材质
        Renderer keyRenderer = key.GetComponent<Renderer>();
        if (keyRenderer != null)
        {
            Material keyMaterial = keyRenderer.material;
            keyMaterial.EnableKeyword("_EMISSION");
            keyMaterial.SetColor("_EmissionColor", Color.yellow * 2f);
        }
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            key.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        
        // 恢复正常材质
        if (keyRenderer != null)
        {
            Material keyMaterial = keyRenderer.material;
            keyMaterial.DisableKeyword("_EMISSION");
        }
    }
}