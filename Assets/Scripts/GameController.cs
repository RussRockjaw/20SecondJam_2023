using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject prefabGamePiece;
    public PlayArea playArea;

    private Vector2[][] gamePieceData;
    private List<Shape> shapes;

    private GameObject heldPiece;
    private Vector2 pickupOffset;


    public List<Color> gamePieceColors = new List<Color>() 
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
    };

    private Vector2[] directions = new Vector2[] 
    {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(-1, 0),
    };

    void Start()
    {
        playArea.BuildPlayArea(5, 5);
        gamePieceData = GenerateGamePieceData(6);
        SpawnGamePieces(gamePieceData);
    }

    void Update()
    {
        if(heldPiece == null && Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
        else if(heldPiece != null && !Input.GetMouseButton(0))
        {
            heldPiece = null;
            // TODO/incomplete: here we need to also check the playArea grid and stuff
        }

        if(heldPiece != null)
        {
            HandleHolding();
        }
    }

    public void SpawnGamePieces(Vector2[][] data)
    {
        // TODO/feature: probably will want a way to actually delete the game objects
        // clear the old list, if their is one
        shapes = new List<Shape>();

        float degSplit = 360 / data.Length;

        for(int i = 0; i < data.Length; i++)
        {
            Vector2 circlePos = KE.Math.GetPositionAroundCirlce(degSplit * i, 5.0f);
            GameObject g = Instantiate(prefabGamePiece);
            Shape s = g.GetComponent<Shape>();
            s.CreateMesh(data[i]);
            //g.transform.GetChild(0).gameObject.AddComponent<PolygonCollider2D>();
            s.transform.position = new Vector3(circlePos.x, circlePos.y, 0);
        }
    }


    public Vector2[][] GenerateGamePieceData(int maxPieceSize)
    {
        bool[] cellsClaimed = new bool[playArea.Size()];
        List<Vector2[]> result = new List<Vector2[]>();

        int count = playArea.Size();
        int currSize = maxPieceSize;
        int i = 0;

        while(count > 0) 
        {
            int openCellIndex = 0;
            for(int b = 0; b < cellsClaimed.Length; b++)
            {
                if(!cellsClaimed[b])
                {
                    openCellIndex = b;
                    break;
                }
            }

            int steps;
            if(count < currSize)
            {
                steps = count;
            }
            else
            {
                steps = currSize;
            }

            Vector2[] walk = RandomWalk(openCellIndex, steps, cellsClaimed);
            result.Add(walk);
            count -= walk.Length;
            currSize--;
            if(currSize < 1)
                currSize = maxPieceSize;
            i++;
        }

        return result.ToArray();
    }


    private Vector2[] RandomWalk(int start, int steps, bool[] cellsClaimed)
    {
        Vector2 currentGridPosition = playArea.Index2Cart(start);
        Vector2 currentPiecePosition = new Vector2(0, 0);
        List<Vector2> tracker = new List<Vector2>();
        List<Vector2> results = new List<Vector2>();

        cellsClaimed[start] = true;
        tracker.Add(currentGridPosition);
        results.Add(currentPiecePosition);

        Debug.Log("Generating New Piece");
        Debug.Log($"starting at {currentGridPosition}");

        for(int i = 1; i < steps; i++)
        {
            Debug.Log($"taking step {i}");
            for(int j = 0; j < 4; j++)
            {
                // choose a random direction
                Vector2 d = directions[Random.Range(0, 4)];
                Vector2 targetGridPos = currentGridPosition + d;
                int targetPosIndex = playArea.Cart2Index(targetGridPos);
                Debug.Log($"target pos: {targetGridPos}");
                Debug.Log($"target index: {targetPosIndex}");

                // check if that space is open
                if(playArea.GetGrid().Contains(targetGridPos) && !cellsClaimed[targetPosIndex])
                {
                    Debug.Log($"Open spot found at {targetGridPos}");

                    currentGridPosition = targetGridPos;
                    currentPiecePosition += d;
                    tracker.Add(currentGridPosition);
                    results.Add(currentPiecePosition);

                    cellsClaimed[targetPosIndex] = true;
                    break;
                }
            }
        }

        return results.ToArray();
    }

    private void HandleLeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "piece")
            {
                heldPiece = hit.collider.gameObject;
            }
        }
    }


    private void HandleHolding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        heldPiece.transform.position = mousePos;
    }



    // TODO/enhance: this will need to do a better job of distributing the
    //               cells to be a bit more random and each piece isnt
    //               the same size
    //
    // splits a number(n) into a number of parts(c) whose sum equal n
    private int[] SplitCells(int n, int max)
    {
        List<int> result = new List<int>();
        int count = n;
        int i = 0;
        int currSize = max;

        while(count > 0) 
        {
            if(count < currSize)
            {
                result.Add(count);
                count = 0;
            }
            else
            {
                result.Add(currSize);
                count -= currSize;
                i++;
                currSize--;
                if(currSize == 0)
                    currSize = max;
            }
        }
        return result.ToArray();
    }
}
