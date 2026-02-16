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

    [SerializeField] private float moveDuration = 1.5f;

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

        isInsideB = !isInsideB;
        Transform targetPos = isInsideB ? posB : posA;

        camA.Priority = isInsideB ? 0 : 10;
        camB.Priority = isInsideB ? 10 : 0;

        yield return StartCoroutine(player.MoveToPosition(targetPos.position, moveDuration));

        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
}