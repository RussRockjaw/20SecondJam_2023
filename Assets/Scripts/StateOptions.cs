using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateOptions : IGameState
{
    private GameObject prefabOptions;
    private Options options;
    private StateMachine sm;

    private Settings defaultSettings;


    public StateOptions(StateMachine s, GameObject obj, Settings set)
    {
        prefabOptions = obj;
        defaultSettings = set;
        sm = s;
    }

    public void Initialize()
    {
        options = GameObject.Instantiate(prefabOptions).GetComponent<Options>();
        options.timerSlider.value = defaultSettings.time;
        options.widthSlider.value = defaultSettings.width;
        options.heightSlider.value = defaultSettings.height;

        options.timerSlider.onValueChanged.AddListener(delegate {TimerSliderChange();});
        options.widthSlider.onValueChanged.AddListener(delegate {WidthSliderChange();});
        options.heightSlider.onValueChanged.AddListener(delegate {HeightSliderChange();});

        options.returnButton.onClick.AddListener(ReturnButton);
        options.confirmButton.onClick.AddListener(ConfirmButton);

        TimerSliderChange();
        WidthSliderChange();
        HeightSliderChange();

    }

    public void Cleanup()
    {
        GameObject.Destroy(options.gameObject);
    }

    public void HandleUpdate()
    {

    }

    public void TimerSliderChange()
    {
        options.labelTimerValue.GetComponent<TMP_Text>().text = $"{(int)options.timerSlider.value}";
    }

    public void WidthSliderChange()
    {
        options.labelWidthValue.GetComponent<TMP_Text>().text = $"{(int)options.widthSlider.value}";
    }

    public void HeightSliderChange()
    {
        options.labelHeightValue.GetComponent<TMP_Text>().text = $"{(int)options.heightSlider.value}";
    }

    public void ReturnButton()
    {
        sm.StateTitle();
    }

    public void ConfirmButton()
    {
        sm.SetGameSettings(new Settings((int)options.timerSlider.value, (int)options.widthSlider.value, (int)options.heightSlider.value, 5));
        sm.StateTitle();
    }
}
