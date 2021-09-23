using UnityEngine;

public class ChangeOrderInLayer : MonoBehaviour
{
    public int sortingOrderUp, sortingOrderDown;

    Transform thisTransform;
    private void Start()
    {
        thisTransform = transform;       
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {

            if (thisTransform.position.y < collision.transform.position.y)
            {
                collision.GetComponent<SpriteRenderer>().sortingOrder = sortingOrderDown;
            }
            else
            {
                collision.GetComponent<SpriteRenderer>().sortingOrder = sortingOrderUp;
            }
        }
    }
}
