using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour 
{
    AnimatedButton lastHitButton;
    AdvertButton lastHitAdvertButton;

    void Update()
    {
        RaycastOut();
    }

    void RaycastOut()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 50f, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Play Button")
            {
                lastHitButton = hit.collider.GetComponent<AnimatedButton>();
                lastHitButton.IncreaseFill();
            }
            else
            {
                if (lastHitButton != null)
                    lastHitButton.ResetFill();
                //return;
            }

            if (hit.collider.tag == "Advert Button")
            {
                lastHitAdvertButton = hit.collider.GetComponent<AdvertButton>();
                lastHitAdvertButton.IncreaseFill();
            }
            else
            {
                if (lastHitAdvertButton != null)
                    lastHitAdvertButton.ResetFill();
                //return;
            }
        }
    }
}
