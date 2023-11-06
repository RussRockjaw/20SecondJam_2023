using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO/feature: currently this class only really works assuming its always using the x and y axis,
//               maybe we should fix that?
//
// TODO/feature: currently returning a 'world position' assumes 3rd axis is 0, maybe we should let the user provide
//               a value they would like the extra axis to be set to


public class Grid2D
{

    private Vector2 cellScale;
    // TODO/feature: maybe this could be Vector3 so we can offset the grid on a certain Z axis
    private Vector2 offsetFromZero; 
    private bool useZ = false;

    private int cols;
    private int rows;

    public int Cols { get { return cols; } private set {} }
    public int Rows { get { return rows; } private set {} }
    public int Size { get { return cols * rows; } private set {} }
    public Vector2 CellScale { get { return cellScale; } private set {} }


    // Constructor
    public Grid2D(int cols, int rows, Vector2 cellScale, Vector2 offsetFromZero, bool zFlag = false)
    {
        this.rows = rows == 0 ? 1 : rows;
        this.cols = cols == 0 ? 1 : cols;
        this.offsetFromZero = offsetFromZero;
        this.cellScale = cellScale;
        useZ = zFlag;
    }

    // Constructor
    public Grid2D(int cols, int rows, bool zFlag = false)
    {
        this.rows = rows == 0 ? 1 : rows;
        this.cols = cols == 0 ? 1 : cols;
        this.offsetFromZero = Vector3.zero;
        this.cellScale = new Vector3(1, 1, 1);
        useZ = zFlag;
    }

    // converts cartesian coords to the corresponding index
    public int Cart2Index(int x, int y)
    {
        return (y * cols) + x;
    }

    static public int Cart2Index(int x, int y, int cols)
    {
        return (y * cols) + x;
    }

    // converts cartesian coords to the corresponding index
    public int Cart2Index(Vector2 pos)
    {
        return ((int)pos.y * cols) + (int)pos.x;
    }

    // convert grid coordinates to that location in world space
    public Vector3 Cart2World(Vector2 gridPos)
    {
        Vector3 naturalOffset = new Vector3(cellScale.x / 2, cellScale.y / 2, 0);
        float x = (gridPos.x * cellScale.x) + naturalOffset.x + offsetFromZero.x;
        float y = (gridPos.y * cellScale.y) + naturalOffset.y + offsetFromZero.y;

        if(useZ) return new Vector3(x, 0f, y);
        else return new Vector3(x, y, 0f);
    }

    // convert grid coordinates to that location in world space
    public Vector3 Cart2World(int x, int y)
    {
        return Cart2World(new Vector2(x, y));
    }

    // convert an index to its cartesian coords
    public Vector2 Index2Cart(int index)
    {
        return new Vector2(Mathf.FloorToInt(index % cols), Mathf.FloorToInt(index / cols));
    }

    // convert the index of a 1D array of a grid, into a world position
    public Vector3 Index2World(int index)
    {
        return Cart2World(Index2Cart(index));
    }

    // convert a world position to a grid position
    public Vector2 World2Cart(Vector3 worldPos)
    {
        int x = (int)Mathf.Floor((worldPos.x / cellScale.x) - offsetFromZero.x);
        int y = (int)Mathf.Floor((worldPos.y / cellScale.y) - offsetFromZero.y);
        return new Vector2(x, y);
    }

    // convert a world position to an index 
    public int World2Index(Vector3 pos)
    {
        return Cart2Index(World2Cart(pos));
    }

    // checks if the provided Vector2 is contained withing the grid
    public bool Contains(Vector2 gridpos)
    {
       return (gridpos.x >= 0 && gridpos.x < cols) && (gridpos.y >= 0 && gridpos.y < rows);
    }

    // checks if the provided x and y coords are contained withing the grid
    public bool Contains(int x, int y)
    {
       return (x >= 0 && x < cols) && (y >= 0 && y < rows);
    }

    // checks if the provided index is contained withing the grid
    public bool Contains(int index)
    {
       return (index < Size) && (index >= 0);
    }

    // checks if the provided Vector3 is contained withing the grid
    public bool Contains(Vector3 pos)
    {
       Vector2 gridpos = World2Cart(pos);
       return (gridpos.x >= 0 && gridpos.x < cols) && (gridpos.y >= 0 && gridpos.y < rows);
    }
}
