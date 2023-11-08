using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public Vector2[] piece1;
    public Vector2[] piece2;
    public Vector2[] piece3;
    public Vector2[] piece4;
    public Vector2[] piece5;
    
    public Vector3[] originPoints;

    // Start is called before the first frame update
    void Start()
    {
        Shape shape1 = new Shape(piece1, originPoints[0]);
        Shape shape2 = new Shape(piece2, originPoints[1]);
        Shape shape3 = new Shape(piece3, originPoints[2]);
        Shape shape4 = new Shape(piece4, originPoints[3]);
        Shape shape5 = new Shape(piece5, originPoints[4]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
