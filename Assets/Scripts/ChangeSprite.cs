using System.Collections.Generic;
using UnityEngine;

public enum VersionOfLivingObject
{
    Normal,
    Dirty,
    Angry
}

public class ChangeSprite : MonoBehaviour
{
    public List<Sprite> normalSprites, angrySprites, dirtySprites;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Change(VersionOfLivingObject version, Directions direction)
    {
       
        switch (version)
        {
            case VersionOfLivingObject.Normal:
                spriteRenderer.sprite = normalSprites[(int)direction];
                break;
            case VersionOfLivingObject.Angry:
                spriteRenderer.sprite = angrySprites[(int)direction];
                break;
            case VersionOfLivingObject.Dirty:
                spriteRenderer.sprite = dirtySprites[(int)direction];
                break;
            default:
                break;
        }

    }
}
