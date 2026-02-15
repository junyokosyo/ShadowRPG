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
        if (state.IsActionLocked) return;
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
    public System.Collections.IEnumerator MoveToPosition(Vector3 targetPos, float moveSpeed)
    {
        // 1. 操作をロックする
        state.SetActionLocked(true);

        // 2. 目的地に近づくまでループ
        // 高さは無視して平面（X, Z）の距離で判定すると安定します
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                          new Vector3(targetPos.x, 0, targetPos.z));

        while (distance > 0.1f)
        {
            Vector3 dir = (targetPos - transform.position).normalized;

            // 物理移動を継続
            rb.linearVelocity = new Vector3(
                dir.x * moveSpeed,
                rb.linearVelocity.y,
                dir.z * moveSpeed
            );

            // 距離を更新
            distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                        new Vector3(targetPos.x, 0, targetPos.z));

            yield return new WaitForFixedUpdate();
        }

        // 3. 到着したらピタッと止める
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);

        // 4. 操作ロックを解除
        state.SetActionLocked(false);
    }
}
