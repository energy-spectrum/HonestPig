using UnityEngine;

public class Cell : MonoBehaviour
{
    public PairOfIndexes pair;
    public bool empty = true;

    public Vector2 position;

    public int numerVertex;
    private void Start()
    {
        position = transform.position;
    }
}
