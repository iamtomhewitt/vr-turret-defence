using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour 
{
    public enum CannonType {shotgun, gatling, assault};
    public CannonType type;
    
    public GameObject bullet;
    public GameObject muzzleFlash;
    public Transform[] bulletSpawns;
    public float fireRate;

    public int currentSpawn = 0;

    Animator animator;
    int currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartShooting()
    {
        animator.speed = 1f;
        InvokeRepeating("Shoot", fireRate, fireRate);
    }

    public void StopShooting()
    {
        animator.speed = 0f;
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        GameObject m = Instantiate(muzzleFlash, bulletSpawns[currentSpawn].position, bulletSpawns[currentSpawn].rotation);
        Destroy(m, 5f);

        switch (type)
        {
            default:
                Instantiate(bullet, bulletSpawns[currentSpawn].position, bulletSpawns[currentSpawn].rotation);

                ChangeAnimation(currentSpawn);

                currentSpawn++;

                if (currentSpawn >= bulletSpawns.Length)
                    currentSpawn = 0;
                break;

            case CannonType.shotgun:
                Instantiate(bullet, bulletSpawns[currentSpawn].position, bulletSpawns[currentSpawn].rotation);

                foreach (Transform child in bulletSpawns[currentSpawn])
                    Instantiate(bullet, child.position, child.rotation);

                ChangeAnimation(currentSpawn);

                currentSpawn++;

                if (currentSpawn >= bulletSpawns.Length)
                    currentSpawn = 0;

                break;
        }
    }

    public void ChangeAnimation(int state)
    {   
        if (currentAnimation == state)
            return;

        switch (state)
        {
            case Constants.LEFT:
                animator.SetInteger("State", Constants.LEFT);
                break;

            case Constants.RIGHT:
                animator.SetInteger("State", Constants.RIGHT);
                break;

            case Constants.BOTTOM_LEFT:
                animator.SetInteger("State", Constants.BOTTOM_LEFT);
                break;

            case Constants.BOTTOM_RIGHT:
                animator.SetInteger("State", Constants.BOTTOM_RIGHT);
                break;
        }

        currentAnimation = state;
    }
}
