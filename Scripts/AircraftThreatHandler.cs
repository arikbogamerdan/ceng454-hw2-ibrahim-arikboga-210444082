// AircraftThreatHandler.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Attached to the aircraft. Handles what happens when a missile hits it.
//              Resets the aircraft to the respawn point on hit.

using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform        respawnPoint;
    [SerializeField] private AudioSource      hitAudioSource;
    [SerializeField] private FlightExamManager examManager;

    // 3-G: Rigidbody cached in Start
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 3-H: React only when hit by a missile
        if (!other.CompareTag("Missile")) return;

        Debug.Log("[AircraftThreatHandler] Hit by missile!");

        // Play hit sound
        if (hitAudioSource != null)
            hitAudioSource.Play();

        // Tell the mission manager
        if (examManager != null)
            examManager.OnMissileHit();

        // Destroy the missile
        Destroy(other.gameObject);

        // Reset aircraft to respawn point
        ResetAircraft();
    }

    private void ResetAircraft()
    {
        if (respawnPoint == null) return;

        // Stop all physics motion
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport back to spawn
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        Debug.Log("[AircraftThreatHandler] Aircraft reset to respawn point.");
    }
}
