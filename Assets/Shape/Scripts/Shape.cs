using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    private int nodeCount;
    private Vector2[] nodePositions;
    
    
    public Shape(Vector2[] nodePositions)
    {
        this.nodePositions = nodePositions;
        CreateMesh();
    }

    private void CreateMesh()
    {
        Vector3 position = new Vector3(0, 0, 0);
        GameObject obj = new GameObject();
        MeshFilter meshFilter = obj.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = obj.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
    }
}
