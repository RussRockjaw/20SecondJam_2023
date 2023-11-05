using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public Vector2[] nodePositions;

    // Start is called before the first frame update
    void Start()
    {
        Shape shape = new Shape(nodePositions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
