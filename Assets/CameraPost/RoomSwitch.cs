using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class RoomSwitch : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineCamera camA;
    [SerializeField] private CinemachineCamera camB;

    [Header("Positions")]
    [SerializeField] private Transform posA; // A地点（部屋A側の出口）
    [SerializeField] private Transform posB; // B地点（部屋B側の出口）

    [SerializeField] private float moveSpeed = 4f;

    // 現在どっちの部屋にいるか（初期状態でAなら false、Bなら true にしておく）
    [SerializeField] private bool isInsideB = false;
    private bool isBusy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isBusy || !other.CompareTag("Player")) return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            StartCoroutine(SwitchRoutine(player));
        }
    }

    private IEnumerator SwitchRoutine(PlayerMovement player)
    {
        isBusy = true;

        // 目的地とカメラを判定
        // BにいないならBへ向かう、BにいるならAへ戻る
        Transform targetPos = isInsideB ? posA : posB;

        // カメラの優先度（Priority）を切り替え
        camA.Priority = isInsideB ? 10 : 0;
        camB.Priority = isInsideB ? 0 : 10;

        // プレイヤーの移動が終わるまで待機
        yield return StartCoroutine(player.MoveToPosition(targetPos.position, moveSpeed));

        // 部屋の状態を反転
        isInsideB = !isInsideB;

        // 連続で発火しないように少しだけクールダウン
        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
}