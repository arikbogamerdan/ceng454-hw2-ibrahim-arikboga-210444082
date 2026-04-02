// DangerZoneController.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Detects aircraft entry/exit of the danger zone trigger volume.
//              Notifies FlightExamManager and starts/cancels the missile countdown.

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
        // Only react to the Player-tagged object
        if (!other.CompareTag("Player")) return;

        Debug.Log("[DangerZoneController] Player entered danger zone.");

        // Immediately tell the manager (shows HUD warning right away)
        examManager.EnterDangerZone();

        // Start the missile countdown (5 seconds)
        if (activeCountdown != null)
            StopCoroutine(activeCountdown);
        activeCountdown = StartCoroutine(MissileCountdown(other.transform));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[DangerZoneController] Player exited danger zone.");

        // Cancel the pending missile launch if still counting down
        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        // Destroy any active missile
        missileLauncher.DestroyActiveMissile();

        // Tell the manager the threat is cleared
        examManager.ExitDangerZone();
    }

    private IEnumerator MissileCountdown(Transform playerTransform)
    {
        Debug.Log($"[DangerZoneController] Missile launching in {missileDelay} seconds...");
        yield return new WaitForSeconds(missileDelay);

        // Only launch if the player is still in the zone (coroutine wasn't cancelled)
        Debug.Log("[DangerZoneController] Launching missile!");
        missileLauncher.Launch(playerTransform);
        activeCountdown = null;
    }
}
