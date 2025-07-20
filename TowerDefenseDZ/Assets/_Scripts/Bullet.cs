using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedY;
    public float speedX;
    Rigidbody2D rigidBody;

    private void Start()
    {
        transform.position += new Vector3(speedX/8, speedY/8, 0);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidBody.linearVelocity = new Vector2(speedX, speedY);

        if (GameManager.instance.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.LoseHealth(1);
        }
        if (other.gameObject.TryGetComponent(out EnemyDiag enemyDiag))
        {
            enemyDiag.LoseHealth(1);
        }
        Destroy(gameObject);
    }

}
