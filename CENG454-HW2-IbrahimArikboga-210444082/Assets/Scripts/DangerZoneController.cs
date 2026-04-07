using System.Collections;
using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;

    [Header("Settings")]
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        Debug.Log("[DangerZoneController] Player entered danger zone.");
        examManager.EnterDangerZone();

        if (activeCountdown != null)
            StopCoroutine(activeCountdown);
        activeCountdown = StartCoroutine(MissileCountdown(other.transform));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[DangerZoneController] Player exited danger zone.");

        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        if (missileLauncher != null) missileLauncher.DestroyActiveMissile();
        examManager.ExitDangerZone();
    }

    private IEnumerator MissileCountdown(Transform playerTransform)
    {
        Debug.Log($"[DangerZoneController] Missile launching in {missileDelay} seconds...");
        yield return new WaitForSeconds(missileDelay);
        Debug.Log("[DangerZoneController] Launching missile!");
        if (missileLauncher != null) missileLauncher.Launch(playerTransform);
        activeCountdown = null;
    }
}
