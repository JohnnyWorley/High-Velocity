using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [HideInInspector]public float hideUITimer = 4f;
    public int cameraShakeMagniude = 40;

    [Header("Score")]
    public int timeBonus = 100;
    public int enemiesKilled;
    public int specialMoves;
    public int totalScore;

    [Header("End Level Screen")]

    public GameObject endLevelUI;
    public TextMeshProUGUI timeBonusText;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI specialMovesText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI levelTimerText;

    [Header("Settings")]

  
    [SerializeField] private bool autoSprint = true;

   


    [Header("Sensitivity")]
    public TextMeshProUGUI displaySens;
    public Slider sensSlider;

    public TextMeshProUGUI aimDisplaySens;
    public Slider aimSensSlider;



    [Header("UI Elements")]
    public Image crusherDeathBlack;
    public TextMeshProUGUI fovText;
    public Slider fovSlider;
    public GameObject playerHUD;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject deathScreen;
    public TextMeshProUGUI deathText;
    public Button autoSprintButton;
    public Button backButton;
    public TextMeshProUGUI pickUpText;

    [Header("Loading Screen Elements")]
    public TextMeshProUGUI progresPercentageText;
    public Image loadingBar;
    public GameObject loadingScreen;


    [Header("Booleans")]

    public bool isAlive = true;

    [Header("Game Objects")]

    public GameObject player;

    [Header("Miscellanous")]
    public Camera renderCamera;
    public static GameManager instance;




    // Start is called before the first frame update
    void Start()
    {
        ApplyPlayerSettings();
    }
    private void Awake()
    {
        instance = this;
    }
    public void ApplyPlayerSettings()
    {
        Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
        FirstPersonLook.instance.sensitivity = PlayerPrefs.GetFloat("sens");
        autoSprint = Convert.ToBoolean(PlayerPrefs.GetInt("autoSprint"));
        AutoSpringToggle();
    }

    public void RestartLevel()
    {
        player.GetComponent<SlowMotion>().SlowMotionDisable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseScript.instance.Unpause();
       
    }

/*
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
|  __      __             _____    _____              ____    _        ______         ______   _    _   _   _    _____   _______   _____    ____    _   _    _____    |
|  \ \    / /     /\     |  __ \  |_   _|     /\     |  _ \  | |      |  ____|       |  ____| | |  | | | \ | |  / ____| |__  __|  |_   _|  / __ \  | \ | |  / ____|   |
|   \ \  / /     /  \    | |__) |   | |      /  \    | |_) | | |      | |__          | |__    | |  | | |  \| | | |         | |      | |   | |  | | |  \| | | (___     |
|    \ \/ /     / /\ \   |  _  /    | |     / /\ \   |  _ <  | |      |  __|         |  __|   | |  | | | . ` | | |         | |      | |   | |  | | | . ` |  \___ \    |
|     \  /     / ____ \  | | \ \   _| |_   / ____ \  | |_) | | |____  | |____        | |      | |__| | | |\  | | |____     | |     _| |_  | |__| | | |\  |  ____) |   |
|      \/     /_/    \_\ |_|  \_\ |_____| /_/    \_\ |____/  |______| |______|       |_|       \____/  |_| \_|  \_____|    |_|    |_____|  \____/  |_| \_| |_____/    |
|                                                                                                                                                                     |
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
*/


    public void ChangeShakeMagnitude(int newMagnitude)
    {
        cameraShakeMagniude = newMagnitude;
    }

    public void ChangeKeybindText_PickUp()
    {
        pickUpText.text = PlayerPrefs.GetString("pickUpString");
    }



