using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{

    public TextMeshProUGUI progresPercentageText;
    public Image loadingBar;
    public GameObject loadingScreen;


    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAsync(levelName));
    }

    IEnumerator LoadLevelAsync(string levelName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;
            Debug.Log((operation.progress * 100));
            progresPercentageText.text = ((operation.progress / 0.9f) * 100).ToString("F0") + "%";
            yield return null;
        }
    }
}