using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Heure")]
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;

    [Header("Paramètre Heure")]
    [SerializeField] private float timePerMinute;

    [Header("Skybox")]
    [SerializeField] private Material morningSkybox;
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material eveningSkybox;
    [SerializeField] private Material nightSkybox;

    [Header("Canva")]
    [SerializeField] private Text hourText;

    public static event Action OnDayStarted;
    public static event Action OnNightStarted;

    private bool isDayTime;

    public static TimeManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
        StartCoroutine(PassTime());
        AddTime(0,6,0);
        isDayTime = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RenderSettings.skybox = daySkybox;
        }
    }

    private IEnumerator PassTime()
    {
        yield return new WaitForSeconds(timePerMinute);
        AddMinute(1);
        StartCoroutine(PassTime());
    }

    private void AddMinute(int minute)
    {
        this.minute=(this.minute+minute)%60;
        if (this.minute < minute)
        {
            AddHour(1);
        }
        refreshTimeText();
        CheckDayTime();
    }

    private void AddHour(int hour)
    {
        this.hour=(this.hour+hour)%24;
        if (this.hour < hour) {
            AddDay(1);
        }

        refreshTimeText();
        CheckDayTime();
        
        changeSkybox();

    }

    private void AddDay(int day)
    {
        this.day += day;
        refreshTimeText();
    }

    private void AddTime(int day, int hour, int minute)
    {
        AddMinute(minute);
        AddHour(hour);
        AddDay(day);
    }

    private void refreshTimeText()
    {
        hourText.text = this.hour.ToString("D2") + ":" + this.minute.ToString("D2");
    }

    private void CheckDayTime()
    {
        if (hour >= 6 && hour < 22)
        {
            if (!isDayTime)
            {
                isDayTime = true;
                OnDayStarted?.Invoke();
                Debug.Log("Day started");
            }
        }
        else
        {
            if (isDayTime)
            {
                isDayTime = false;
                OnNightStarted?.Invoke();
                Debug.Log("Night started");
            }
        }
    }

    private void changeSkybox()
    {
        switch (hour)
        {
            case 6:
                RenderSettings.skybox = morningSkybox;
                break;
            case 10:
                RenderSettings.skybox = daySkybox;
                break;
            case 18:
                RenderSettings.skybox = eveningSkybox;
                break;
            case 22:
                RenderSettings.skybox = nightSkybox;
                break;
        }
    }
}
