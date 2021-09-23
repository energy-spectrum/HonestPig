using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDog : Move
{
    public ChangeSprite changeSprite;

    public MovePig movePig;

    public MoveFarmer moveFarmer;

    public Transform resultPanel;
    public Text textResult;
    public Vector2 positionResult;

    [SerializeField]
    public VersionOfLivingObject version = VersionOfLivingObject.Normal;
 
    [SerializeField]
    public int countExplosion = 0;

    public bool inAction;

    private SeePigEnemy seePigEnemy;

    private bool sawPig = false, killedPig = false;

    private PairOfIndexes lastPair = PairOfIndexes.None;

    private List<int>[] adj;
    private int[,] vertexMatrix;
    private PairOfIndexes[] vertexCoordinates;

    void Start()
    {
        speed = startSpeed;

        positionResult = resultPanel.localPosition;

        direction = Directions.Down;

        startPair = pair;
        startDirection = direction;

        thisTransform = transform;

        thisTransform.position = Matrix.matrixCells[pair.i, pair.j].transform.position;

        adj = Matrix.adj;
        vertexMatrix = Matrix.vertexMatrix;
        vertexCoordinates = Matrix.vertexCoordinates;

        seePigEnemy = GetComponent<SeePigEnemy>();
    }

    public void Restart()
    {
        countGo = 0;
        speed = startSpeed;

        pair = startPair;
        thisTransform.position = Matrix.matrixCells[pair.i, pair.j].position;

        lastSawPig = false;

        //lastChangeDirection = false;
        direction = startDirection;
        version = VersionOfLivingObject.Normal;
        changeSprite.Change(version, direction);

        inAction = false;
        sawPig = killedPig = false;

        lastPair = PairOfIndexes.None;
        countExplosion = 0;
    }

    public bool FaceToFace()
    {
        if (((pair - movePig.pair).AbsMaxIndex() == 1)
           && (Vector2.Distance(movePig.thisTransform.position, thisTransform.position) < 1.2f))
        {
            movePig.isDead = true;
            movePig.inAction = false;

            moveFarmer.killedPig = true;
            moveFarmer.inAction = false;

            textResult.text = "You Lose!!!!!:<(";

            killedPig = true;
            inAction = false;

            resultPanel.localPosition = positionResult;
            return true;
        }
        return false;
    }

    void ToWalk()
    {
        List<int> neighboringVertexes = adj[vertexMatrix[pair.i, pair.j]];

        int lastVertex = -1;
        for (int u = 0; u < neighboringVertexes.Count; u++)
        {
            if (vertexCoordinates[neighboringVertexes[u]] == lastPair)
            {
                lastVertex = u;
                break;
            }
        }

        int numerRandomVertex = Random.Range(0, neighboringVertexes.Count);
        while (numerRandomVertex == lastVertex) numerRandomVertex = Random.Range(0, neighboringVertexes.Count);

        PairOfIndexes pairCell = vertexCoordinates[neighboringVertexes[numerRandomVertex]];

        direction = AdditionalFunctions.DirectionToCell(pair, pairCell);
        changeSprite.Change(version, direction);

        finishPosition = Matrix.GetFinishPosition(pairCell, direction);

        inAction = !AdditionalFunctions.EqualsVector2(finishPosition, AdditionalFunctions.NoneFinishPosition);

        lastPair = pair;
    }

    private bool lastSawPig;
    public int countGo;
    void Update()
    {
        if (killedPig || countExplosion == 1) return;

        seePigEnemy.SeePig(ref sawPig, ref lastSawPig, ref direction, pair, movePig.pair);
       
        if (sawPig == false && lastSawPig == false)
        {
            if (inAction)
                inAction = Go();
            else
            {
                ToWalk();
            }
        }
        else
        {
            speed = startSpeed * 1.8f;
            if (inAction)
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
                    if (!Go())
                        countGo--;
                }
                else 
                {
                    lastSawPig = false;
                    inAction = false;
                }
            }
        }
    }
}
