using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce = 1000f;
    public GameObject grenadeSpawner;
    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire2"))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab,
            grenadeSpawner.transform.position,
            grenadeSpawner.transform.rotation);
        Rigidbody grenadeRigidbody = grenade.GetComponent<Rigidbody>();
        grenadeRigidbody.AddForce(grenadeSpawner.transform.forward * throwForce);
    }
}
