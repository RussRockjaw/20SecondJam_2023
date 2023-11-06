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

    public Grid2D GetGrid()
    {
        return grid;
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

}
