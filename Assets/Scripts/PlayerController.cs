using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public float maxDistance;

    public GameObject bulletPrefab;

    public float bulletSpeed;

    public float fireCooldown;

    public GameObject deathScreen;

    private float fireTimer;

    private bool run;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.Space) == true)
        {
            Fire();
        }

        fireTimer += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (run == true)
        {
            GetComponent<Animator>().SetBool("Run", true);

            run = false;
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", false);
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x > -maxDistance)
        {
            GetComponent<SpriteRenderer>().flipX = true;

            transform.position += Vector3.left * speed * Time.deltaTime;

            run = true;
        }
    }

    public void MoveRight()
    {
        if (transform.position.x < maxDistance)
        {
            GetComponent<SpriteRenderer>().flipX = false;

            transform.position += Vector3.right * speed * Time.deltaTime;

            run = true;
        }
    }

    public void Fire()
    {
        if (fireTimer >= fireCooldown)
        {

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

            fireTimer = 0;
        }
    }

    public void DeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
