using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayArea playArea;
    private Vector2[][] gamePieceData;

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
        gamePieceData = GenerateGamePieces(6);

        for(int i = 0; i < gamePieceData.Length; i++)
        {
            playArea.ColorTheCells(gamePieceData[i], Random.ColorHSV());
        }
    }



    public Vector2[][] GenerateGamePieces(int maxPieceSize)
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
        Vector2 currPos = playArea.Index2Cart(start);
        cellsClaimed[start] = true;
        List<Vector2> results = new List<Vector2>();
        results.Add(currPos);
        Debug.Log("Generating New Piece");
        Debug.Log($"starting at {currPos}");

        for(int i = 1; i < steps; i++)
        {
            Debug.Log($"taking step {i}");
            for(int j = 0; j < 4; j++)
            {
                // choose a random direction
                Vector2 targetGridPos = currPos + directions[Random.Range(0, 4)];
                int targetPosIndex = playArea.Cart2Index(targetGridPos);
                Debug.Log($"target pos: {targetGridPos}");
                Debug.Log($"target index: {targetPosIndex}");

                // check if that space is open
                if(playArea.GetGrid().Contains(targetGridPos) && !cellsClaimed[targetPosIndex])
                {
                    Debug.Log($"Open spot found at {targetGridPos}");
                    // if it is available, claim it and add it to results
                    results.Add(targetGridPos);
                    currPos = targetGridPos;
                    cellsClaimed[targetPosIndex] = true;
                    break;
                }
            }
        }

        return results.ToArray();
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
