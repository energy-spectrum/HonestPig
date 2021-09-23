using System.Collections;
using UnityEngine;

public class Contusion : MonoBehaviour
{
    public float stunning = 0f;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(stunning > 0)
        {
            stunning -= Time.deltaTime;

            spriteRenderer.color = new Color(1, 1, 1, (Mathf.Sin(stunning * 360 / Mathf.Rad2Deg) + 1f) / 2f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
}

