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
        ClearCells();
    }


    public void BuildPlayArea(int gridWidth, int gridHeight)
    {
        grid = new Grid2D(gridWidth, gridHeight, new Vector2(1, 1), new Vector2(-gridWidth / 2, -gridHeight / 2));
        cells = new GameObject[grid.Size];
        
        for(int i = 0; i < grid.Size; i++) 
        {
            cells[i] = Instantiate(prefabCell);
            cells[i].transform.position = grid.Index2World(i);
            cells[i].transform.SetParent(this.gameObject.transform);
        }
    }

    private void ClearCells()
    {
        foreach(GameObject g in cells)
        {
            SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }

    }

    public void ColorTheCells(bool[] bs)
    {
        for(int i = 0; i < bs.Length; i++)
        {
            if(bs[i])
            {
                SpriteRenderer sr = cells[i].GetComponent<SpriteRenderer>();
                sr.color = Color.red;
            }
            else
            {
                SpriteRenderer sr = cells[i].GetComponent<SpriteRenderer>();
                sr.color = Color.white;
            }
        }
    }

    public bool[] CheckCells(List<Shape> shapes)
    {
        bool[] result = new bool[grid.Size];
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = false;
        }

        foreach(Shape s in shapes)
        {
            foreach(Vector2 p in s.LocalToWorld())
            {
                if(grid.Contains(grid.World2Cart(p)))
                {
                    int i = grid.World2Index(p);
                    result[i] = true;
                }
            }
        }

        return result;
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

    public Vector3 Cart2World(Vector2 v)
    {
        return grid.Cart2World(v);
    }

    public Vector2 World2Cart(Vector3 v)
    {
        return grid.World2Cart(v);
    }

}
