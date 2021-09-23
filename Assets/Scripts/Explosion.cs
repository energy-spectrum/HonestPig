using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool wasPig = false, wasFarmer = false, wasDog = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pig" && !wasPig)
        {
            wasPig = true;

            MovePig movePig = collision.transform.parent.GetComponent<MovePig>();
            movePig.inAction = false;
            collision.transform.parent.GetComponent<Contusion>().stunning = 3;
        }

        if (collision.gameObject.tag == "Farmer" && !wasFarmer)
        {
            wasFarmer = true;

            MoveFarmer moveFarmer = collision.transform.parent.GetComponent<MoveFarmer>();
            moveFarmer.stunning = 3f;
            moveFarmer.countExplosion++;
            moveFarmer.changeSprite.Change(VersionOfLivingObject.Dirty, moveFarmer.direction);
            if(moveFarmer.countExplosion > 1)
            {
                moveFarmer.resultPanel.localPosition = moveFarmer.positionResult;
                moveFarmer.moveDog.inAction = false;
                moveFarmer.moveDog.countExplosion = 1;
            }
            else
            {
                moveFarmer.Invoke("MakeAngry", moveFarmer.stunning);
            }
        }

        if (collision.gameObject.tag == "Dog" && !wasDog)
        {
            wasDog = true;
            MoveDog moveDog = collision.transform.parent.GetComponent<MoveDog>();

            moveDog.countExplosion++;
            moveDog.inAction = false;
            moveDog.version = VersionOfLivingObject.Dirty;
            moveDog.changeSprite.Change(moveDog.version, moveDog.direction);
        }
    }
}