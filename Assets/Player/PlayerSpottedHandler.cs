using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.Rendering.Universal;
public class PlayerSpottedHandler : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private RestCS restScript;
    [SerializeField] private float effectDuration = 0.5f;
    private bool hasSpottedEffectPlayed = false;

    void Start()
    {
        playerState.OnSpottedEvent += HandleSpotted;
    }

    void OnDestroy()
    {
        playerState.OnSpottedEvent -= HandleSpotted;
    }

    void HandleSpotted()
    {
        Debug.Log("演出開始");
        // ポストプロセスの演出を開始
        if (postProcessVolume != null)
        {
            StartCoroutine(PlaySpottedPostProcess());
        }
        if (animator != null && hasSpottedEffectPlayed == false)
        {
            animator.SetTrigger("Spotted");
            hasSpottedEffectPlayed = true;
            playerState.SetActionLocked(true);
        }
    }
    private IEnumerator PlaySpottedPostProcess()
    {
        ColorAdjustments colorAdjust;
        if (postProcessVolume.profile.TryGet(out colorAdjust))
        {
            float elapsed = 0f;
            // 0（通常）から 10（真っ白）くらいまで上げる
            float targetExposure = 3.5f;

            while (elapsed < effectDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / effectDuration;

                // 露出を上げて画面を白く飛ばす
                colorAdjust.postExposure.value = Mathf.Lerp(0f, targetExposure, t);

                yield return null;
            }
        }
        yield return new WaitForSeconds(0.3f);
        restScript.Reset();



    }
}