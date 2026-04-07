using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed = 45f; 
    [SerializeField] private float yawSpeed = 45f;   
    [SerializeField] private float rollSpeed = 45f;  
    [SerializeField] private float thrustSpeed = 20f; 

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
        float pitchInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right * pitchInput * pitchSpeed * Time.deltaTime);
        float yawInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);
        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
        if (Input.GetKey(KeyCode.E)) rollInput = -1f; 
        transform.Rotate(Vector3.forward * rollInput * rollSpeed * Time.deltaTime);
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime);
        }
    }
}