using UnityEngine;
using UnityEngine.UI;

public class PlantBomb : MonoBehaviour
{
    public MovePig movePig;
    public GameObject bombPrefab;

    public Restart restart;

    public float rechargeTimeInput = 6f;
    private float rechargeTime;

    bool canPlant = true;
    public void Plant()
    {
        if (movePig.isDead) return;
        if (canPlant)
        {
            restart.bomb = Instantiate(bombPrefab);

            restart.bomb.transform.position = transform.position;
            rechargeTime = rechargeTimeInput;

            rechargeImage.fillAmount = 0f;
            canPlant = false;
        }
    }
    public Image rechargeImage;

    private void Update()
    {
        rechargeTime -= Time.deltaTime;
        
        if (rechargeTime < 0)
        {
            canPlant = true;
        }
        else
        {
            rechargeImage.fillAmount += 1f / rechargeTimeInput * Time.deltaTime;
        }
    }
}
