using UnityEngine;
class AdditionalFunctions 
{
    static public Vector2 NoneFinishPosition
    {
        get { return new Vector2(6666f, 6666f); }
    }

    static public bool EqualsVector2(Vector2 a, Vector2 b)
    {
        float e = 0.001f;

        return Mathf.Abs(a.x - b.x) <= e && Mathf.Abs(a.y - b.y) <= e;
    }

    static public int Choose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    static public Directions DirectionToCell(PairOfIndexes pairObject, PairOfIndexes pairCell)
    {
        PairOfIndexes distation = pairCell - pairObject;
        distation /= distation.AbsMaxIndex();

        if (distation == PairOfIndexes.up)        
            return Directions.Up;

        if (distation == PairOfIndexes.right)
            return Directions.Right;

        if (distation == PairOfIndexes.down)
            return Directions.Down;

        if (distation == PairOfIndexes.left)
            return Directions.Left;

        //Nope!
        return Directions.Down;
    }
}