using UnityEngine;
using UnityEngine.UI;

public class EnemyDiag : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private Vector3[] rotations;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image healthBar;

    int health = 3;
    float timer = 0;
    bool timerActive;

    int spawnPoint;
    float speedX;
    float speedY;
    Rigidbody2D rigidBody;

    void Start()
    {
        spawnPoint = Random.Range(0,4);
        transform.position = spawnPoints[spawnPoint]; 
        sprite.transform.eulerAngles = rotations[spawnPoint];
        rigidBody = GetComponent<Rigidbody2D>();
        if (spawnPoint == 0)
        {
            speedX = 0.22f;
            speedY = 0.22f;
            canvas.transform.position += new Vector3(0.05f, 0.35f);
        }
        else if (spawnPoint == 1)
        {
            speedX = -0.22f;
            speedY = 0.22f;
            canvas.transform.position += new Vector3(-0.05f, 0.35f);
        }
        else if (spawnPoint == 2)
        {
            speedX = -0.22f;
            speedY = -0.22f;
            canvas.transform.position += new Vector3(-0.05f, -0.03f);
        }
        else
        {
            speedX = 0.22f;
            speedY = -0.22f;
            canvas.transform.position += new Vector3(0.05f, -0.03f);
        }
    }

    void Update()
    {
        rigidBody.linearVelocity = new Vector2(speedX, speedY);

        if (timerActive)
        {
            if (timer > 0.13f && sprite.GetComponent<SpriteRenderer>().color != Color.white)
            {
                sprite.GetComponent<SpriteRenderer>().color = Color.white;
            }
            timer += Time.deltaTime;
            if (timer > 0.6f)
            {
                HideHealth();
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.IncreaseScore();
        }

        if (GameManager.instance.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }

    public void LoseHealth(int hp)
    {
        health -= hp;
        sprite.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 1f);
        healthBar.fillAmount -= 0.33f;
        canvas.SetActive(true);
        timerActive = true;
        timer = 0;
    }

    void HideHealth()
    {
        canvas.SetActive(false);
        timerActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.LoseHealth(1);
            Destroy(gameObject);
        }
    }

}
