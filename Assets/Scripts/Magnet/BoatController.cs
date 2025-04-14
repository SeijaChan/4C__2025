using System.Collections.Generic;
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

    [Header("磁偏角轨迹")]
    public LineRenderer pathRenderer;
    public int maxPoints = 50;       // 最大轨迹点数
    public float pointSpacing = 0.5f; // 点间距
    public Color pathColor = new Color(0.4f, 0.6f, 1f, 0.8f); // 浅蓝色

    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    private Vector3 lastRecordedPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ConfigureParticles();
        Panel.gameObject.SetActive(false);

        if (pathRenderer != null)
        {
            pathRenderer.startWidth = 0.3f;
            pathRenderer.endWidth = 0.1f;
            pathRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            pathRenderer.material.SetColor("_TintColor", pathColor);
            pathRenderer.positionCount = 0;
        }

    }

    void Update()
    {
        GetInput();
        UpdateWakeEffect();
        UpdatePathVisualization();
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
    void UpdatePathVisualization()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            // 基于磁偏角计算预测偏移
            Vector3 predictedOffset = transform.right * currentOffset * 2f;

            // 动态添加轨迹点
            if (Vector3.Distance(transform.position, lastRecordedPosition) > pointSpacing)
            {
                pathPoints.Enqueue(transform.position + predictedOffset);
                lastRecordedPosition = transform.position;

                // 保持队列长度
                if (pathPoints.Count > maxPoints)
                    pathPoints.Dequeue();

                UpdateLineRenderer();
            }
        }
        else
        {
            ClearPath();
        }
    }

    void UpdateLineRenderer()
    {
        pathRenderer.positionCount = pathPoints.Count;
        pathRenderer.SetPositions(pathPoints.ToArray());

        // 添加动态渐隐效果
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new(pathColor, 0), new(pathColor, 1) },
            new GradientAlphaKey[] { new(0.8f, 0), new(0.2f, 1) }
        );
        pathRenderer.colorGradient = gradient;
    }

    void ClearPath()
    {
        pathPoints.Clear();
        pathRenderer.positionCount = 0;
    }
}