using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Image healthBar;

    float bulletSpeedX;
    float bulletSpeedY;

    public static int playerHealth = 4;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if(playerHealth <= 0)
        {
            healthBar.fillAmount = 1;
        }
    }

    void TurnLeft()
    {
        transform.Rotate(0,0,45);
    }
    void TurnRight()
    {
        transform.Rotate(0,0,-45);
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet);
        if(Mathf.Round(transform.rotation.eulerAngles.z) == 45) 
        {
            bulletSpeedX = -1.1f;
            bulletSpeedY = 1.1f;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 90)
        {
            bulletSpeedX = -1.5f;
            bulletSpeedY = 0;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 135)
        {
            bulletSpeedX = -1.1f;
            bulletSpeedY = -1.1f;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 180)
        {
            bulletSpeedX = 0;
            bulletSpeedY = -1.5f;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 225)
        {
            bulletSpeedX = 1.1f;
            bulletSpeedY = -1.1f;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 270)
        {
            bulletSpeedX = 1.5f;
            bulletSpeedY = 0;
        }
        else if (Mathf.Round(transform.rotation.eulerAngles.z) == 315)
        {
            bulletSpeedX = 1.1f;
            bulletSpeedY = 1.1f;
        }
        else
        {
            bulletSpeedX = 0;
            bulletSpeedY = 1.5f;
        }
        bulletInstance.GetComponent<Bullet>().speedX = bulletSpeedX;
        bulletInstance.GetComponent<Bullet>().speedY = bulletSpeedY;
    }

    public void LoseHealth(int hp)
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.35f, 0.35f);
        Invoke(nameof(ChangeColorBack), 0.13f);
        playerHealth -= hp;
        healthBar.fillAmount -= 0.25f;
    }

    void ChangeColorBack()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
