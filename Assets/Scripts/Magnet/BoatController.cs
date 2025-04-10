using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;          
    public float acceleration = 10f;     
    public float deceleration = 15f;      
    [Range(0, 1)] public float driftFactor = 0.95f; 

    [Header("浪花效果")]
    public ParticleSystem wakeParticles;  
    public float wakeEmissionRate = 20f; 
    public float wakeSpeedThreshold = 0.1f;

    private Rigidbody rb;
    private Vector2 movementInput;
    private float currentSpeed;

    [Header("磁偏角效果")]
    [Tooltip("横向偏移强度（按W/S时的左右偏移量）")]
    public float magneticOffset = 0.3f;
    [Tooltip("偏移响应速度")]
    public float offsetSmoothness = 5f;

    private float currentOffset;
    public GameObject Panel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ConfigureParticles();
        Panel.gameObject.SetActive(false);

    }

    void Update()
    {
        GetInput();
        UpdateWakeEffect();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyDrift();
    }

    void ConfigureParticles()
    {
        if (wakeParticles != null)
        {
            var emission = wakeParticles.emission;
            emission.rateOverTime = 0f; 
        }
    }

    void GetInput()
    {
        movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }


    void ApplyMovement()
    {

        float targetOffset = 0f;
        if (movementInput.y != 0)
        {
            targetOffset = movementInput.y > 0 ? magneticOffset : -magneticOffset;
        }
        currentOffset = Mathf.Lerp(currentOffset, targetOffset, offsetSmoothness * Time.fixedDeltaTime);

  
        if (movementInput != Vector2.zero)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, acceleration * Time.fixedDeltaTime);

 
            Vector3 moveDirection = new Vector3(
                movementInput.x + currentOffset, 
                movementInput.y,
                0
            ).normalized;

            rb.velocity = moveDirection * currentSpeed;
        }

    }
    void ApplyDrift()
    {

        rb.velocity = Vector2.Lerp(rb.velocity, movementInput * currentSpeed, driftFactor);
    }

    void UpdateWakeEffect()
    {
        if (wakeParticles == null) return;


        bool isMoving = rb.velocity.magnitude > wakeSpeedThreshold;
        var emission = wakeParticles.emission;
        emission.rateOverTime = isMoving ? wakeEmissionRate : 0f;


        if (isMoving)
        {
            float angle = Mathf.Atan2(-rb.velocity.y, -rb.velocity.x) * Mathf.Rad2Deg;
            wakeParticles.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Finish"))
        {
            Panel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        if (other.collider.CompareTag("Obstacle"))
        {

        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying && rb != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rb.velocity);
        }
    }
}