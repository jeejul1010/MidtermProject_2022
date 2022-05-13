using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenTarget : MonoBehaviour
{
    public float health = 50f;
    public GameObject crackedBoxPrefab;
    public GameObject gunPrefab;
    public bool gunHolder = false;
    public GameObject[] paintPrefab;
    public bool[] paintHolder;


    private void Awake()
    {
        for(int i = 0;i < paintHolder.Length;i++)
        {
            paintHolder[i] = false;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject crackedObj = Instantiate(crackedBoxPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(crackedObj, 3f);

        
        if (gunHolder)
        {
            Instantiate(gunPrefab, new Vector3(transform.position.x, 0.631f, transform.position.z), transform.rotation);
            Debug.Log("I have gun");
        }
        else if (paintHolder[0])
        {
            Instantiate(paintPrefab[0], new Vector3(transform.position.x, 0.407f, transform.position.z), transform.rotation);
            Debug.Log("I have the paintHolder");
        }
        else if (paintHolder[1])
        {
            Instantiate(paintPrefab[1], new Vector3(transform.position.x, 0.407f, transform.position.z), transform.rotation);
            Debug.Log("I have the paintHolder");
        }
        else if (paintHolder[2])
        {
            Instantiate(paintPrefab[2], new Vector3(transform.position.x, 0.407f, transform.position.z), transform.rotation);
            Debug.Log("I have the paintHolder");
        }
    }
    
}
