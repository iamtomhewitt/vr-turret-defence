using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;

public class AdvertButton : MonoBehaviour 
{
    [Header("Loading Progress")]
    public Image fill;
    public float fillSpeed;
    public Image background;

    void Awake()
    {
        Advertisement.Initialize("1550752", true);
    }

    public void ShowAdvert(string zone = "")
    {
        #if UNITY_EDITOR
        //StartCoroutine(WaitForAdvert());
        #endif

        if (string.Equals (zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions ();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady (zone))
            Advertisement.Show (zone, options);
    }

    void AdCallbackhandler (ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                GameObject.FindObjectOfType<PlayerHealth>().ResetHealth();
                GameObject.FindObjectOfType<GameManager>().StartGame();
                IsUsable(false);
                break;

            case ShowResult.Skipped:
                Debug.Log("Advert skipped. Do not reward.");
                SceneManager.LoadScene("Main Menu");
                break;

            case ShowResult.Failed:
                Debug.Log("Advert Failed. Uh oh.");
                SceneManager.LoadScene("Main Menu");
                break;
        }
    }

    IEnumerator WaitForAdvert()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }

    public void IncreaseFill()
    {
        fill.fillAmount += fillSpeed * Time.deltaTime;

        if (fill.fillAmount >= 1f)
        {
            ShowAdvert();
            ResetFill();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ResetFill()
    {
        fill.fillAmount = 0f;
    }

    public void IsUsable(bool useable)
    {
        GetComponent<BoxCollider>().enabled = useable;

        if (!useable)
            background.color = Color.gray;
    }
}
