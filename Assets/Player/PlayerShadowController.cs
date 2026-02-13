using UnityEngine;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] Animator animator;
    [SerializeField] Renderer playerRenderer;
    [SerializeField] PlayerState state;

    private MaterialPropertyBlock mpb;
    private float currentDissolve;
    private float targetDissolve;

    private void Awake()
    {
        mpb = new MaterialPropertyBlock();
        playerRenderer.GetPropertyBlock(mpb);
        currentDissolve = mpb.GetFloat("_Dissolve");
        targetDissolve = currentDissolve;
    }

    private void OnEnable()
    {
        input.OnDiveChanged += HandleDiveChanged;
    }

    private void OnDisable()
    {
        input.OnDiveChanged -= HandleDiveChanged;
    }

    private void HandleDiveChanged(bool isDiving)
    {
        // 状態を更新（ここが重要）
        state.SetDiving(isDiving);

        // 目標値だけ更新
        targetDissolve = isDiving ? 2f : 0f;

        animator.SetBool("IsDiving", isDiving);
    }

    private void Update()
    {
        // 毎フレームなめらかに変化
        currentDissolve = Mathf.MoveTowards(
            currentDissolve,
            targetDissolve,
            Time.deltaTime * 2f
        );

        playerRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Dissolve", currentDissolve);
        playerRenderer.SetPropertyBlock(mpb);
    }
}
