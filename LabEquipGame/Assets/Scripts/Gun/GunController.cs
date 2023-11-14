using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// sound stuff
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public int maxAmmo = 30;
    public float reloadTime = 2f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;

    //public AudioSource gunshotSound;
    //public AudioSource reloadSound;

    public FMODUnity.EventReference pistol;
    public FMODUnity.EventReference reload;
    
    FMOD.Studio.EventInstance reloadSFX; 


    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

            
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        //gunshotSound.Play();

        //gunshotsound
        EventInstance pistolSFX;
        pistolSFX = FMODUnity.RuntimeManager.CreateInstance(pistol); //creates the event
        pistolSFX.start();    //plays the event
        pistolSFX.release(); // destroys the event after it plays


        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                // Apply damage to the enemy
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        //reloadSound.Play();
        //gunshotsound
        reloadSFX = FMODUnity.RuntimeManager.CreateInstance(reload); //creates the event
        reloadSFX.start();    //plays the event
        reloadSFX.release(); // destroys the event after it plays



        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}