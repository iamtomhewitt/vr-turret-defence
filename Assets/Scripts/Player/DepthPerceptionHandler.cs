using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthPerceptionHandler : MonoBehaviour 
{
    public Camera _camera;
    float maxDistance = Mathf.Infinity;
    Vector3 originalScale;
    Vector3 tempPos;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        RaycastHit hit;
        float distance;

        Debug.DrawRay(transform.position, transform.forward*20000f, Color.red);

        // If we hit something
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxDistance))
        {
            // Then the distance is the object we hit's distance
            if (hit.collider.gameObject.tag == "Enemy")
                distance = hit.distance;

            else
                distance = _camera.farClipPlane * 0.95f;
        }
        else
        {
            distance = _camera.farClipPlane * 0.95f;
        }

        // Set the new crosshair position - which is the current position multiplied by the distance of the object we hit
        tempPos.x = transform.position.x;
        tempPos.y = transform.position.y;
        tempPos.z = distance;

        //transform.position = tempPos;
        transform.position = _camera.transform.position + (_camera.transform.forward * distance);
        transform.LookAt(_camera.transform.position);

        // Set the scale to the original scale multiplied by the distance of the object we hit
        transform.localScale = originalScale * distance;
    }
}
