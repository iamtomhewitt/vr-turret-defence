using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHighscore : MonoBehaviour 
{
    public enum CannonType {Assault, Gatling, Shotgun};
    public CannonType cannonType;

    void Start()
    {
        string highscore = "";

        switch (cannonType)
        {
            case CannonType.Assault:
                highscore = PlayerPrefs.GetInt(Constants.assaultHighscoreKey).ToString();
                break;

            case CannonType.Gatling:
                highscore = PlayerPrefs.GetInt(Constants.gatlingHighscoreKey).ToString();
                break;

            case CannonType.Shotgun:
                highscore = PlayerPrefs.GetInt(Constants.shotgunHighscoreKey).ToString();
                break;
        }

        GetComponent<TextMesh>().text = "HIGHSCORE: "+highscore;
    }
}
