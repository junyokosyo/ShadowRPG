using UnityEngine;
using UnityEngine.Rendering;

public class ShadowPostEffect : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Volume volume;

    private void Update()
    {
        float target = playerState.IsDiving ? 0.6f : 0f;

        volume.weight = Mathf.Lerp(
            volume.weight,
            target,
            Time.deltaTime * 5f
        );
    }
}
