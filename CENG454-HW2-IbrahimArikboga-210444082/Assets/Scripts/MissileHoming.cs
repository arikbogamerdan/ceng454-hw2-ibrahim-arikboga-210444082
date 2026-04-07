using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float moveSpeed  = 25f;  
    [SerializeField] private float turnSpeed  = 4f;   

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                                               turnSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