/*
----------------------------------------------------------------------------------------------------------     
|   _    _    _____       ______   _    _   _   _    _____   _______   _____    ____    _   _    _____   |
|  | |  | |  |_   _|     |  ____| | |  | | | \ | |  / ____| |__   __| |_   _|  / __ \  | \ | |  / ____|  |
|  | |  | |    | |       | |__    | |  | | |  \| | | |         | |      | |   | |  | | |  \| | | (___    |
|  | |  | |    | |       |  __|   | |  | | | . ` | | |         | |      | |   | |  | | | . ` |  \___ \   |
|  | |__| |   _| |_      | |      | |__| | | |\  | | |____     | |     _| |_  | |__| | | |\  |  ____) |  |
|   \____/   |_____|     |_|       \____/  |_| \_|  \_____|    |_|    |_____|  \____/  |_| \_| |_____/   |
|                                                                                                        |
----------------------------------------------------------------------------------------------------------     

*/

   /* public void SensitivityChange()
    {
        displaySens.text = sensSlider.value.ToString("F1");
        FirstPersonLook.instance.sensitivity = sensSlider.value;
        gameManagerSensitivity = sensSlider.value;

    }

    public void AimSensitivityChange()
    {
        aimDisplaySens.text = aimSensSlider.value.ToString("F1");
        aimSens = aimSensSlider.value;
    }

    public void ChangeFOV()
    {
        playerFOV = (int)fovSlider.value;
        Camera.main.fieldOfView = playerFOV;
       // renderCamera.fieldOfView = playerFOV;
        Dash.instance.playerFov = playerFOV;
        float displayText = playerFOV;
        fovText.text = displayText.ToString();

    }
   */
    public IEnumerator LevelEnd()
    {
        if (timeBonus < 100)
        {
            timeBonus = 100;
        }
        levelTimerText.text = "Level Time - " + TimerScript.instance.timerText.text;
        totalScore = enemiesKilled + timeBonus + specialMoves;
        yield return new WaitForSeconds(2);
        float lerpNumber = 0;
        float timer = 0;
        endLevelUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        while (lerpNumber < timeBonus)
        {
            timer += Time.deltaTime;
            lerpNumber = (int)Mathf.Lerp(0, timeBonus, timer / 2);
            timeBonusText.text = "Time Bonus: " + lerpNumber.ToString();
            yield return null;
        }
        timeBonusText.text = "Time Bonus: " + timeBonus.ToString();



        lerpNumber = 0;
        timer = 0;
        while (lerpNumber < enemiesKilled)
        {
            timer += Time.deltaTime;
            lerpNumber = (int)Mathf.Lerp(0, enemiesKilled, timer / 2);
            enemiesKilledText.text = "Enemies Killed Bonus: " + lerpNumber.ToString();
            yield return null;
        }


        lerpNumber = 0;
        timer = 0;
        while (lerpNumber < specialMoves)
        {
            timer += Time.deltaTime;
            lerpNumber = (int)Mathf.Lerp(0, specialMoves, timer / 2);
            specialMovesText.text = "Special Moves: " + lerpNumber.ToString();
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        lerpNumber = 0;
        timer = 0;


        while (lerpNumber < totalScore)
        {
            timer += Time.deltaTime;
            lerpNumber = (int)Mathf.Lerp(0, totalScore, timer / 2);
            totalScoreText.text = "Total Score: " + lerpNumber.ToString();
            yield return null;
        }
    }


    public void AutoSpringToggle()
    {
        if (autoSprint)
        {
            FirstPersonMovement.instance.speed = 18;
            autoSprint = false;
        }
        else if (!autoSprint)
        {
            FirstPersonMovement.instance.speed = 10;
            autoSprint = true;

        }
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        PlayerSettings.instance.LoadData();
    }

    public void CloseSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

/*
------------------------------------------------------------------------------------------------------------------------------------                                                                                                                                |
|    _____     _____  ______   _   _   ______       ______   _    _   _   _    _____   _______   _____    ____    _   _    _____   |
|   / ____|  / ____| |  ____| | \ | | |  ____|     |  ____| | |  | | | \ | |  / ____| |__  __|  |_   _|  / __ \  | \ | |  / ____|  |
|  | (___   | |      | |__    |  \| | | |__        | |__    | |  | | |  \| | | |         | |      | |   | |  | | |  \| | | (___    |
|   \___ \  | |      |  __|   | . ` | |  __|       |  __|   | |  | | | . ` | | |         | |      | |   | |  | | | . ` |  \___ \   |
|   ____) | | |____  | |____  | |\  | | |____      | |      | |__| | | |\  | | |____     | |     _| |_  | |__| | | |\  |  ____) |  |
|  |_____/   \_____| |______| |_| \_| |______|     |_|       \____/  |_| \_|  \_____|    |_|    |_____|  \____/  |_| \_| |_____/   |
|                                                                                                                                  |                                                                                                                             
|-----------------------------------------------------------------------------------------------------------------------------------                                                                                                                   
*/



    public void Death(string deathDisplayText)
    {
        if (SniperScript.instance != null)
        {
            SniperScript.instance.UnScope();
        }
        player.GetComponent<SlowMotion>().SlowMotionDisable();
        player.GetComponent<SlowMotion>().enabled = false;
        PauseScript.instance.BlurEnable();
        isAlive = false;
        EZCameraShake.CameraShaker.Instance.ShakeOnce(5, 1000000, 0.15f, 0.7f);
        deathText.text = deathDisplayText;
        deathScreen.SetActive(true);
        playerHUD.SetActive(false);
        renderCamera.gameObject.SetActive(false);

        player.GetComponent<FirstPersonMovement>().enabled = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Camera.main.GetComponent<FirstPersonLook>().enabled = false;

        Quaternion prePauseRot = Camera.main.transform.rotation;
        Camera.main.transform.parent.transform.rotation = prePauseRot;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAsync(levelName));
    }

    IEnumerator LoadLevelAsync(string levelName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;
            progresPercentageText.text = ((int)progress * 100).ToString() + "%";
            yield return null;
        }




    }

}
