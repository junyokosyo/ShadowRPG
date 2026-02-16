using UnityEngine;
using System;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    [Header("視界設定")]
    [SerializeField] private float viewRadius = 5f;
    [Range(0, 360)]
    [SerializeField] private float viewAngle = 60f;
    public float ViewRadius => viewRadius;
    public float ViewAngle => viewAngle;

    [Header("検知対象")]
    [SerializeField] private LayerMask targetMask;   // 例: Player
    [SerializeField] private LayerMask obstacleMask; // 例: 壁や障害物

    public Action<Transform> OnTargetSpotted;  // 視界に入ったときのコールバックここ二つは使わないかも
    public Action<Transform> OnTargetLost;     // 視界から出たときのコールバック
    
    private Transform detectedTarget;
    
    void Start()
    {
        StartCoroutine(VisionRoutine());
    }

    IEnumerator VisionRoutine()
    {
        while (true)
        {
            CheckForTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void CheckForTarget()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInView.Length > 0)
        {
            Transform target = targetsInView[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distance, obstacleMask))
                {
                    // プレイヤーが見つけられる状態かチェック
                    PlayerState playerState = target.GetComponent<PlayerState>();
                    if (playerState != null && !playerState.IsDetecctable)
                        return; // 見つけられない（影やダイビング中）
                    // 視界に入った
                    if (detectedTarget != target)
                    {
                        detectedTarget = target;
                        OnTargetSpotted?.Invoke(target);
                        playerState?.SendMessage("OnSpotted");
                    }
                    return;
                }
            }
        }
        // 視界から消えた
        if (detectedTarget != null) { OnTargetLost?.Invoke(detectedTarget); detectedTarget = null; }
    }

    // デバッグ用: Sceneで視界を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftDir = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewRadius);
    }
}
