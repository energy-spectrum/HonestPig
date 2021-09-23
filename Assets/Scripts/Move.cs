using UnityEngine;

public class Move : MonoBehaviour
{
    public float startSpeed = 1f;
    protected float speed;

    public Transform thisTransform;

    public Directions direction;
    public Vector2 finishPosition;
    public PairOfIndexes pair;


    protected PairOfIndexes startPair;
    protected Directions startDirection;

    protected bool Go()
    {
        bool isFinish = false;

        if (AdditionalFunctions.EqualsVector2(thisTransform.position, finishPosition))
        {
            isFinish = true;
            switch (direction)
            {
                case Directions.Up:
                    pair += PairOfIndexes.up;
                    break;
                case Directions.Right:
                    pair += PairOfIndexes.right;
                    break;
                case Directions.Down:
                    pair += PairOfIndexes.down;
                    break;
                case Directions.Left:
                    pair += PairOfIndexes.left;
                    break;
                default:
                    break;
            }
        }
        if (isFinish)
        {
            thisTransform.position = finishPosition;
            return false;
        }

        thisTransform.position = Vector2.MoveTowards(thisTransform.position, finishPosition, Time.deltaTime * speed);

        return true;
    }
}