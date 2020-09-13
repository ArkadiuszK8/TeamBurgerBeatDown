using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lives;

    public GameObject burgerPrefab;

    public float burgerSpeed;

    private EnemySpawner _spawner;

    public bool isBoss;

    public Transform bossCannon1;

    public Transform bossCannon2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReference(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    public void StartShooting(float minRate, float maxRate)
    {
        float rate = Random.Range(minRate, maxRate);

        float cooldown = Random.Range(0, rate);

        if (isBoss == true)
        {
            InvokeRepeating("Shoot", cooldown, rate * 0.31256f);
        }

        InvokeRepeating("Shoot", cooldown, rate);
    }

    public void StartMovement(float minSpeed, float maxSpeed)
    {
        //float speed = Random.Range(minSpeed, maxSpeed);

        //GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    private void Shoot()
    {
        if (isBoss == true)
        {
            Transform randomCannon = Random.Range(0, 2) == 0 ? bossCannon1 : bossCannon2;

            GameObject burger = Instantiate(burgerPrefab, randomCannon.position, Quaternion.identity);

            burger.GetComponent<Rigidbody2D>().velocity = Vector2.down * burgerSpeed;
        }
        else
        {
            GameObject burger = Instantiate(burgerPrefab, transform.position, Quaternion.identity);

            burger.GetComponent<Rigidbody2D>().velocity = Vector2.down * burgerSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") == true)
        {
            lives--;

            Destroy(collision.gameObject);

            if (lives == 0)
            {
                Destroy(gameObject);

                _spawner.EnemyShooted();
            }
        }
        else if (collision.CompareTag("Ground") == true)
        {
            Destroy(gameObject);

            _spawner.EnemyShooted();
        }
        else if (collision.CompareTag("Player") == true)
        {
            collision.gameObject.GetComponent<PlayerController>().DeathScreen();

            Time.timeScale = 0;

            //Destroy(collision.gameObject);
        }
    }
}
