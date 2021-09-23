using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePigDog : MonoBehaviour
{
    private MoveDog moveDog;
    void Start()
    {
        moveDog = GetComponent<MoveDog>();
    }



    public void SeePig(ref bool sawPig, ref Directions direction, PairOfIndexes pair, PairOfIndexes pairPig)
    {
        bool behindPig = false;

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

        //По квадрату - Благодаря носу
        if (Mathf.Abs(pairPig.i - pair.i) <= 2 && Mathf.Abs(pairPig.j - pair.j) <= 2)
        {
            sawPig = true;

            direction = AdditionalFunctions.DirectionToCell(pair, new PairOfIndexes(pair.i, pairPig.j));
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
            if(Matrix.GetFinishPosition(pair, direction) != AdditionalFunctions.NoneFinishPosition)
            {
                direction = AdditionalFunctions.DirectionToCell(pair, new PairOfIndexes(pair.j, pairPig.i));
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
        }

        if (sawPig)
        {
            
            if (behindPig)
            {
                moveDog.changeSprite.Change(moveDog.version, (Directions)(((int)direction + 2) % 4));

                moveDog.FaceToFace();
                return;
            }

            if (moveDog.version != VersionOfLivingObject.Angry)
            {
                moveDog.version = VersionOfLivingObject.Angry;
                moveDog.changeSprite.Change(moveDog.version, direction);
            }
            // moveDog.countGo = (pairPig - pair).AbsMaxIndex();

            moveDog.inAction = true;
        }
    }
}