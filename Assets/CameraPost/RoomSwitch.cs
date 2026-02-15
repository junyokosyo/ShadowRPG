using UnityEngine;
using Unity.Cinemachine;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineCamera currentRoom;
    [SerializeField] private CinemachineCamera nextRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentRoom.Priority = 0;
            nextRoom.Priority = 10;
        }
    }
}
