using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    bool reachedZero = false;
    bool slowMo = false;
    [SerializeField]
    float slowMoAmount = 1.25f;
    float cooldown = 0.75f;
    float cooldownTimer = 0f;

    public Image slowMoBar;
    public RawImage emptyBar;
    void Update()
    {
        KeyCode abilityKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abilityKeybind"));
        slowMoBar.fillAmount = slowMoAmount / 1.25f;
        slowMoAmount = Mathf.Clamp(slowMoAmount, 0, 1.25f);


        if (Input.GetKeyDown(abilityKeycode))
        {
            SlowMotionToggle();
        }


        if (slowMo)
        {
            slowMoAmount -= 1 * Time.deltaTime;
            if (slowMoAmount <= 0)
            {
                reachedZero = true;
                SlowMotionDisable();
            }
        }
        else
        {
            if (cooldownTimer <= 0f)
            {
                slowMoAmount += 1.25f * Time.deltaTime;
                if (slowMoAmount >= 0.625f)
                {
                    reachedZero = false;
                }
                if (slowMoAmount >= 1.25f)
                {
                    emptyBar.gameObject.SetActive(false);
                }
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
    }

    void SlowMotionEnable()
    {
        emptyBar.gameObject.SetActive(true);
        slowMo = true;
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void SlowMotionDisable()
    {
        slowMo = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        cooldownTimer = cooldown;
    }
    private void SlowMotionToggle()
    {

        if (!slowMo && slowMoAmount >= 0 && !reachedZero)
        {
            SlowMotionEnable();
        }
        else if (slowMo)
        {
            SlowMotionDisable();
        }

    }

}
