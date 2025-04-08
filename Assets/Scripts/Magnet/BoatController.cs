using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float currentDeclination = 15f; // 磁偏角数值

    private float inputAngle = 0f; // 玩家输入角度

    void Update()
    {
        // 计算实际移动方向 = 输入角度 + 磁偏角
        Vector2 moveDir = new Vector2(
            Mathf.Sin((inputAngle + currentDeclination) * Mathf.Deg2Rad),
            Mathf.Cos((inputAngle + currentDeclination) * Mathf.Deg2Rad)
        );

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    // 供UI Slider调用
    public void OnAngleChanged(float angle)
    {
        inputAngle = angle;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            
            Debug.Log("撞到礁石！");
        }
        else if (other.CompareTag("Finish"))
        {
            Debug.Log("通关成功！");
           
        }
    }
}