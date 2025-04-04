using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    public Slider angleSlider;
    public RectTransform needle;

    void Update()
    {
       
        needle.rotation = Quaternion.Euler(0, 0, -angleSlider.value);
    }
}
