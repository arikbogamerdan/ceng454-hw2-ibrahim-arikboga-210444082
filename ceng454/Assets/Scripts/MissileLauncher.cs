// MissileLauncher.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Spawns a missile prefab at the launch point and gives it a target.
//              Can also destroy the active missile when the player escapes.

using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [Header("Missile Setup")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform  launchPoint;

    [Header("Audio")]
    [SerializeField] private AudioSource launchAudioSource;

    // Track the currently active missile so we can destroy it on exit
    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        if (missilePrefab == null || launchPoint == null)
        {
            Debug.LogError("[MissileLauncher] missilePrefab or launchPoint is not assigned!");
            return null;
        }

        // 3-A: Instantiate the missile at the launch point
        activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);

        // 3-B: Give the missile its homing target
        MissileHoming homing = activeMissile.GetComponent<MissileHoming>();
        if (homing != null)
            homing.SetTarget(target);
        else
            Debug.LogWarning("[MissileLauncher] Missile prefab has no MissileHoming component!");

        // 3-C: Play launch audio
        if (launchAudioSource != null)
            launchAudioSource.Play();

        Debug.Log("[MissileLauncher] Missile launched!");
        return activeMissile;
    }

    public void DestroyActiveMissile()
    {
        // 3-D: Safely destroy the current missile if one exists
        if (activeMissile != null)
        {
            Destroy(activeMissile);
            activeMissile = null;
            Debug.Log("[MissileLauncher] Active missile destroyed (player escaped zone).");
        }
    }
}
