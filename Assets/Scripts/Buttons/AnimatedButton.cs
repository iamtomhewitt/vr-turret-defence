using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimatedButton : MonoBehaviour 
{
    [Header("Loading Progress")]
    public Image fill;
    public float fillSpeed;

    [Space()]
    public string sceneToLoad;
    public AudioSource selectSound;

    public void IncreaseFill()
    {
        fill.fillAmount += fillSpeed * Time.deltaTime;

        if (fill.fillAmount >= 1f)
        {
            StartCoroutine(WaitAndLoad());
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ResetFill()
    {
        fill.fillAmount = 0f;
    }

    IEnumerator WaitAndLoad()
    {
        if(!selectSound.isPlaying) selectSound.Play();
        yield return new WaitForSeconds(selectSound.clip.length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
