using UnityEngine;
public class MovePig : Move
{
    public Cell startCell;
    [SerializeField]
    public bool isDead = false;
    [SerializeField]
    public bool inAction;

    private ChangeSprite changeSprite;
    private void Start()
    {
        speed = startSpeed;

        direction = Directions.Right;
        startDirection = direction;

        pair = startCell.pair;
        startPair = pair;

        thisTransform = transform;
        changeSprite = GetComponent<ChangeSprite>();
    }

    public void Restart()
    {
        pair = startPair;
        direction = startDirection;
        changeSprite.Change(VersionOfLivingObject.Normal, direction);

        thisTransform.position = Matrix.matrixCells[pair.i, pair.j].position;

        isDead = inAction = false;
    }
    
    void Update()
    {
        if (inAction)
        {
            inAction = Go();
        }
    }
}