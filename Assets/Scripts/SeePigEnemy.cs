using UnityEngine;

public class SeePigEnemy : MonoBehaviour
{
    public bool isFarmer;
    private MoveFarmer moveFarmer;
    private MoveDog moveDog;
    void Start()
    {
        if (isFarmer)
            moveFarmer = GetComponent<MoveFarmer>();
        else
            moveDog = GetComponent<MoveDog>();
    }
    public void SeePig(ref bool sawPig, ref bool lastSawPig, ref Directions direction, PairOfIndexes pair, PairOfIndexes pairPig)
    {
        bool behindPig = false;
        if (sawPig)
        {
            lastSawPig = sawPig;
        }
        switch (direction)
        {
            case Directions.Up:
                sawPig = (pair.j == pairPig.j && pair.j % 2 == 0 && pair.i + 1 >= pairPig.i)
                       || (pair.i % 2 == 0 && pair.i == pairPig.i);

                behindPig = pair.j == pairPig.j && pair.i + 1 == pairPig.i;
                break;
            case Directions.Right:
                sawPig = (pair.i == pairPig.i && pair.i % 2 == 0 && pair.j - 1 <= pairPig.j)
                       || (pair.j % 2 == 0 && pair.j == pairPig.j);

                behindPig = pair.i == pairPig.i && pair.j - 1 == pairPig.j;
                break;
            case Directions.Down:
                sawPig = (pair.j == pairPig.j && pair.j % 2 == 0 && pair.i - 1 <= pairPig.i)
                       || (pair.i % 2 == 0 && pair.i == pairPig.i);

                behindPig = pair.j == pairPig.j && pair.i - 1 == pairPig.i;

                break;
            case Directions.Left:
                sawPig = (pair.i == pairPig.i && pair.i % 2 == 0 && pair.j + 1 >= pairPig.j)
                       || (pair.j % 2 == 0 && pair.j == pairPig.j);

                behindPig = pair.i == pairPig.i && pair.j + 1 == pairPig.j;
                break;
            default:
                break;
        }
        if (sawPig)
        {
            if (behindPig)
            {
                if (isFarmer)
                {
                    moveFarmer.changeSprite.Change(moveFarmer.Version, (Directions)(((int)direction + 2) % 4));

                    moveFarmer.FaceToFace();
                }
                else
                {
                    moveDog.changeSprite.Change(moveFarmer.Version, (Directions)(((int)direction + 2) % 4));

                    moveDog.FaceToFace();
                }
                return;
            }

            direction = AdditionalFunctions.DirectionToCell(pair, pairPig);

            if (isFarmer)
            {
                moveFarmer.countGo = (pairPig - pair).AbsMaxIndex();

                moveFarmer.changeSprite.Change(moveFarmer.Version, direction);

                moveFarmer.inAction = true;
            }
            else
            {
                moveDog.countGo = (pairPig - pair).AbsMaxIndex();

                moveDog.version = VersionOfLivingObject.Angry;
                moveDog.changeSprite.Change(moveDog.version, direction);

                moveDog.inAction = true;
            }
        }
    }
}
