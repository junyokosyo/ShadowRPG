using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Input : MonoBehaviour
{
    private InputSystem_Actions input;

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;


    private Vector2 moveInput;
    private Rigidbody rb;

    private void Awake()
    {
        input = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }



    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // カメラ基準の方向を取得
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Y方向は使わない（地面移動想定）
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // 入力をカメラ基準で変換
        Vector3 moveDir =
            camForward * moveInput.y +
            camRight * moveInput.x;

        Vector3 velocity = new Vector3(
            moveDir.x * speed,
            rb.linearVelocity.y,
            moveDir.z * speed
        );

        rb.linearVelocity = velocity;
    }


}
