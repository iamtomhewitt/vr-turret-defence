using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour 
{
    public TextMesh scoreText;
    int score;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void SaveHighscore()
    {
        Cannon cannonUsed = GameObject.FindObjectOfType<Cannon>();

        switch (cannonUsed.type)
        {
            case Cannon.CannonType.assault:
                RetrieveAndSetHighscore("Assault Cannon Highscore");
                break;

            case Cannon.CannonType.shotgun:
                RetrieveAndSetHighscore("Shotgun Cannon Highscore");
                break;

            case Cannon.CannonType.gatling:
                RetrieveAndSetHighscore("Gatling Cannon Highscore");
                break;
        }
    }

    void RetrieveAndSetHighscore(string currentHighscoreKey)
    {
        int currentHighscore = PlayerPrefs.GetInt(currentHighscoreKey);
        if (score > currentHighscore)
        {
            PlayerPrefs.SetInt(currentHighscoreKey, score);
            print("New " + currentHighscoreKey + ": " + score);
        }
    }
}
