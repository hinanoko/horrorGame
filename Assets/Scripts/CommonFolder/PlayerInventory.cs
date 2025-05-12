using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static bool hasKey = false;

    void Awake()
    {
        hasKey = false; // 场景开始时没有钥匙
    }
}