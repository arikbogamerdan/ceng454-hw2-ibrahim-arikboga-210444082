// TakeoffZoneController.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Detects when the aircraft leaves the takeoff runway area.
//              Notifies FlightExamManager that the player has taken off.

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
