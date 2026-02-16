using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerState))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerInputReader input;

    // 外部参照用（Visual用）
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

        state.SetFacing(cachedInput.x);

        // 移動
        rb.linearVelocity = new Vector3(
            MoveDirection.x * speed,
            rb.linearVelocity.y,
            MoveDirection.z * speed
        );
    }
    public System.Collections.IEnumerator MoveToPosition(Vector3 targetPos, float duration)
    {
        state.SetActionLocked(true);
        state.SetMoving(true);

        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;

            // 現在の進行度 (0.0 ～ 1.0) を計算
            float t = elapsed / duration;

            // 物理演算で動かしたいので、MovePosition を使うのが最も安定します
            rb.MovePosition(Vector3.Lerp(startPos, targetPos, t));

            // 向きの更新（必要であれば）
            Vector3 dir = (targetPos - startPos).normalized;
            state.SetFacing(dir.x);

            yield return new WaitForFixedUpdate();
        }
        state.SetMoving(false);
        state.SetActionLocked(false);
    }
}
