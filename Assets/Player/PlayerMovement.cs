using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerInputReader input;

    // 外部参照用
    public Vector3 MoveDirection { get; private set; }

    private Rigidbody rb;
    private PlayerState state;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        state = GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = input.MoveInput;

        // カメラ基準の方向
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // 入力をワールド方向へ変換
        Vector3 rawDir =
            camForward * moveInput.y +
            camRight * moveInput.x;

        // 状態更新
        bool isMoving = rawDir.sqrMagnitude > 0.001f;

        state.SetMoving(isMoving);

        MoveDirection = isMoving ? rawDir.normalized : Vector3.zero;

        // 実移動
        Vector3 velocity = new Vector3(
            MoveDirection.x * speed,
            rb.linearVelocity.y,
            MoveDirection.z * speed
        );

        rb.linearVelocity = velocity;
    }
}
