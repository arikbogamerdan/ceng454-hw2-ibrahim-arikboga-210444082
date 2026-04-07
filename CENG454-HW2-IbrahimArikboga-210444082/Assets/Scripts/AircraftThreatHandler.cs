using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform        respawnPoint;
    [SerializeField] private AudioSource      hitAudioSource;
    [SerializeField] private FlightExamManager examManager;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Missile")) return;

        Debug.Log("[AircraftThreatHandler] Hit by missile!");

        if (hitAudioSource != null)
            hitAudioSource.Play();

        if (examManager != null)
            examManager.OnMissileHit();

        Destroy(other.gameObject);
        ResetAircraft();
    }

    private void ResetAircraft()
    {
        if (respawnPoint == null) return;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
        Debug.Log("[AircraftThreatHandler] Aircraft reset to respawn point.");
    }
}
