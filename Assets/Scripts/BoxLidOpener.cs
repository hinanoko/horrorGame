using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLidOpener : MonoBehaviour
{
    public Transform lidTransform;        // 指向盖子的 Transform
    public float openAngle = -90f;        // 打开角度（负值向上翻）
    public float openSpeed = 90f;         // 每秒旋转角速度
    private bool isOpen = false;
    private bool isRotating = false;

    void Update()
    {
        // 鼠标右键点击
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject && !isRotating)
                {
                    // 切换打开状态
                    isOpen = !isOpen;
                    StartCoroutine(RotateLid());
                }
            }
        }
    }

    System.Collections.IEnumerator RotateLid()
    {
        isRotating = true;

        Quaternion startRot = lidTransform.localRotation;
        Quaternion endRot = Quaternion.Euler(openAngle, 0f, 0f);
        if (!isOpen) endRot = Quaternion.identity; // 回归初始角度

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed / Mathf.Abs(openAngle);
            lidTransform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        lidTransform.localRotation = endRot;
        isRotating = false;
    }
}
