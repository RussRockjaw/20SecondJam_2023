using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public GameObject prefabCell;

    private Grid2D grid;
    private GameObject[] cells;


    void Start()
    {

    }

    void Update()
    {
        
    }


    public void BuildPlayArea(int gridWidth, int gridHeight)
    {
        grid = new Grid2D(gridWidth, gridHeight, new Vector2(1, 1), new Vector2(-gridWidth, -gridHeight / 2));
        cells = new GameObject[grid.Size];
        
        for(int i = 0; i < grid.Size; i++) 
        {
            cells[i] = Instantiate(prefabCell);
            cells[i].transform.position = grid.Index2World(i);
            cells[i].transform.SetParent(this.gameObject.transform);
        }
    }

    public void ColorTheCells(Vector2[] pos, Color c)
    {
        int pI = grid.Cart2Index(pos[0]);
        Transform parent = cells[pI].transform;

        foreach(Vector2 p in pos)
        {
            int i = grid.Cart2Index(p);
            SpriteRenderer sr = cells[i].GetComponent<SpriteRenderer>();
            sr.color = c;
            if(p != pos[0])
                cells[i].transform.SetParent(parent);
        }
    }

    public int Size()
    {
        return grid.Size;
    }

    public Grid2D GetGrid()
    {
        return grid;
    }

    public Vector2 Index2Cart(int n)
    {
        return grid.Index2Cart(n);
    }

    public int Cart2Index(Vector2 p)
    {
        return grid.Cart2Index(p);
    }

}
