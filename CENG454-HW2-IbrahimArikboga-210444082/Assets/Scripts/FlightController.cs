using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Flight Speeds")]
    [SerializeField] private float pitchSpeed = 45f; 
    [SerializeField] private float yawSpeed = 45f;   
    [SerializeField] private float rollSpeed = 45f;  
    [SerializeField] private float thrustSpeed = 20f; 

    [Header("Axis Corrections (Eksen Düzeltmeleri)")]
    [Tooltip("Uçak ters gidiyorsa bunu (0, 0, -1) yapın. Sağa/Sola gidiyorsa X eksenini deneyin.")]
    [SerializeField] private Vector3 forwardDirection = Vector3.forward;
    
    [Tooltip("Burun aşağı/yukarı hareketi ters ise bu değeri -1 yapın.")]
    [SerializeField] private float pitchMultiplier = 1f;
    
    [Tooltip("Q ve E tuşlarıyla dönme yönü ters ise bu değeri -1 yapın.")]
    [SerializeField] private float rollMultiplier = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.freezeRotation = true;
        }
    }

    void Update() 
    {
        HandleRotation();
        HandleThrust();
    }

    private void HandleRotation()
    {
 
        float pitchInput = Input.GetAxis("Vertical") * pitchMultiplier;
        transform.Rotate(Vector3.right * pitchInput * pitchSpeed * Time.deltaTime);
        float yawInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);
        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1f * rollMultiplier;
        if (Input.GetKey(KeyCode.E)) rollInput = -1f * rollMultiplier; 
        transform.Rotate(Vector3.forward * rollInput * rollSpeed * Time.deltaTime);
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(forwardDirection * thrustSpeed * Time.deltaTime);
        }
    }
}