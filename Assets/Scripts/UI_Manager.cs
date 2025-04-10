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

                yield return new WaitForSeconds(0.5f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}

