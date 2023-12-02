using System.Collections.Generic;
using UnityEngine;

public class StatePlay : IGameState
{
    private bool gameStarted;
    private bool gameOver;
    private Timer gameTimer;
    private Timer countdownTimer;
    private PlayArea playArea;
    private GameObject prefabGamePiece;
    private GameObject prefabPlayArea;
    private GameObject prefabCell;
    private Vector2[][] gamePieceData;
    private List<Shape> shapes;
    private GameObject heldPiece;
    private Vector3 pickupOffset;
    private Settings settings;
    private Vector2[] directions = new Vector2[] 
    {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(-1, 0),
    };

    private float zoomMin = -10.0f;
    private float zoomMax = -20.0f;
    private float zoomSense = 10.0f;
    private StateMachine sm;



    public StatePlay(StateMachine sm, Settings set, GameObject pp, GameObject ppa, GameObject cell)
    {
        this.sm = sm;
        settings = set;
        prefabGamePiece = pp;
        prefabPlayArea = ppa;
        prefabCell = cell;
        gameTimer = new Timer((float)settings.time, true);
        countdownTimer = new Timer(3, true);
    }

    public void Initialize()
    {
        playArea = GameObject.Instantiate(prefabPlayArea).GetComponent<PlayArea>();
        playArea.BuildPlayArea(settings.width, settings.height, this.prefabCell);
        gamePieceData = GenerateGamePieceData(settings.maxPieceSize);
        SpawnGamePieces(gamePieceData);
        playArea.SetTimerText(gameTimer.CurrentAsString);
        playArea.SetExtraText("");
        playArea.TurnOnCountDown();
        gameTimer.Reset();
        countdownTimer.Reset();
        gameStarted = false;
        gameOver = false;
    }

    public void Cleanup()
    {
        Object.Destroy(playArea.gameObject);
        for(int i = shapes.Count - 1; i > -1; i--) 
        {
            Object.Destroy(shapes[i].gameObject);
        }
    }

    private void Restart()
    {
        Cleanup();
        Initialize();
    }

    public void HandleUpdate()
    {
        HandleInput();

        if(!gameStarted)
        {
            CountDown();
            return;
        }

        if(gameOver)
            return;

        HandleGameTimer();

        if(heldPiece != null)
        {
            HoldGamePiece();
        }

        bool[] containedCells = playArea.CheckContainedCells(shapes);
        playArea.HighlightCells(containedCells);

        if(heldPiece == null && WeWin(containedCells))
        {
            gameOver = true;
            playArea.SetTimerText($"You finished with {gameTimer.CurrentAsString} seconds left!");
            playArea.SetExtraText("Press 'R' to restart or 'ESC' to quit");
            // TODO/incomplete: swtich to win state
        }
    }

    public void HandleGameTimer()
    {
        if(gameTimer.Tick(Time.deltaTime))
        {
            playArea.SetTimerText("Game Over");
            // TODO/incomplete: switch to game over state
        }
        else 
        {
            playArea.SetTimerText(gameTimer.CurrentAsString);
        }
    }

    private void HandleInput()
    {
        float scroll = Input.mouseScrollDelta.y;
        if(scroll != 0)
        {
            Camera.main.orthographicSize += -scroll * Time.deltaTime * zoomSense;
        }

        if(!gameStarted)
        {
            return;
        }

        if(gameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                sm.StateTitle();
            }
        }

        if(heldPiece == null && Input.GetMouseButtonDown(0))
        {
            PickupGamePiece();
        }
        else if(heldPiece != null && !Input.GetMouseButton(0))
        {
            DropGamePiece();
        }
    }

    private void CountDown()
    {
        gameStarted = countdownTimer.Tick(Time.deltaTime);
        if(gameStarted)
            playArea.TurnOffCountDown();
        playArea.SetCountDownText(countdownTimer.Current);
        // TODO/incomplete: set the ui countdown timer text
    }


    private void PickupGamePiece()
    {
        RaycastHit hit; 
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject.tag == "piece")
            {
                heldPiece = hit.collider.gameObject.transform.parent.gameObject;
                heldPiece.GetComponent<Shape>().SetInitialPos(heldPiece.transform.position);
                pickupOffset = heldPiece.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    private void DropGamePiece()
    {
        Vector3 snapToGridPos = playArea.Cart2World(playArea.World2Cart(heldPiece.transform.position));
        if(playArea.AnyCellsContained(new List<Shape>() { heldPiece.GetComponent<Shape>() }))
        {
            heldPiece.transform.position = snapToGridPos;
        }

        if(playArea.CheckForOverlappingPieces(shapes))
        {
            heldPiece.GetComponent<Shape>().ResetToInitialPos();
        }

        Vector3 temp = heldPiece.transform.position;
        temp.z = 0;
        heldPiece.transform.position = temp;

        heldPiece = null;
    }

    private void HoldGamePiece()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        pickupOffset.z = -1;
        heldPiece.transform.position = mousePos + pickupOffset;
    }


    private bool WeWin(bool[] cells)
    {
        foreach(bool b in cells)
        {
            if(!b) 
                return false;
        }
        return true;
    }


    public void SpawnGamePieces(Vector2[][] data)
    {
        shapes = new List<Shape>();

        float degSplit = 360 / data.Length;

        for(int i = 0; i < data.Length; i++)
        {
            Vector2 circlePos = KE.Math.GetPositionAroundCirlce(degSplit * i, Mathf.Max(settings.width, settings.height) * 1.25f, playArea.GetCenterWorldPosition(0));
            circlePos.x -= 1;
            circlePos.y -= 1;
            Shape s = GameObject.Instantiate(prefabGamePiece).GetComponent<Shape>();
            s.CreateMesh(data[i]);
            s.SetInitialPos(new Vector3(circlePos.x, circlePos.y, 0));
            s.ResetToInitialPos();
            shapes.Add(s);
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
        List<Vector2> results = new List<Vector2>();

        cellsClaimed[start] = true;
        results.Add(currentPiecePosition);

        for(int i = 1; i < steps; i++)
        {
            List<int> checkedDirections = new List<int>();

            while(checkedDirections.Count < 4)
            {
                // choose a random direction
                int di = Random.Range(0, 4);
                if(checkedDirections.Contains(di))
                    continue;

                Vector2 d = directions[di];
                Vector2 targetGridPos = currentGridPosition + d;
                int targetPosIndex = playArea.Cart2Index(targetGridPos);

                // check if that space is open
                if(playArea.GetGrid().Contains(targetGridPos) && !cellsClaimed[targetPosIndex])
                {
                    currentGridPosition = targetGridPos;
                    currentPiecePosition += d;
                    results.Add(currentPiecePosition);
                    cellsClaimed[targetPosIndex] = true;
                    break;
                }
                else
                {
                    checkedDirections.Add(di);
                }
            }

            if(checkedDirections.Count >= 4)
                break;
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
