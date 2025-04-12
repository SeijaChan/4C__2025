using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject LoadScreen;
    public GameObject transit;
    public Slider slider;
    public Text percent;

    public Material material;
    [Header("参数范围")]
    private float _minGray = -0.05f; 
    private float _maxGray = 1.05f;      
    private float _duration = 1.0f;


    public static UI_Manager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        UpdateMaterialGray(_minGray);

    }
    void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void LoadMainLevel()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        LoadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;

            percent.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                percent.text = "加载完毕";

                yield return new WaitForSeconds(0.3f);

                transit.SetActive(true);
                StartCoroutine(FadeGrayScale(operation));

                yield return new WaitForSeconds(2f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator FadeGrayScale(AsyncOperation operation)
    {
        float elapsedTime = 0;
        float startGray = material.GetFloat("_gray");
        float targetGray = Mathf.PingPong(Time.time, _maxGray - _minGray) + _minGray;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _duration);

            float currentGray = Mathf.Lerp(startGray, targetGray, t);
            UpdateMaterialGray(currentGray);

            if (operation.progress >= 0.9f && !transit.activeSelf)
            {
                transit.SetActive(true);
            }

            yield return null;
        }
    }

    private void UpdateMaterialGray(float value)
    {
        if (material == null) return;

        material.SetFloat("_gray", value);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

