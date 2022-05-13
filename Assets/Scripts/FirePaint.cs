using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePaint : MonoBehaviour
{
    public GameObject[] paintPrefab;
    public int chosenPaintIndex;
    public bool havePaint = false;
    public Transform paintSpawner;
    public float power = 6f;

    public AudioSource gunShotSound;
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gun.activeInHierarchy && havePaint && Input.GetMouseButtonDown(0))
        {
            gunShotSound.PlayOneShot(gunShotSound.clip);

            GameObject paint = Instantiate(paintPrefab[chosenPaintIndex], paintSpawner.position, paintSpawner.rotation);

            paint.GetComponent<Rigidbody>().velocity = paint.transform.forward * power;

            Destroy(paint, 5f);
        }
    }
}
