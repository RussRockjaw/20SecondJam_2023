using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject returnButtonGO;
    public Button returnButton;

    public GameObject confirmButtonGO;
    public Button confirmButton;

    public GameObject timerSliderGO;
    public Slider timerSlider;
    public GameObject labelTimerValue;
    
    public GameObject widthSliderGO;
    public Slider widthSlider;
    public GameObject labelWidthValue;
    
    public GameObject heightSliderGO;
    public Slider heightSlider;
    public GameObject labelHeightValue;

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
       //timerSlider = timerSliderGO.GetComponent<Slider>();
       //widthSlider = widthSliderGO.GetComponent<Slider>();
       //heightSlider = heightSliderGO.GetComponent<Slider>();
       //returnButton = returnButtonGO.GetComponent<Button>();
       //confirmButton = confirmButtonGO.GetComponent<Button>();
    }
}
