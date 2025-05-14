using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("movement setting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera playerCamera;
    [Header("view setting")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookUpAngle = 90f;
    [SerializeField] private float maxLookDownAngle = -90f;
    [Header("jump setting")]
    [SerializeField] private float jumpForce = 8f;        // 增加跳跃力度
    [SerializeField] private float gravity = -15f;        // 增加重力
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 1.0f;  // 增加地面检测距离

    private float rotationX = 0f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    void Start()
    {
        // 获取组件
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        
        // 设置角色控制器参数
        if (characterController != null)
        {
            characterController.radius = 0.3f;        // 减小碰撞半径
            characterController.height = 1.8f;        // 适当调整高度
            characterController.stepOffset = 0.7f;    // 增加台阶高度
            characterController.slopeLimit = 75f;     // 增加可攀爬斜坡角度
            characterController.skinWidth = 0.1f;     // 增加皮肤宽度
            characterController.minMoveDistance = 0.001f; // 减小最小移动距离
        }
        
        // 锁定鼠标光标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    void CheckGrounded()
    {
        // 检测是否在地面上（使用多点检测）
        Vector3 position = transform.position;
        Vector3 center = position + characterController.center;
        float radius = characterController.radius;
        
        // 中心点检测
        bool centerGrounded = Physics.CheckSphere(center, groundCheckDistance, groundMask);
        // 四周点检测（增加检测点的数量）
        bool frontGrounded = Physics.CheckSphere(center + transform.forward * radius, groundCheckDistance, groundMask);
        bool backGrounded = Physics.CheckSphere(center - transform.forward * radius, groundCheckDistance, groundMask);
        bool leftGrounded = Physics.CheckSphere(center - transform.right * radius, groundCheckDistance, groundMask);
        bool rightGrounded = Physics.CheckSphere(center + transform.right * radius, groundCheckDistance, groundMask);
        // 增加对角线检测点
        bool frontLeftGrounded = Physics.CheckSphere(center + (transform.forward - transform.right).normalized * radius, groundCheckDistance, groundMask);
        bool frontRightGrounded = Physics.CheckSphere(center + (transform.forward + transform.right).normalized * radius, groundCheckDistance, groundMask);
        bool backLeftGrounded = Physics.CheckSphere(center + (-transform.forward - transform.right).normalized * radius, groundCheckDistance, groundMask);
        bool backRightGrounded = Physics.CheckSphere(center + (-transform.forward + transform.right).normalized * radius, groundCheckDistance, groundMask);
        
        // 只要有一个点检测到地面就认为在地面上
        isGrounded = centerGrounded || frontGrounded || backGrounded || leftGrounded || rightGrounded ||
                    frontLeftGrounded || frontRightGrounded || backLeftGrounded || backRightGrounded;

        // 如果在地面上且垂直速度为负，则设置垂直速度
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; // 减小负值，使角色更容易走上台阶
        }
    }
    
    void HandleMovement()
    {
        // 获取输入
        float horizontal = Input.GetAxisRaw("Horizontal"); // A D
        float vertical = Input.GetAxisRaw("Vertical");     // W S
        
        // 获取相机的前方和右方（基于Y轴）
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        
        // 将Y分量设为0，保持水平移动
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        
        // 计算移动方向
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        
        // 应用移动（增加上坡时的移动速度）
        if (moveDirection.magnitude >= 0.1f)
        {
            float currentSpeed = moveSpeed;
            // 如果在上坡，增加速度
            if (isGrounded && Vector3.Dot(moveDirection, Vector3.up) > 0.1f)
            {
                currentSpeed *= 1.5f;  // 增加上坡速度提升
            }
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // 应用重力
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        characterController.Move(velocity * Time.deltaTime);
    }
    
    void HandleMouseLook()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // 垂直旋转（上下看）
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, maxLookDownAngle, maxLookUpAngle);
        transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, 0);
        
        // 水平旋转（左右看）
        transform.Rotate(Vector3.up * mouseX);
    }
    
    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    
    // 解锁鼠标的方法（用于打开菜单时）
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // 锁定鼠标的方法
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}