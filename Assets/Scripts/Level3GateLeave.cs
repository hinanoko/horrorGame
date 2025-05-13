using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3GateLeave : MonoBehaviour
{
    public GameObject[] balls;   // 拖入四个球
    public GameObject door;      // 拖入大门（默认是激活的）

    void Update()
    {
        if (door != null && AllBallsPlaced())
        {
            door.SetActive(false); // 打开大门（隐藏大门）
            enabled = false;       // 禁用自己，避免重复检测
        }
    }

    bool AllBallsPlaced()
    {
        foreach (GameObject ball in balls)
        {
            if (ball == null || !ball.activeSelf)
                return false;
        }
        return true;
    }
}
