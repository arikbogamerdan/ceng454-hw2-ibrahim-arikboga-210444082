// CameraFollow.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Ibrahim Arikboga | Student ID: 210444082
// Description: Smooth follow camera that stays behind and above the aircraft.

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target; // drag your aircraft here

    [Header("Position Settings")]
    [SerializeField] private float distance = 15f;  // how far behind the aircraft
    [SerializeField] private float height   = 5f;   // how high above the aircraft
    [SerializeField] private float smoothSpeed = 5f; // how smoothly camera follows

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSmooth = 5f; // how smoothly camera rotates

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position: behind and above the aircraft
        Vector3 desiredPosition = target.position
                                - target.forward * distance
                                + target.up * height;

        // Smoothly move camera to desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition,
                                          smoothSpeed * Time.deltaTime);

        // Smoothly rotate camera to look at the aircraft
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation,
                                               rotationSmooth * Time.deltaTime);
    }
}
