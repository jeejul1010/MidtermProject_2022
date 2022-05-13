using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDroppedObj : MonoBehaviour
{
    public bool isPlayerDrop = false;
    public bool isGunDrop = false;
    public bool [] isPaintDrop;

    void Start()
    {
        for(int i = 0;i<isPaintDrop.Length;i++)
        {
            isPaintDrop[i] = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerDrop = true;
            
        }
        else if(other.CompareTag("Gun") || other.gameObject.name == "GunWoodenCrate")
        {
            Debug.Log("Gun dropped");
            isGunDrop = true;
        }
        else if(other.CompareTag("Orange") || other.gameObject.name == "OrangeWoodenCrate")
        {
            Debug.Log("Orange dropped");
            isPaintDrop[0] = true;
        }
        else if(other.CompareTag("Pink") || other.gameObject.name == "PinkWoodenCrate")
        {
            Debug.Log("Pink dropped");
            isPaintDrop[1] = true;
        }
        else if(other.CompareTag("Green") || other.gameObject.name == "GreenWoodenCrate")
        {
            Debug.Log("Green dropped");
            isPaintDrop[2] = true;
        }
    }
}
