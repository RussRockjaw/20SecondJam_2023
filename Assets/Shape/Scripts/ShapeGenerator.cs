using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject piece;
    public Vector2[] piece1;
    public Vector2[] piece2;
    public Vector2[] piece3;
    public Vector2[] piece4;
    public Vector2[] piece5;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj1 = Instantiate(piece, new Vector3(0, 0), Quaternion.identity);
        GameObject obj2 = Instantiate(piece, new Vector3(0, 1), Quaternion.identity);
        GameObject obj3 = Instantiate(piece, new Vector3(2, 1), Quaternion.identity);
        GameObject obj4 = Instantiate(piece, new Vector3(3, 1), Quaternion.identity);
        GameObject obj5 = Instantiate(piece, new Vector3(2, 3), Quaternion.identity);
        
        obj1.transform.GetComponent<Shape>().CreateMesh(piece1);
        obj2.transform.GetComponent<Shape>().CreateMesh(piece2);
        obj3.transform.GetComponent<Shape>().CreateMesh(piece3);
        obj4.transform.GetComponent<Shape>().CreateMesh(piece4);
        obj5.transform.GetComponent<Shape>().CreateMesh(piece5);
        
        Vector2[] localPositions = obj2.GetComponent<Shape>().GetNodePositions();
        Vector2[] globalPositions = obj2.GetComponent<Shape>().LocalToWorld();
        for(int i = 0; i < localPositions.Length; i++)
        {
            Debug.Log("Local Position: " + localPositions[i] + "-> Global Position: " + globalPositions[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
