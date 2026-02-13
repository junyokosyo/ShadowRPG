using UnityEngine;

[RequireComponent(typeof(Light))]
public class SpotlightVision : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    private Light spotLight;

    void Awake()
    {
        spotLight = GetComponent<Light>();
    }

    void Update()
    {
        if (playerState.IsOnSpotted == false)
        {
            if (playerState == null) return;
            if (!playerState.IsDetecctable) return;

            Vector3 toPlayer = playerState.transform.position - transform.position;
            float distance = toPlayer.magnitude;

            // 距離チェック
            if (distance > spotLight.range) return;

            // 角度チェック
            float angle = Vector3.Angle(transform.forward, toPlayer);
            if (angle <= spotLight.spotAngle * 0.5f)
            {
                playerState.OnSpotted();

            }
        }

    }
}
