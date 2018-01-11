using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardwareCheck : MonoBehaviour 
{
	void Start () 
    {
        if (!SystemInfo.supportsGyroscope)
        {
            SceneManager.LoadScene("Hardware Check");
        }
	}	
}
