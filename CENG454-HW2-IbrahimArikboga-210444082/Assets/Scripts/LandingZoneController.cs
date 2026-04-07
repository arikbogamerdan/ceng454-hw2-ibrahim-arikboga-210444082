using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[LandingZoneController] Player entered landing zone.");
        examManager.TryCompleteMission();
    }
}
