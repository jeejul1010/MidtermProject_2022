using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContactOthers : MonoBehaviour
{
    public float damage = 10f;
    public float power = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.CompareTag("Player"))
        {
            if(collision.collider.CompareTag("Box"))
            {
                Target target = collision.gameObject.GetComponent<Target>();
                if(target != null)
                {
                    Debug.Log("colortag: " + gameObject.tag);
                    target.AddColorPoint(damage, gameObject.tag);
                }
            }
            else if(collision.collider.CompareTag("WoodenBox"))
            {
                WoodenTarget w_target = collision.gameObject.GetComponent<WoodenTarget>();
                if(w_target != null)
                {
                    w_target.TakeDamage(damage);
                }
                collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * power);
            }

            Destroy(gameObject);
        }
        
    }
}
