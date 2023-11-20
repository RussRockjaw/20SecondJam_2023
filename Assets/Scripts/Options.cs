using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Settings settings;
    
    public GameObject returnButtonGO;

    public GameObject timerSliderGO;
    private Slider timerSlider;
    public GameObject labelTimerValue;
    public int TimerValue {get; set;}
    
    public GameObject widthSliderGO;
    private Slider widthSlider;
    public GameObject labelWidthValue;
    private int WidthValue {get; set;}
    
    public GameObject heightSliderGO;
    private Slider heightSlider;
    public GameObject labelHeightValue;
    private int HeightValue {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        settings = new Settings(20, 3, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup()
    {
       timerSlider = timerSliderGO.GetComponent<Slider>();
       timerSlider.onValueChanged.AddListener(delegate {TimerSliderChange();});
       widthSlider = widthSliderGO.GetComponent<Slider>();
       widthSlider.onValueChanged.AddListener(delegate {WidthSliderChange();});
       heightSlider = heightSliderGO.GetComponent<Slider>();
       heightSlider.onValueChanged.AddListener(delegate {HeightSliderChange();});
    }

    public void TimerSliderChange()
    {
        TimerValue = (int)timerSlider.value;
        labelTimerValue.GetComponent<TMP_Text>().text = "" + TimerValue;
    }

    public void WidthSliderChange()
    {
        WidthValue = (int)widthSlider.value;
        labelWidthValue.GetComponent<TMP_Text>().text = "" + WidthValue;
    }

    public void HeightSliderChange()
    {
        HeightValue = (int)heightSlider.value;
        labelHeightValue.GetComponent<TMP_Text>().text = "" + HeightValue;
    }
}
