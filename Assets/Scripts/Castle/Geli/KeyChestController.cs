using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChestController : MonoBehaviour
{
    [Header("箱子盖子设置")]
    public Transform lid;               // 箱子盖子的 Transform，如果不手动赋值，会在 Awake 时自动查找
    public float openAngle = -90f;      // 打开时盖子绕 X 轴旋转的角度
    public float openSpeed = 2f;        // 开/关 的插值速度

    [Header("钥匙&交互设置")]
    public ItemPickup playerPickup;     // 玩家身上管理“捡起物品”的脚本
    public float interactionRange = 3f; // 射线检测最远距离

    private bool isOpen = false;        // 记录箱子是否已打开
    private Quaternion closedRotation;  // 盖子关闭时的本地旋转
    private Quaternion openRotation;    // 盖子打开时的本地旋转
    private Camera cam;                 // 主摄像机引用

    void Awake()
{
    // 如果 Inspector 里没赋 lid，就尝试在子物体里找名为 "chest_top" 的 Transform
    if (lid == null)
    {
        lid = transform.Find("chest_top");
        if (lid == null)
            Debug.LogError($"[{nameof(KeyChestController)}] 没有在 {gameObject.name} 下找到名为 'chest_top' 的子物体，请检查层级或手动赋值。");
    }
}

    void Start()
    {
        // 记录盖子初始的旋转（关闭状态）
        closedRotation = lid.localRotation;
        // 计算一个在初始基础上绕 X 轴旋转 openAngle 度的目标旋转
        openRotation = closedRotation * Quaternion.Euler(openAngle, 0, 0);
        // 缓存主摄像机
        cam = Camera.main;
    }

    void Update()
    {
        // 右键点击且箱子还没有打开
        if (Input.GetMouseButtonDown(1) && !isOpen)
        {
            TryOpenBox();
        }

        // 始终平滑插值当前旋转到目标旋转
        if (lid != null)
        {
            lid.localRotation = Quaternion.Slerp(
                lid.localRotation,
                isOpen ? openRotation : closedRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    void TryOpenBox()
    {
        // 从鼠标位置发射一条射线
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            // 确保射线打到的正是这个箱子
            if (hit.collider.gameObject == gameObject)
            {
                // 检查玩家是否拿着物品，以及物品是否是“GreenKey”标签
                if (playerPickup.currentItem.name == "GreenKey")
                {
                    isOpen = true;
                    Debug.Log("箱子已打开！");
                }
                else
                {
                    Debug.Log("你需要一把绿色钥匙来打开这个箱子！");
                }
            }
        }
    }
}