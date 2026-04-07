using UnityEngine;

public class TakeoffZoneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[TakeoffZoneController] Player left the takeoff area - takeoff confirmed.");
        examManager.NotifyTakeoff();
    }
}
