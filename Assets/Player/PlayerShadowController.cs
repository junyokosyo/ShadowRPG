using UnityEngine;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] Animator animator;
    [SerializeField] Renderer playerRenderer;

    private Material mat;

    private void Awake()
    {
        mat = playerRenderer.material;
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
        animator.SetBool("IsDiving", isDiving);
        StopAllCoroutines();
        StartCoroutine(DissolveRoutine(isDiving));
    }

    private System.Collections.IEnumerator DissolveRoutine(bool isDiving)
    {
        float target = isDiving ? 1.5f : 0f;

        while (!Mathf.Approximately(mat.GetFloat("_Dissolve"), target))
        {
            float current = mat.GetFloat("_Dissolve");
            float value = Mathf.MoveTowards(current, target, Time.deltaTime * 2f);
            mat.SetFloat("_Dissolve", value);
            yield return null;
        }
    }
}
