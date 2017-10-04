using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public float speed;

    public int health;
    public TextMesh healthText;

    public int pointValue;

    public GameObject explosion;
    public AudioSource hitSound;

    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < health; i++)
        {
            healthText.text += "-";
        }
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    int GetCurrentHealth()
    {
        return healthText.text.Length;
    }

    public void DecreaseHealth(int amount)
    {
        if (GetCurrentHealth() > 1)
        {
            healthText.text = healthText.text.Remove(healthText.text.Length - 1);
        }
        else
        {
            GameObject.FindObjectOfType<PlayerScore>().AddScore(pointValue);
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(e, 3f);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            default:
                break;

            case "Player":
                other.gameObject.GetComponent<PlayerHealth>().DecreaseHealth(1);
                Destroy(this.gameObject);
                break;
        }
    }
}
