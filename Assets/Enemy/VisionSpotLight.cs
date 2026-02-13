using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
public class VisionSpotLight : MonoBehaviour
{
    [SerializeField] private Light visionLight;

    private EnemyVision enemyVision;

    void Awake()
    {
        enemyVision = GetComponent<EnemyVision>();

        if (visionLight == null)
        {
            GameObject lightObj = new GameObject("VisionLight");
            lightObj.transform.parent = transform;
            lightObj.transform.localPosition = Vector3.up * 1.5f; // 高さ調整
            visionLight = lightObj.AddComponent<Light>();
            visionLight.type = LightType.Spot;
            visionLight.color = Color.yellow;
            visionLight.intensity = 2f;
        }
    }

    void Update()
    {
        if (enemyVision != null && visionLight != null)
        {
            visionLight.transform.rotation = transform.rotation;
            visionLight.range = enemyVision.viewRadius;
            visionLight.spotAngle = enemyVision.viewAngle;
        }
    }
}
