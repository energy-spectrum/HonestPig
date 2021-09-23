using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeDirectionForPig : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MovePig movePig;
    public ChangeSprite changeSprite;
    public Contusion contusion;

    public Directions direction;

    public void Change()
    {
        if (movePig.isDead) return;

        if (!movePig.inAction && contusion.stunning <= 0f)
        {
            movePig.direction = direction;
            changeSprite.Change(VersionOfLivingObject.Normal, direction);

            switch (direction)
            {
                case Directions.Up:
                    movePig.finishPosition = Matrix.GetFinishPosition(movePig.pair + PairOfIndexes.up, direction);
                    break;
                case Directions.Right:              
                    movePig.finishPosition = Matrix.GetFinishPosition(movePig.pair + PairOfIndexes.right, direction);        
                    break;
                case Directions.Down:
                    movePig.finishPosition = Matrix.GetFinishPosition(movePig.pair + PairOfIndexes.down, direction);
                    break;
                case Directions.Left:
                    movePig.finishPosition = Matrix.GetFinishPosition(movePig.pair + PairOfIndexes.left, direction);
                    break;
                default:
                    print("BanCangeDirection");
                    break;
            }

            if (movePig.finishPosition == AdditionalFunctions.NoneFinishPosition)
            {
                print("Ban move");
            }
            else
            {
                movePig.inAction = true;
            }
        }
    }

    private bool isPresed = false;
    private void Update()
    {
        if (isPresed)
        {
            Change();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPresed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPresed = false;
    }
}