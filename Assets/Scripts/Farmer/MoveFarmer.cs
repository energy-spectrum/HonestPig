using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveFarmer : Move
{
    public MoveDog moveDog;

    public ChangeSprite changeSprite;

    public Transform resultPanel;
    public Text textResult;
    public Vector2 positionResult;

    [SerializeField]
    public int countGo;
    [SerializeField]
    public bool inAction = false;

    public bool killedPig = false;

    public MovePig movePig;
    private SeePigEnemy seePigEnemy;
    private NaSalo naSalo;
    private bool sawPig = false, lastSawPig = false;
    
    private PairOfIndexes lastPair = PairOfIndexes.None;

    private VersionOfLivingObject version = VersionOfLivingObject.Normal;
    public VersionOfLivingObject Version
    {
        get { return version; }
    }
    private List<int>[] adj;   
    private int[,] vertexMatrix;
    private PairOfIndexes[] vertexCoordinates;

    public float stunning = 0;
    public int countExplosion = 0;

    void ToWalk()
    {
        List<int> neighboringVertexes = adj[vertexMatrix[pair.i, pair.j]];
        
        int lastVertex = -1;
        for (int u = 0; u < neighboringVertexes.Count; u++)
        {
            if(vertexCoordinates[neighboringVertexes[u]] == lastPair)
            {
                lastVertex = u;
                break;
            }
        }

        int numerRandomVertex = Random.Range(0, neighboringVertexes.Count);
        while(numerRandomVertex == lastVertex) numerRandomVertex = Random.Range(0, neighboringVertexes.Count);

        PairOfIndexes pairCell = vertexCoordinates[neighboringVertexes[numerRandomVertex]];

        direction = AdditionalFunctions.DirectionToCell(pair, pairCell);
        changeSprite.Change(version, direction);

        finishPosition = Matrix.GetFinishPosition(pairCell, direction);    

        inAction = !AdditionalFunctions.EqualsVector2(finishPosition, AdditionalFunctions.NoneFinishPosition);

        lastPair = pair;
    }

    public void MakeAngry()
    {
        version = VersionOfLivingObject.Angry;
        changeSprite.Change(version, direction);
        speed = 1.9f;
    }
    public void Restart()
    {
        speed = startSpeed;

        pair = startPair;
        thisTransform.position = Matrix.matrixCells[pair.i, pair.j].position;

        direction = startDirection;
        version = VersionOfLivingObject.Normal;
        changeSprite.Change(VersionOfLivingObject.Normal, direction);

        inAction = false;
        stunning = 0f;
        sawPig = lastSawPig = killedPig = false;

        lastPair = PairOfIndexes.None;
        countExplosion = 0;
    }
    
    void Start()
    {
        speed = startSpeed;

        startPair = pair;
        startDirection = direction;

        positionResult = resultPanel.localPosition;
        resultPanel.localPosition = new Vector3(0, 1010, 0);

        seePigEnemy = GetComponent<SeePigEnemy>();

        naSalo = GetComponent<NaSalo>();

        thisTransform = transform;
        
        thisTransform.position = Matrix.matrixCells[pair.i, pair.j].transform.position;

        adj = Matrix.adj;
        vertexMatrix = Matrix.vertexMatrix;
        vertexCoordinates = Matrix.vertexCoordinates;
    }
   
    public bool FaceToFace()
    {
        if (((pair - movePig.pair).AbsMaxIndex() == 1)
           && (Vector2.Distance(movePig.thisTransform.position, thisTransform.position) < 1.2f))
        {
            movePig.isDead = true;
            movePig.inAction = false;

            textResult.text = "You Lose!!!!!:<(";

            naSalo.FinishHim();
            inAction = false;
            killedPig = true;

            resultPanel.localPosition = positionResult;
            return true;
        }
        return false;
    }
    void Update()
    {
        if (killedPig || countExplosion == 2) return;

        stunning -= Time.deltaTime;

        seePigEnemy.SeePig(ref sawPig, ref lastSawPig, ref direction, pair, movePig.pair);
        
        if (sawPig == false && lastSawPig == false)
        {
            if (inAction && stunning < 0f)
                inAction = Go();
            else if (stunning < 0f)
            {
                ToWalk();
            }
        }
        else 
        {
            if (inAction && stunning < 0f)
            {
                if (FaceToFace())
                {
                    return;
                }
                if (countGo != 0)
                {                  
                    switch (direction)
                    {
                        case Directions.Up:
                            finishPosition = Matrix.GetFinishPosition(pair + PairOfIndexes.up, direction);
                            break;
                        case Directions.Right:
                            finishPosition = Matrix.GetFinishPosition(pair + PairOfIndexes.right, direction);
                            break;
                        case Directions.Down:
                            finishPosition = Matrix.GetFinishPosition(pair + PairOfIndexes.down, direction);
                            break;
                        case Directions.Left:
                            finishPosition = Matrix.GetFinishPosition(pair + PairOfIndexes.left, direction);
                            break;
                        default:
                            print("BanCangeDirection");
                            break;
                    }
                    //Если фермер не в процессе хотьбы
                    if(!Go())
                        countGo--;
                }
                else if(stunning < 0)
                {
                    lastSawPig = false;
                    inAction = false;
                }
            }
        }
    }
}
