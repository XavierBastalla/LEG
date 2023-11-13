using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public int maxAmmo = 30;
    public float reloadTime = 2f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public AudioSource gunshotSound;
    public AudioSource reloadSound;

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
        gunshotSound.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Apply damage to the target 
            // hit.transform.GetComponent<Enemy>().TakeDamage(damage);

            // handle hit effects (e.g., impact particles, decals).

            // implement things like bullet spread and recoil here.
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        //reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}