using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timer;

    Transform thisTransform;
    private void Start()
    {
        thisTransform = transform;
    }
    public GameObject explosionPrefab;

    [System.Obsolete]
    void Bang()
    {
        Destroy(this.gameObject);
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = thisTransform.position;

        Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
    }

    bool explosionStarted = false;
    public float minimum = 0.5f;
    public float maximum = 2f;

    float boost = 10;
    void Update()
    {
        if (!explosionStarted)
        {
            thisTransform.localScale = new Vector2(Mathf.PingPong(Time.time * boost / (timer * Mathf.Sqrt(timer)), maximum - minimum) + minimum,
                                                   Mathf.PingPong(Time.time * boost / (timer * Mathf.Sqrt(timer)), maximum - minimum) + minimum);
            boost -= Time.deltaTime / 1.7f;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Bang();
                explosionStarted = true;
            }
        }
    }
}
