using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") == true || collision.CompareTag("Ground") == true)
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") == true)
        {
            collision.gameObject.GetComponent<PlayerController>().DeathScreen();

            Time.timeScale = 0;

            //Destroy(collision.gameObject);
        }
    }
}
