// MissileHoming.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Makes the missile rotate toward the player and fly forward.
//              Speed and turn rate are tuned so the threat is visible but escapable.

using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float moveSpeed  = 25f;  // Forward speed (units/second)
    [SerializeField] private float turnSpeed  = 4f;   // Rotation speed toward target

    // 3-E: The aircraft transform cached here
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null) return;

        // 3-F: Rotate toward the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                                               turnSpeed * Time.deltaTime);

        // Move forward (in the direction we are now facing)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
