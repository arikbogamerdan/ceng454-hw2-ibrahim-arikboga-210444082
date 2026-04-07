using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [Header("Missile Setup")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform  launchPoint;
    [Header("Audio")]
    [SerializeField] private AudioSource launchAudioSource;

    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        if (missilePrefab == null || launchPoint == null)
        {
            Debug.LogError("[MissileLauncher] missilePrefab or launchPoint is not assigned!");
            return null;
        }

        activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
        MissileHoming homing = activeMissile.GetComponent<MissileHoming>();
        if (homing != null)
            homing.SetTarget(target);
        else
            Debug.LogWarning("[MissileLauncher] Missile prefab has no MissileHoming component!");

        if (launchAudioSource != null)
            launchAudioSource.Play();

        Debug.Log("[MissileLauncher] Missile launched!");
        return activeMissile;
    }

    public void DestroyActiveMissile()
    {

        if (activeMissile != null)
        {
            Destroy(activeMissile);
            activeMissile = null;
            Debug.Log("[MissileLauncher] Active missile destroyed (player escaped zone).");
        }
    }
}
