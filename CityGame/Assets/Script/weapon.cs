using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    public Camera FPcamera;
    public float Range = 100f;
    public float damage = 20f;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    AudioSource audioSource;
    AudioClip silencer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            muzzleFlashEffect();
            audioSource.Play();

        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, Range))
        {
            CreatHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }

    }
    private void muzzleFlashEffect()
    {
        muzzleFlash.Play();
    }
    private void CreatHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
