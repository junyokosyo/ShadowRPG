using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerState))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerInputReader input;

    // 外部参照用（Visualや攻撃用）
    public Vector3 MoveDirection { get; private set; }

    private Rigidbody rb;
    private PlayerState state;
    private Vector2 cachedInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        state = GetComponent<PlayerState>();

        // 勝手に回転しないように
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // 入力は Update
        cachedInput = input.MoveInput;
    }

    private void FixedUpdate()
    {
        // カメラ基準ベクトル
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // 入力 → ワールド方向
        Vector3 rawDir =
            camForward * cachedInput.y +
            camRight * cachedInput.x;

        bool isMoving = rawDir.sqrMagnitude > 0.001f;
        state.SetMoving(isMoving);

        MoveDirection = isMoving ? rawDir.normalized : Vector3.zero;

        // ★左右だけ向きを更新
        state.SetFacing(cachedInput.x);

        // 実際の移動
        rb.linearVelocity = new Vector3(
            MoveDirection.x * speed,
            rb.linearVelocity.y,
            MoveDirection.z * speed
        );
    }
}
