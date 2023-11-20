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

    private int TimerValue {get; set;}
    private int WidthValue {get; set;}
    private int HeightValue {get; set;}

    public Settings settings;

    public StateOptions(StateMachine s, GameObject obj)
    {
       prefabOptions = obj;
       options = prefabOptions.GetComponent<Options>();
       sm = s;
    }

    public void Initialize()
    {
       options.timerSlider.onValueChanged.AddListener(delegate {TimerSliderChange();});
       options.widthSlider.onValueChanged.AddListener(delegate {WidthSliderChange();});
       options.heightSlider.onValueChanged.AddListener(delegate {HeightSliderChange();});
       options.returnButton.onClick.AddListener(ReturnButton);
       options.confirmButton.onClick.AddListener(ConfirmButton);
       settings =new Settings(20, 3, 3);
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
        TimerValue = (int)options.timerSlider.value;
        options.labelTimerValue.GetComponent<TMP_Text>().text = "" + TimerValue;
    }

    public void WidthSliderChange()
    {
        WidthValue = (int)options.widthSlider.value;
        options.labelWidthValue.GetComponent<TMP_Text>().text = "" + WidthValue;
    }

    public void HeightSliderChange()
    {
        HeightValue = (int)options.heightSlider.value;
        options.labelHeightValue.GetComponent<TMP_Text>().text = "" + HeightValue;
    }

    public void ReturnButton()
    {
        sm.StateTitle();
    }

    public void ConfirmButton()
    {
       settings.time = TimerValue;
       settings.width = WidthValue;
       settings.height = HeightValue;
    }

}
