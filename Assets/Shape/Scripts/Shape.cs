using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private int nodeCount;
    private Vector2[] nodePositions;
    
    
    public Shape(Vector2 nodePositions)
    {
        this.nodePositions = nodePositions;
    }

    private CreatMesh()
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[0];
        int[] triangles = new int[0];
    }
}
