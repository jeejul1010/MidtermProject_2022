using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float damage = 200f;
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 5f;
    public float explosionForce = 300f;
    public GameObject explosionSoundPrefab;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        GameObject explosionSound = Instantiate(explosionSoundPrefab, transform.position, transform.rotation);
        Destroy(explosionSound, 3f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody otherRigidbody = nearbyObject.GetComponent<Rigidbody>();
            if(otherRigidbody != null)
            {
                otherRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            WoodenTarget other = nearbyObject.GetComponent<WoodenTarget>();
            
            if(other != null)
            {
                other.TakeDamage(damage);
            }
            
        }

        Destroy(gameObject);
    }
}
