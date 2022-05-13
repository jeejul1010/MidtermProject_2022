using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    public AudioSource explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        explosionSound.PlayOneShot(explosionSound.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
