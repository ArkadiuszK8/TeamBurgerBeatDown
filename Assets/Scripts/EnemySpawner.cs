using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float yPosOffset;

    public float[] slots;

    public int wavesToBoss;

    public GameObject enemyPrefab;

    public GameObject bossPrefab;

    public float minRate;

    public float maxRate;

    public float rateMultiplier;

    public float enemyMinSpeed;

    public float enemyMaxSpeed;

    private int currentWave;

    private int enemiesToShoot;

    public GameObject winScreen;

    public GameObject waveText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesToShoot == 0 && currentWave <= wavesToBoss)
        {
            Invoke("SpawnNewWave", 2);
            enemiesToShoot = -1;

            waveText.SetActive(true);
            Invoke("HideWaveText", 2);
            if (currentWave < wavesToBoss - 1)
            {
                waveText.GetComponent<Text>().text = "WAVE: " + (currentWave + 1);
            }
            else if (currentWave == wavesToBoss - 1)
            {
                waveText.GetComponent<Text>().text = "BOSS";
            }

        }
        else if (currentWave == wavesToBoss + 1)
        {
            winScreen.SetActive(true);

            Time.timeScale = 0;

            currentWave++;
        }
    }

    private void HideWaveText()
    {
        waveText.SetActive(false);
    }

    public void SpawnNewWave()
    {
        currentWave++;

        if (currentWave != wavesToBoss)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                float yPosition = i % 2 == 0 ? 0 : 1;

                GameObject enemy = Instantiate(enemyPrefab, new Vector3(slots[i], transform.position.y + yPosition, transform.position.z), Quaternion.identity);

                Enemy enemyComp = enemy.GetComponent<Enemy>();

                enemyComp.StartShooting(minRate, maxRate);

                //enemyComp.StartMovement(enemyMinSpeed, enemyMaxSpeed);

                enemyComp.SetReference(this);

                enemyComp.lives = currentWave;
            }

            enemiesToShoot = slots.Length;
        }
        else
        {
            GameObject boss = Instantiate(bossPrefab);

            Enemy enemyComp = boss.GetComponent<Enemy>();

            enemyComp.StartShooting(minRate, maxRate);

            //enemyComp.StartMovement(enemyMinSpeed, enemyMaxSpeed);

            enemyComp.SetReference(this);

            enemiesToShoot = 1;
        }

        minRate *= rateMultiplier;

        maxRate *= rateMultiplier;


    }

    public void EnemyShooted()
    {
        enemiesToShoot--;
    }

}
