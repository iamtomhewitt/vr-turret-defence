using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float speed;

    void Start()
    {
        Destroy(this.gameObject, 10f);

        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
    }

	void Update () 
    {
        transform.position += transform.forward * speed * Time.deltaTime;	
	}

    void OnCollisionEnter(Collision other)
    {        
        switch (other.gameObject.tag)
        {
            default:
                Destroy(this.gameObject);
                break;

            case "Enemy":
                other.gameObject.GetComponent<Enemy>().DecreaseHealth(1);
                other.gameObject.GetComponent<Enemy>().hitSound.Play();
                Destroy(this.gameObject);
                break;
        }
    }
}
