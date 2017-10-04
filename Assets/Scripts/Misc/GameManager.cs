using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [Header("Spawn Settings")]
    public GameObject[] enemies;
    public Boundary boundary;
    public float spawnRate;
    public float spawnRateChangeTime; // How long until the spawn gets faster
    public float minSpawnRate;

    [Header("Game Message Settings")]
    public TextMesh gameMessage;
    public TextMesh gameMessageBackground;

    [Header("Countdown Settings")]
    public AudioSource countdown;
    public int countdownTime;

    [Header("Button Settings")]
    public GameObject advertButton;
    public GameObject menuButton;
    public GameObject backButton;

    bool hasShownAdvert;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        advertButton.SetActive(false);
        menuButton.SetActive(false);
        backButton.SetActive(true);

        StartCoroutine(CountdownToStartGame());
    }

    IEnumerator CountdownToStartGame()
    {
        SetGameMessage("READY?");
        yield return new WaitForSeconds(2.5f);
        countdown.Play();

        for (int i = countdownTime; i > 0; i--)
        {
            SetGameMessage(i.ToString());
            yield return new WaitForSeconds(1);
        }

        SetGameMessage("GO!");
        yield return new WaitForSeconds(1f);
        SetGameMessage("");

        SetGameMessage("");

        GameObject.FindObjectOfType<Cannon>().StartShooting();

        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
        InvokeRepeating("ChangeSpawnRate", spawnRateChangeTime, spawnRateChangeTime);
    }

    public void GameOver()
    {
        DestroyAllEnemies();

        SetGameMessage("GAMEOVER!");

        CancelInvoke("SpawnEnemy");
        CancelInvoke("ChangeSpawnRate");
        CancelInvoke("SpawnHealthPack");

        menuButton.SetActive(true);
        advertButton.SetActive(true);
        backButton.SetActive(false);

        if (!hasShownAdvert)
        {
            advertButton.GetComponent<AdvertButton>().IsUsable(true);
            hasShownAdvert = true;
        }
        else
        {
            advertButton.GetComponent<AdvertButton>().IsUsable(false);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        Vector3 spawn = new Vector3(Random.Range(boundary.minX, boundary.maxX), Random.Range(boundary.minY, boundary.maxY), 100f);

        Instantiate(enemy, spawn, Quaternion.identity);
    }

    void ChangeSpawnRate()
    {
        CancelInvoke("SpawnEnemy");

        spawnRate -= 0.25f;

        if (spawnRate <= minSpawnRate)
            spawnRate = minSpawnRate;

        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

    void SetGameMessage(string message)
    {
        gameMessage.text = message;
        gameMessageBackground.text = message;
    }
}

[System.Serializable]
public class Boundary
{
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;
}
