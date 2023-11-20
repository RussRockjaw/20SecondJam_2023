using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject returnButtonGO;

    public GameObject timerSliderGO;
    private Slider timerSlider;
    public GameObject labelTimerValue;
    public int TimerValue {get;}
    
    public GameObject widthSliderGO;
    private Slider widthSlider;
    public GameObject labelWidthValue;
    private int WidthValue {get;}
    
    public GameObject heightSliderGO;
    private Slider heightSlider;
    public GameObject labelHeightValue;
    private int HeightValue {get;}

    // Start is called before the first frame update
    void Start()
    {
       
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
        labelTimerValue.GetComponent<TMP_Text>().text = "" + timerSlider.value;
    }

    public void WidthSliderChange()
    {
        labelWidthValue.GetComponent<TMP_Text>().text = "" + widthSlider.value;
    }

    public void HeightSliderChange()
    {
        labelHeightValue.GetComponent<TMP_Text>().text = "" + heightSlider.value;
    }
}
