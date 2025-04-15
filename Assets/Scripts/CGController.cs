using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 添加场景管理命名空间
using NodeCanvas.Framework;
using DG.Tweening;

public class CGConditionController : MonoBehaviour
{
    [Header("Blackboard 变量设置")]
    public string boolVar1 = "Condition1";
    public string boolVar2 = "Condition2";
    public string boolVar3 = "Condition3";

    [Header("CG Image 组件")]
    public Image cgImage1;
    public Image cgImage2;
    public Image cgImage3;

    [Header("动画设置")]
    public float fadeDuration = 1f;
    public float delayBeforeSceneChange = 1f; // isFinished为真后延迟多少秒切换场景
    public string mapSceneName = "Map"; // 目标场景名称

    private Blackboard blackboard;
    private Image currentActiveImage;
    private bool sceneChangeTriggered = false; // 防止重复触发场景切换

    void Start()
    {
        blackboard = GetComponent<Blackboard>();
        if (blackboard == null)
        {
            Debug.LogError("未找到Blackboard组件！", this);
        }

        InitializeImages();
    }

    void InitializeImages()
    {
        SetImageAlpha(cgImage1, 0f);
        SetImageAlpha(cgImage2, 0f);
        SetImageAlpha(cgImage3, 0f);

        cgImage1.gameObject.SetActive(false);
        cgImage2.gameObject.SetActive(false);
        cgImage3.gameObject.SetActive(false);
    }

    void Update()
    {
        if (blackboard == null) return;

        bool Change1 = blackboard.GetValue<bool>(boolVar1);
        bool Change2 = blackboard.GetValue<bool>(boolVar2);
        bool isFinished = blackboard.GetValue<bool>(boolVar3);

        // 确定应该显示的图片
        Image targetImage = null;

        if (Change1 && Change2 && isFinished)
        {
            targetImage = cgImage3;

            // 当isFinished为真且尚未触发场景切换时
            if (!sceneChangeTriggered)
            {
                sceneChangeTriggered = true;
                Invoke("LoadMapScene", delayBeforeSceneChange);
            }
        }
        else if (Change1 && Change2)
        {
            targetImage = cgImage2;
        }
        else if (Change1)
        {
            targetImage = cgImage1;
        }

        // 如果目标图片与当前不同，则切换
        if (targetImage != currentActiveImage)
        {
            SwitchImage(targetImage);
        }
    }

    void SwitchImage(Image targetImage)
    {
        // 淡出当前图片
        if (currentActiveImage != null)
        {
            currentActiveImage.DOFade(0f, fadeDuration)
                .OnComplete(() => currentActiveImage.gameObject.SetActive(false));
        }

        // 淡入新图片
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(true);
            targetImage.DOFade(1f, fadeDuration);
        }

        currentActiveImage = targetImage;
    }

    void LoadMapScene()
    {
        SceneManager.LoadScene(mapSceneName);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}