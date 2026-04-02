// LandingZoneController.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Detects when the aircraft enters the landing area.
//              Only completes the mission if the player has cleared the threat first.

using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[LandingZoneController] Player entered landing zone.");

        // Delegate mission-completion logic to the manager
        // (Manager will reject it if takeoff or threat-clear hasn't happened)
        examManager.TryCompleteMission();
    }
}
