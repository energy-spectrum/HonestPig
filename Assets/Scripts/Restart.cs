using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Text text;
    public MoveFarmer moveFarmer;
    public MovePig movePig;
    public MoveDog moveDog;
    public GameObject curtain;
    public GameObject bomb;

    public void Restart_()
    {
        text.text = "You Win";
        Destroy(curtain);
        if (bomb) Destroy(bomb);

        moveFarmer.Restart();
        movePig.Restart();
        moveDog.Restart();

        transform.parent.localPosition = new Vector3(0, 1000);
    }
}
