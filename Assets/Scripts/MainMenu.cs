using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject buttonStartGO;
    public GameObject buttonOptionsGO;

    public Button startButton;
    public Button optionsButton;


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
        startButton = buttonStartGO.GetComponent<Button>();
        optionsButton = buttonOptionsGO.GetComponent<Button>();
    }
}
