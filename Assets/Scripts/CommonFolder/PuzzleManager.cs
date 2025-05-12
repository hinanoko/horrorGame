using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public PuzzleItem[] items; // 拖入四个道具
    public GameObject key;     // 拖入钥匙物体

    void Awake()
    {
        Instance = this;
        if (key != null)
            key.SetActive(false); // 开始时钥匙隐藏
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
            Vector3 spawnPos = player.transform.position + player.transform.forward * 1.0f + Vector3.up * 0.5f;
            key.transform.position = spawnPos;
            key.SetActive(true);
        }
        else
        {
            // 没找到玩家就直接显示钥匙
            key.SetActive(true);
        }
      }
   }
}