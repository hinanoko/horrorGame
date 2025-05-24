using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNewDoorController : MonoBehaviour
{
    public float interactionRange = 3f;
    public float doorOpenAngle = 90f;
    public float doorOpenSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + doorOpenAngle, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // �Ҽ�
        {
            TryOpenDoor();
        }

        if (isOpen)
        {
            // ƽ����ת��
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * doorOpenSpeed);
        }
    }

    void TryOpenDoor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // �ж�����Ƿ����Կ��
                if (ItemPickup.instance != null && ItemPickup.instance.currentItemName == "GreenKey")
                {
                    isOpen = true;
                }
                else
                {
                }
            }
        }
    }
}