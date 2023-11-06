using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayArea playArea;
    public int maxPieceSize = 6;

    private List<List<Vector2>> gamePieceData;


    void Start()
    {
        playArea.BuildPlayArea(5, 5);
        gamePieceData = GenerateGamePieces(playArea.GetGrid(), 5);
    }



    public List<List<Vector2>> GenerateGamePieces(Grid2D grid, int num)
    {
        List<List<Vector2>> result = new List<List<Vector2>>();
        int cellsLeft = grid.Size;

        for(int i = 0; i < num; i++)
        {

        }

        return result;
    }
}
