using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    public TextMesh healthText;
    public int health;
    int startingHealth;

    public AudioSource hit;
    public AudioSource dead;

    void Start()
    {
        startingHealth = health;

        for (int i = 0; i < health; i++)
            healthText.text += "-";
    }

    int GetCurrentHealth()
    {
        return healthText.text.Length;
    }

    public void DecreaseHealth(int amount)
    {
        ChangeColour();

        health -= amount;

        if (GetCurrentHealth() > 1)
        {
            healthText.text = healthText.text.Remove(healthText.text.Length - 1);
            hit.Play();
        }

        else
        {
            healthText.text = "";

            dead.Play();

            GetComponent<PlayerScore>().SaveHighscore();

            GameObject.FindObjectOfType<Cannon>().StopShooting();
            GameObject.FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void IncreaseHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            healthText.text += "-";
        }
    }

    public void ResetHealth()
    {
        health = startingHealth;
        IncreaseHealth(startingHealth);
        ChangeColour();
    }

    void ChangeColour()
    {
        if (GetCurrentHealth() > 3 && GetCurrentHealth() < 5)
            healthText.color = Color.yellow;
        else if (GetCurrentHealth() <= 3)
            healthText.color = Color.red;
        else
            healthText.color = Color.green;
    }
}
