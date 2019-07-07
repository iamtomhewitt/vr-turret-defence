using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AnimatedButton : MonoBehaviour 
{
    [Header("Loading Progress")]
    public Image fill;
    public float fillSpeed;

    [Space()]
    public AudioSource selectSound;

	public UnityEvent buttonEvent;

    public void IncreaseFill()
    {
        fill.fillAmount += fillSpeed * Time.deltaTime;

        if (fill.fillAmount >= 1f)
        {
			buttonEvent.Invoke();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ResetFill()
    {
        fill.fillAmount = 0f;
    }

    private IEnumerator WaitAndLoadCoroutine(string scene)
    {
        if(!selectSound.isPlaying) selectSound.Play();
        yield return new WaitForSeconds(selectSound.clip.length);
        SceneManager.LoadScene(scene);
    }

	public void WaitAndLoad(string scene)
	{
		StartCoroutine(WaitAndLoadCoroutine(scene));
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}
